using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Lambda.Core;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AWSFiles.GetDownloadUrl;

public class Function
{
    private readonly IDynamoDBContext _ddbContext;
    private readonly IAmazonS3 _s3Client;
    private readonly FunctionOptions _options;

    public Function()
    {
        AWSConfigsS3.UseSignatureVersion4 = true;

        var ddbClient = new AmazonDynamoDBClient();
        var configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();

        _ddbContext = new DynamoDBContext(ddbClient);
        _s3Client = new AmazonS3Client();
        _options = configuration.Get<FunctionOptions>()
            ?? throw new InvalidOperationException("Function options not found in configuration.");
    }

    /// <summary>
    /// A simple function that creates the download URL.
    /// </summary>
    /// <param name="id">The event for the Lambda function handler to process.</param>
    /// <param name="context">The ILambdaContext that provides methods for logging and describing the Lambda environment.</param>
    /// <returns>The response.</returns>
    public async Task<Response?> FunctionHandler(string id, ILambdaContext context)
    {
        var file = await _ddbContext.LoadAsync<File>(id);
        if (file is null)
        {
            return null;
        }

        context.Logger.LogInformation($"Loaded {id} from database.");

        string url = _s3Client.GetPreSignedURL(new GetPreSignedUrlRequest
        {
            BucketName = _options.BucketName,
            Key = file.Name,
            Verb = HttpVerb.GET,
            Expires = DateTime.UtcNow.Add(_options.UrlExpiration)
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
    public TimeSpan UrlExpiration { get; set; }
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
