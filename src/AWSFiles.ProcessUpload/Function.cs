using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Lambda.Core;
using Amazon.Lambda.S3Events;
using Amazon.S3;
using Amazon.S3.Model;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AWSFiles.ProcessUpload;

public class Function
{
    private readonly IAmazonS3 _s3Client;
    private readonly IAmazonDynamoDB _ddbClient;
    private readonly FunctionOptions _options;

    public Function()
    {
        AWSConfigsS3.UseSignatureVersion4 = true;

        string maxFileSize = Environment.GetEnvironmentVariable("MAX_FILE_SIZE")
            ?? throw new InvalidOperationException("MAX_FILE_SIZE environment variable not found.");

        _s3Client = new AmazonS3Client();
        _ddbClient = new AmazonDynamoDBClient();
        _options = new FunctionOptions
        {
            MaxFileSize = long.Parse(maxFileSize)
        };
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
                await _s3Client.DeleteObjectAsync(new DeleteObjectRequest
                {
                    BucketName = s3Event.Bucket.Name,
                    Key = s3Event.Object.Key
                });

                context.Logger.LogInformation($"{id} exceeded max file size. Deleted from S3.");

                using var ddbContext2 = new DynamoDBContext(_ddbClient);
                await ddbContext2.DeleteAsync<File>(id);

                context.Logger.LogInformation($"Deleted {id} from database.");

                continue;
            }

            using var ddbContext = new DynamoDBContext(_ddbClient);
            await ddbContext.SaveAsync(new File
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
