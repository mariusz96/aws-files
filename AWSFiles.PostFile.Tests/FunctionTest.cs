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
                Content = Encoding.UTF8.GetBytes("test")
            },
            true
        },
        {
            new Request
            {
                Name = null!,
                Content = Encoding.UTF8.GetBytes("test")
            },
            false
        },
        {
            new Request
            {
                Name = "",
                Content = Encoding.UTF8.GetBytes("test")
            },
            false
        },
        {
            new Request
            {
                Name = "filenamewithoutextension",
                Content = Encoding.UTF8.GetBytes("test")
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
                Name = "testdata.json",
                Content = System.IO.File.ReadAllBytes("testdata.json") // 6MB
            },
            false
        }
    };
}
