# AWS Files
A serverless API in C# and AWS:
```JavaScript
Invoke GetUploadUrl
{
    "Name": "photo.jpg"
}
```
```JavaScript
{
    "Id": "a3ccf8ac-fed4-458c-a0b3-9d552f634403",
    "Url": "https://mariusz96-aws-files.s3.amazonaws.com/a3ccf8ac-fed4-458c-a0b3-9d552f634403.jpg?X-Amz-Expires=3600&X-Amz-Security-Token=IQoJb3JpZ2luX2VjEGQaCXVzLWVhc3QtMSJHMEUCIQCRQm7txHpdiGHS0YeZRNyvBw8F%2BlcRcnmI8UoPdH4M0wIgF9zp8i%2BbSyN8lOyzCAbhok%2FtVfTJoVMKHgjpZNOgUgwq7AIIHRAAGgwzMzk3MTMxMTI2NDgiDHtUo0twQERh4aQ7%2ByrJAmtQ%2BTG0%2FbyScd13h9q3GiEMpzKktpDo15eag8Sdqth974ATBVXZVfN19IoH4JPn%2FGsspnk6EBFrGExdOH9pZJ7NmH%2FRQ2DbTFDoRkRYky0dMP04m46NOwYg238GWYarw26SEv8%2FFaf%2B7sZsCAhEffVY7wdGshy4AO0xxPgz%2BnVunA6ttOreNIhmrh2jCXLPBEahG3sHgaiIfTNTWPnWNeFpXvqupJjM9PFDINLJMXE7cJNiyNQc2mUL68FfljHTchL%2BaBX1BTlD8NizO78PHQO7DbiYyEU23Q0%2Bh2s2sg1weMlkzdHHbU6j6Aj7Z%2F8KkR9pcnoudMP9weHWZpcqc%2Fr8KxYgP4dmdJTupqhuxeM%2FQeaJnC5UJh3UdYFvkqDaVwtn8hYqNmXA1Mwfq%2Fnw4V4bkIIzhPmt3a0EfQFccmzBM1OVY2TXbB4pMIyOyLoGOp4BUdZ3X9SriAXwz7NS4yv0ktYhG5XhHno5N85wOS%2B78GJIp3XEHh8Hwb1rEMXmW8zDf9c%2FNptc6%2FaxEn%2ByaWTHO60frSEsu1%2BhrKRVJTLLKYptHpLtlrCaM8l23823KqMYu9rkYe3ZcmzfFEX9Ta3kP8hYAyoUA5wA4m5oCDhdy9bt8zQvhALYDYr5GlHc8BICtSFFoMDe7ytPVdme6vw%3D&X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=ASIAU6GD2XJEGZXCTQUL%2F20241205%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20241205T200325Z&X-Amz-SignedHeaders=content-type%3Bhost&X-Amz-Signature=918ddced2be9ae93c85398f48ec71242a49d75d81a371f6f35f7e1526013a8f1"
}
```
```JavaScript
PUT https://mariusz96-aws-files.s3.amazonaws.com/a3ccf8ac-fed4-458c-a0b3-9d552f634403.jpg?X-Amz-Expires=3600&X-Amz-Security-Token=IQoJb3JpZ2luX2VjEGQaCXVzLWVhc3QtMSJHMEUCIQCRQm7txHpdiGHS0YeZRNyvBw8F%2BlcRcnmI8UoPdH4M0wIgF9zp8i%2BbSyN8lOyzCAbhok%2FtVfTJoVMKHgjpZNOgUgwq7AIIHRAAGgwzMzk3MTMxMTI2NDgiDHtUo0twQERh4aQ7%2ByrJAmtQ%2BTG0%2FbyScd13h9q3GiEMpzKktpDo15eag8Sdqth974ATBVXZVfN19IoH4JPn%2FGsspnk6EBFrGExdOH9pZJ7NmH%2FRQ2DbTFDoRkRYky0dMP04m46NOwYg238GWYarw26SEv8%2FFaf%2B7sZsCAhEffVY7wdGshy4AO0xxPgz%2BnVunA6ttOreNIhmrh2jCXLPBEahG3sHgaiIfTNTWPnWNeFpXvqupJjM9PFDINLJMXE7cJNiyNQc2mUL68FfljHTchL%2BaBX1BTlD8NizO78PHQO7DbiYyEU23Q0%2Bh2s2sg1weMlkzdHHbU6j6Aj7Z%2F8KkR9pcnoudMP9weHWZpcqc%2Fr8KxYgP4dmdJTupqhuxeM%2FQeaJnC5UJh3UdYFvkqDaVwtn8hYqNmXA1Mwfq%2Fnw4V4bkIIzhPmt3a0EfQFccmzBM1OVY2TXbB4pMIyOyLoGOp4BUdZ3X9SriAXwz7NS4yv0ktYhG5XhHno5N85wOS%2B78GJIp3XEHh8Hwb1rEMXmW8zDf9c%2FNptc6%2FaxEn%2ByaWTHO60frSEsu1%2BhrKRVJTLLKYptHpLtlrCaM8l23823KqMYu9rkYe3ZcmzfFEX9Ta3kP8hYAyoUA5wA4m5oCDhdy9bt8zQvhALYDYr5GlHc8BICtSFFoMDe7ytPVdme6vw%3D&X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=ASIAU6GD2XJEGZXCTQUL%2F20241205%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20241205T200325Z&X-Amz-SignedHeaders=content-type%3Bhost&X-Amz-Signature=918ddced2be9ae93c85398f48ec71242a49d75d81a371f6f35f7e1526013a8f1
Content-Type: image/jpeg
(binary)
```
```JavaScript
200 OK
```
```JavaScript
Invoke GetDownloadUrl
"a3ccf8ac-fed4-458c-a0b3-9d552f634403"
```
```JavaScript
{
    "Url": "https://mariusz96-aws-files.s3.amazonaws.com/a3ccf8ac-fed4-458c-a0b3-9d552f634403.jpg?X-Amz-Expires=3600&X-Amz-Security-Token=IQoJb3JpZ2luX2VjEGUaCXVzLWVhc3QtMSJGMEQCIHnBXGolUBPxA%2BLulz5%2FqqW75u6hLImQMHOkD01%2FEoVWAiA36Z5KWtPfcPkmFQwmqFglAb2DH08dHJy4AI1%2Ffz0hASrwAggdEAAaDDMzOTcxMzExMjY0OCIMPFXNZiaOafaYCdLCKs0CabT8dK%2B3gpIUunlATL5MP0MqS9vc3Vt%2FIUCaY36d3q4uY7AVVtywAADCp8CEmlQRCgok1dmqDnRuAETEpH%2BrMFgy73HWO6V9eWST0659hmkagWlfyoW4iQmUJnOq%2FQ%2FytzlrKBJ6xV3yyxs8QIyF0yeRUzBoc8kcu1S0tYC2ihu8Sxd1neRml5PpuE1ORD91hveyBUfujI1tEtUPQDrOsfpisHT78GMdWmFULkF2w5PaL7CMhd14ogPrtpX%2BFp9TXX8fbPmy10%2BeWNkX3W3NwT2jlXC1ETQIBTxPRnT%2BO0gJGpHoJdh0%2Bc5cicwGkKDTYxDI1QsFVbIiFXcaqAhDNiE3yIK9YQmFVbR1attpGClK7GREiUx8PGqgvkA3RQprOqDdOs%2BITz3SZc%2Bp9lE9H7dDN5ZUe3mFnAVAm74%2F8RMzoYLFElGwzDbMfuh1MNSPyLoGOp8BjqP4HzrWPc9qviDAyJ9VZABQX2BipNGoYqiaBi2HX6FTVc90B2qSkrhrMEkM81rKGMm3kH5imFf5%2F7SI0hThq2k2X8Il1xte49knjYdJC%2Bi2L1igvNWjKyeeq4AyuvwLwchLsz8piOYwrn4vQHLB3g5zz%2BCF3ZMLOmh9T8dUx08zeSNBvoHVQKaQDi1Emy1VvdH%2Bw8wuj5aWaIVqmFQX&X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=ASIAU6GD2XJEDX7HS46S%2F20241205%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20241205T200752Z&X-Amz-SignedHeaders=host&X-Amz-Signature=5c66e03a072b247ace9f4eb493822e340cba27396208bd981b44a3496beb37b3"
}
```
```JavaScript
GET https://mariusz96-aws-files.s3.amazonaws.com/a3ccf8ac-fed4-458c-a0b3-9d552f634403.jpg?X-Amz-Expires=3600&X-Amz-Security-Token=IQoJb3JpZ2luX2VjEGUaCXVzLWVhc3QtMSJGMEQCIHnBXGolUBPxA%2BLulz5%2FqqW75u6hLImQMHOkD01%2FEoVWAiA36Z5KWtPfcPkmFQwmqFglAb2DH08dHJy4AI1%2Ffz0hASrwAggdEAAaDDMzOTcxMzExMjY0OCIMPFXNZiaOafaYCdLCKs0CabT8dK%2B3gpIUunlATL5MP0MqS9vc3Vt%2FIUCaY36d3q4uY7AVVtywAADCp8CEmlQRCgok1dmqDnRuAETEpH%2BrMFgy73HWO6V9eWST0659hmkagWlfyoW4iQmUJnOq%2FQ%2FytzlrKBJ6xV3yyxs8QIyF0yeRUzBoc8kcu1S0tYC2ihu8Sxd1neRml5PpuE1ORD91hveyBUfujI1tEtUPQDrOsfpisHT78GMdWmFULkF2w5PaL7CMhd14ogPrtpX%2BFp9TXX8fbPmy10%2BeWNkX3W3NwT2jlXC1ETQIBTxPRnT%2BO0gJGpHoJdh0%2Bc5cicwGkKDTYxDI1QsFVbIiFXcaqAhDNiE3yIK9YQmFVbR1attpGClK7GREiUx8PGqgvkA3RQprOqDdOs%2BITz3SZc%2Bp9lE9H7dDN5ZUe3mFnAVAm74%2F8RMzoYLFElGwzDbMfuh1MNSPyLoGOp8BjqP4HzrWPc9qviDAyJ9VZABQX2BipNGoYqiaBi2HX6FTVc90B2qSkrhrMEkM81rKGMm3kH5imFf5%2F7SI0hThq2k2X8Il1xte49knjYdJC%2Bi2L1igvNWjKyeeq4AyuvwLwchLsz8piOYwrn4vQHLB3g5zz%2BCF3ZMLOmh9T8dUx08zeSNBvoHVQKaQDi1Emy1VvdH%2Bw8wuj5aWaIVqmFQX&X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=ASIAU6GD2XJEDX7HS46S%2F20241205%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20241205T200752Z&X-Amz-SignedHeaders=host&X-Amz-Signature=5c66e03a072b247ace9f4eb493822e340cba27396208bd981b44a3496beb37b3
```
```JavaScript
(binary)
```

## Prerequisites:
- .NET 8.0
- Visual Studio 2022
- AWS Toolkit with Amazon Q 1.60.0.1

## Test:
- `Test Explorer` > `Run All Tests`

## Deploy:
- Create a new S3 bucket with unique name and cofigure `BucketName` environment variable in `aws-lambda-tools-defaults.json` files
- Create a new DynamoDB table with `Files` table name and `Id` hash key name
- Publish `GetUploadUrl`, `ProcessUpload` and `GetDownloadUrl` functions to Lambda
- Add permisions to all functions to access other created resources
- Add trigger to `ProcessUpload` function on `All object create events` in the created S3 bucket
- Delete all created resources to prevent ongoing charges

## AWS services used:
- Lambda
- DynamoDB
- S3
