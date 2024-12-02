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
    "Id": "4241b469-deef-4dce-995a-bcdb492d0e20"
}
```
```JavaScript
Invoke GetFile
"4241b469-deef-4dce-995a-bcdb492d0e20"
```
```JavaScript
{
    "Name": "test.txt",
    "Url": "https://mariusz96-aws-files.s3.amazonaws.com/4241b469-deef-4dce-995a-bcdb492d0e20?AWSAccessKeyId=ASIAU6GD2XJEOBLQYNXP&Expires=1733135272&x-amz-security-token=IQoJb3JpZ2luX2VjEBIaCXVzLWVhc3QtMSJGMEQCIBOjAG1N38sBj3%2BuHamOgigeg7AMY44vigT4nVjscc%2FNAiB7N1oyrndIFpdnNJBXagbVTjZfAQ7bSa6YQp1%2FJ%2BenhSrrAgi7%2F%2F%2F%2F%2F%2F%2F%2F%2F%2F8BEAAaDDMzOTcxMzExMjY0OCIMyI6lp79iu2Rgo4VYKr8C%2FtqxrXF%2F5K0qRgE%2FyKZuVCB46WDh%2B%2BKZHgGtgAGfOg0h8HnHHfE%2FS6a%2BYRwpCwfZBJfQA43iacJMOlrwWjIICu4CkbL%2B051DBOuzKdQ6l8GEk5tYCtRK0WAizTW%2BdMM8h3Dxd8CUlJFldOqYJsSjbiydkbjapujLAOMwYcE%2Bg9CTuDhrWT0VcrDGL3ttwVJm3RFHlTz%2FEPzfTWHk%2FN2ZLz%2FgGHESnR73CrAoXcDlK3CQrijf7%2BGCkgizj5CmT6u3B1QQy2X%2BQCJReLPz7GBxTlXY8UoozWZGj7nGkGAXfchXbYqjz2szwXa6SMcX9MJlQT7tRhDYw0wAVD9ycq%2FW8qG%2BY1MUxeLMsXDQKtB8Qa5kAi%2BiTtp6JoUnBRuRjdfRICwXic5vt85xvyVpRGdrClk1z7HF6hto5eE%2F6kHGcTCV%2B7W6BjqfAUfpP%2BAHqE%2Bm0MT26t1bQzSs7K%2FsUgx961JonntNfPvRd25vriRLPPjGJP0cPeUnrB4qMLAoc9qE2wV7tQmfMZMTXWVg4BlC6NbgjofzsbDhqiWKMKhoc9n6Bi%2Fa0O72%2BBup%2FEG2Zipq8sZ0N0mEVTf6d6CozhVJt1y%2FWT66sPEGuO1p3JZRJgFrzkQwcPb2B0zmq2Ihzyr%2FuuqI8k33Qg%3D%3D&Signature=%2FDtv1gxRb%2F6yJnPeSfwGkljiljM%3D"
}
```

## Prerequisites:
- .NET 8.0
- Visual Studio 2022
- AWS Toolkit with Amazon Q 1.60.0.1

## Test:
- `Test Explorer` > `Run All Tests`

## Deploy:
- Create new S3 bucket with unique name and cofigure `BUCKET_NAME` environment variable in `aws-lambda-tools-defaults.json` files
- Create new DynamoDB table with `Files` table name and `Id` hash key name
- Right click on `AWSFiles.PostFile` and `AWSFiles.GetFile` and `Publish to AWS Lambda...`
- Make sure lambdas IAM roles have permissions to access created resources
- Delete created resources to prevent ongoing charges

## AWS services used:
- Lambda
- DynamoDB
- S3
