using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Lambda.Core;
using Amazon.Lambda.S3Events;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AWSFiles.ProcessUpload;

public class Function
{
    private readonly IAmazonDynamoDB _ddbClient;

    public Function()
    {
        _ddbClient = new AmazonDynamoDBClient();
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

            context.Logger.LogInformation($"Read {id} from S3 event.");

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

[DynamoDBTable("Files")]
public class File
{
    [DynamoDBHashKey]
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}
