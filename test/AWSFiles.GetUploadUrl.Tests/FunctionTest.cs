using MiniValidation;
using Xunit;

namespace AWSFiles.GetUploadUrl.Tests;

public class FunctionTest
{
    [Theory]
    [MemberData(nameof(TestValidationData))]
    public void TestValidation(Request request, bool isValid)
    {
        Assert.Equal(isValid, MiniValidator.TryValidate(request, out _));
    }

    public static TheoryData<Request, bool> TestValidationData => new()
    {
        {
            new Request
            {
                Name = "photo.jpg"
            },
            true
        },
        {
            new Request
            {
                Name = null!
            },
            false
        },
        {
            new Request
            {
                Name = ""
            },
            false
        },
        {
            new Request
            {
                Name = "virus.exe"
            },
            false
        }
    };
}
