using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Lambda.Core;
using Amazon.S3;
using Amazon.S3.Model;
using MiniValidation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AWSFiles.PostFile;

public class Function
{
    private readonly IAmazonDynamoDB _ddbClient;
    private readonly IAmazonS3 _s3Client;
    private readonly FunctionOptions _options;

    public Function()
    {
        _ddbClient = new AmazonDynamoDBClient();
        _s3Client = new AmazonS3Client();
        _options = new FunctionOptions()
        {
            BucketName = Environment.GetEnvironmentVariable("BUCKET_NAME")
                ?? throw new InvalidOperationException("BUCKET_NAME environment variable not found.")
        };
    }

    /// <summary>
    /// A simple function that takes a file and saves it
    /// </summary>
    /// <param name="request">The event for the Lambda function handler to process.</param>
    /// <param name="context">The ILambdaContext that provides methods for logging and describing the Lambda environment.</param>
    /// <returns>The response.</returns>
    public async Task<Response> FunctionHandler(Request request, ILambdaContext context)
    {
        if (!MiniValidator.TryValidate(request, out var errors))
        {
            throw new InvalidOperationException($"The request was invalid: {JsonSerializer.Serialize(errors)}.");
        }

        var id = Guid.NewGuid().ToString();

        using var ddbContext = new DynamoDBContext(_ddbClient);
        await ddbContext.SaveAsync(new File
        {
            Id = id,
            Name = request.Name
        });

        context.Logger.LogInformation($"Saved {id} to database.");

        await _s3Client.PutObjectAsync(new PutObjectRequest
        {
            BucketName = _options.BucketName,
            Key = id,
            InputStream = new MemoryStream(request.Content)
        });

        context.Logger.LogInformation($"Saved {id} to S3.");

        return new Response
        {
            Id = id
        };
    }
}

public class FunctionOptions
{
    public string BucketName { get; set; } = string.Empty;
}

public class Request
{
    [Required(AllowEmptyStrings = false)]
    [FileName]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Length(1, 5 * 1024 * 1024)]
    public byte[] Content { get; set; } = [];
}

public class Response
{
    public string Id { get; set; } = string.Empty;
}

[DynamoDBTable("Files")]
public class File
{
    [DynamoDBHashKey]
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}
