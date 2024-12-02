using MiniValidation;
using System.Text;
using Xunit;

namespace AWSFiles.PostFile.Tests;

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
                Name = "filename.txt",
                Content = Encoding.UTF8.GetBytes("filecontent")
            },
            true
        },
        {
            new Request
            {
                Name = null!,
                Content = Encoding.UTF8.GetBytes("filecontent")
            },
            false
        },
        {
            new Request
            {
                Name = "",
                Content = Encoding.UTF8.GetBytes("filecontent")
            },
            false
        },
        {
            new Request
            {
                Name = "filename.txt",
                Content = null!
            },
            false
        },
        {
            new Request
            {
                Name = "filename.txt",
                Content = []
            },
            false
        },
        {
            new Request
            {
                Name = "largefile.json",
                Content = System.IO.File.ReadAllBytes("largefile.json") // 6MB
            },
            false
        }
    };
}
