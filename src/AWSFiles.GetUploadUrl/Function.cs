using Amazon;
using Amazon.Lambda.Core;
using Amazon.S3;
using Amazon.S3.Model;
using MiniValidation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AWSFiles.GetUploadUrl;

public class Function
{
    private readonly IAmazonS3 _s3Client;
    private readonly FunctionOptions _options;

    public Function()
    {
        AWSConfigsS3.UseSignatureVersion4 = true;

        _s3Client = new AmazonS3Client();
        _options = new FunctionOptions()
        {
            BucketName = Environment.GetEnvironmentVariable("BUCKET_NAME")
                ?? throw new InvalidOperationException("BUCKET_NAME environment variable not found.")
        };
    }

    /// <summary>
    /// A simple function that creates the upload URL.
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

        string id = Guid.NewGuid().ToString();
        string fileName = $"{id}{Path.GetExtension(request.Name)}";

        string url = await _s3Client.GetPreSignedURLAsync(new GetPreSignedUrlRequest
        {
            BucketName = _options.BucketName,
            Key = fileName,
            Verb = HttpVerb.PUT,
            ContentType = MimeTypes.GetMimeType(request.Name),
            Expires = DateTime.UtcNow.AddHours(1)
        });

        context.Logger.LogInformation($"Created upload URL for {id}.");

        return new Response
        {
            Id = id,
            Url = url
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
    [AllowedExtensions(".jpg", ".mp3", ".mp4", ".txt", ".json")]
    public string Name { get; set; } = string.Empty;
}

public class Response
{
    public string Id { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}
