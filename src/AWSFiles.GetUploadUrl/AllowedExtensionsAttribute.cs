using System.ComponentModel.DataAnnotations;

namespace AWSFiles.GetUploadUrl;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class AllowedExtensionsAttribute : ValidationAttribute
{
    public AllowedExtensionsAttribute(params string?[] extensions)
    {
        ArgumentNullException.ThrowIfNull(extensions);
        Extensions = extensions;
        ErrorMessage = "The {0} field extension does not equal any of the values specified in AllowedExtensionsAttribute.";
    }

    public string?[] Extensions { get; }

    public override bool IsValid(object? value)
    {
        return value switch
        {
            null => Extensions.Contains(null),
            string name => Extensions.Contains(Path.GetExtension(name)),
            _ => false
        };
    }
}
