using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Lambda.Core;
using Amazon.S3;
using Amazon.S3.Model;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AWSFiles.GetDownloadUrl;

public class Function
{
    private readonly IAmazonDynamoDB _ddbClient;
    private readonly IAmazonS3 _s3Client;
    private readonly FunctionOptions _options;

    public Function()
    {
        AWSConfigsS3.UseSignatureVersion4 = true;

        _ddbClient = new AmazonDynamoDBClient();
        _s3Client = new AmazonS3Client();
        _options = new FunctionOptions()
        {
            BucketName = Environment.GetEnvironmentVariable("BUCKET_NAME")
                ?? throw new InvalidOperationException("BUCKET_NAME environment variable not found.")
        };
    }

    /// <summary>
    /// A simple function that creates the download URL.
    /// </summary>
    /// <param name="id">The event for the Lambda function handler to process.</param>
    /// <param name="context">The ILambdaContext that provides methods for logging and describing the Lambda environment.</param>
    /// <returns>The response.</returns>
    public async Task<Response?> FunctionHandler(string id, ILambdaContext context)
    {
        using var ddbContext = new DynamoDBContext(_ddbClient);
        var file = await ddbContext.LoadAsync<File>(id);
        if (file is null)
        {
            return null;
        }

        context.Logger.LogInformation($"Loaded {id} from database.");

        string url = await _s3Client.GetPreSignedURLAsync(new GetPreSignedUrlRequest
        {
            BucketName = _options.BucketName,
            Key = file.Name,
            Verb = HttpVerb.GET,
            Expires = DateTime.UtcNow.AddHours(1)
        });

        context.Logger.LogInformation($"Created download URL for {id}.");

        return new Response
        {
            Url = url
        };
    }
}

public class FunctionOptions
{
    public string BucketName { get; set; } = string.Empty;
}

public class Response
{
    public string Url { get; set; } = string.Empty;
}

[DynamoDBTable("Files")]
public class File
{
    [DynamoDBHashKey]
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}
