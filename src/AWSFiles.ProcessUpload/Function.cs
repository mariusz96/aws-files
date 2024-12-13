using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Lambda.Core;
using Amazon.Lambda.S3Events;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AWSFiles.ProcessUpload;

public class Function
{
    private readonly IAmazonS3 _s3Client;
    private readonly IDynamoDBContext _ddbContext;
    private readonly FunctionOptions _options;

    public Function()
    {
        AWSConfigsS3.UseSignatureVersion4 = true;

        var ddbClient = new AmazonDynamoDBClient();
        var configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();

        _s3Client = new AmazonS3Client();
        _ddbContext = new DynamoDBContext(ddbClient);
        _options = configuration.Get<FunctionOptions>()
            ?? throw new InvalidOperationException("Function options not found in configuration.");
    }

    /// <summary>
    /// A simple function that processes the upload.
    /// </summary>
    /// <param name="evnt">The event for the Lambda function handler to process.</param>
    /// <param name="context">The ILambdaContext that provides methods for logging and describing the Lambda environment.</param>
    public async Task FunctionHandler(S3Event evnt, ILambdaContext context)
    {
        var eventRecords = evnt.Records ?? [];
        foreach (var record in eventRecords)
        {
            var s3Event = record.S3;
            if (s3Event is null)
            {
                continue;
            }

            string id = Path.GetFileNameWithoutExtension(s3Event.Object.Key);
            if (string.IsNullOrEmpty(id))
            {
                continue;
            }

            if (s3Event.Object.Size > _options.MaxFileSize)
            {
                context.Logger.LogInformation($"{id} exceeded max file size.");

                await _s3Client.DeleteObjectAsync(new DeleteObjectRequest
                {
                    BucketName = s3Event.Bucket.Name,
                    Key = s3Event.Object.Key
                });

                context.Logger.LogInformation($"Deleted {id} from S3.");

                await _ddbContext.DeleteAsync<File>(id);

                context.Logger.LogInformation($"Deleted {id} from database.");

                continue;
            }

            await _ddbContext.SaveAsync(new File
            {
                Id = id,
                Name = s3Event.Object.Key
            });

            context.Logger.LogInformation($"Saved {id} to database.");
        }
    }
}

public class FunctionOptions
{
    public long MaxFileSize { get; set; }
}

[DynamoDBTable("Files")]
public class File
{
    [DynamoDBHashKey]
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}
