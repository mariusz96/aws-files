# AWS Files
A serverless API in C# and AWS:
```JavaScript
Invoke PostFile
{
    "Name": "test.txt",
    "Content": "dGVzdA=="
}
```
```JavaScript
{
    "Id": "2a12beb9-6769-46a0-bd83-cff0aaed0b1e"
}
```
```JavaScript
Invoke GetFile
"2a12beb9-6769-46a0-bd83-cff0aaed0b1e"
```
```JavaScript
{
    "Name": "test.txt",
    "Url": "https://mariusz96-aws-files.s3.amazonaws.com/2a12beb9-6769-46a0-bd83-cff0aaed0b1e?AWSAccessKeyId=ASIAU6GD2XJEI5ZQYCGF&Expires=1733172796&x-amz-security-token=IQoJb3JpZ2luX2VjEBwaCXVzLWVhc3QtMSJHMEUCIAFpUXDXqEO0r3KS7GFiDPF2jdB3nsuhUFeipf73%2F%2B3cAiEA4aJFbcWvlyt8MuEzM%2BXEY9obN8NQKAmdu5oTu%2F7OBtEq6wIIxf%2F%2F%2F%2F%2F%2F%2F%2F%2F%2FARAAGgwzMzk3MTMxMTI2NDgiDHe2jj0qDEH1%2FiLnESq%2FAtrb4ZS57aM2TahhhtBCJtf3Fg2n8GOOlQhlRpZel56wYUTOm8lObQOhRez0Q2sN8UbAwQPXz1WwOYgh7fU9H%2FJVgGYgjjLWAEg7qihMaSxBJBrTYOigX23iPFhskqeaevu8VQZw%2BmMQ959ewVsuojA%2FKR%2BXf3HsGVw4o3%2FglGU4fR44rbquWhDWqDIKG8e1t6T6lgR3sX8mEELwAyUMXz03sSKZmNRmG06fn4%2B1ZYqhCTWKLhReBhJhbPGZV%2BA4jgx4C2lpCZrAwTEDCe5EtGH9kmlf36BpcO8KMeLzu3Znhtq%2BHfjMLZBcmw1bIbABYOfEXEEtgdl4CsKllKlb4y98pCA7JAVQU3Jo3CmZhRQ9SO7Wd0JlecmyhyU8UFD3TO6E4PTL5MkHVW%2FW22dTUhTpkKQSN%2FhBIuPyAeyLDbUwpKC4ugY6ngGhMIuP0PmrLDSlv3RlmbCYqk8E7%2BGYhYLiGw4%2FDFCVf5WLTzBnMd0dV%2F1LrY0C%2Bp3MTBocPXUBEREFKZUWm4hU%2BfzzqEPF3VqnbJxmVnrUii6qgxUYMqjJkMhovI%2F6tZA7%2FcaHYI2LR3i70GZt6IT86sRS5JhjYLMNV7MKV8hULdCCI7S8MDD39FbbgwRbwu7Cj0x%2BxB7rYFxe1vjL7g%3D%3D&Signature=nulOcSI%2BrGYqAq87wPhN6EojXco%3D"
}
```

## Prerequisites:
- .NET 8.0
- Visual Studio 2022
- AWS Toolkit with Amazon Q 1.60.0.1

## Test:
- `Test Explorer` > `Run All Tests`

## Deploy:
- Create a new S3 bucket with unique name and cofigure `BUCKET_NAME` environment variable in `aws-lambda-tools-defaults.json` files
- Create a new DynamoDB table with `Files` table name and `Id` hash key name
- Publish `AWSFiles.PostFile` and `AWSFiles.GetFile` functions to Lambda
- Add permisions to functions' IAM roles to access other created resources
- Delete all created resources to prevent ongoing charges

## AWS services used:
- Lambda
- DynamoDB
- S3
