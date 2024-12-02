using System.ComponentModel.DataAnnotations;

namespace AWSFiles.PostFile;

public class FileNameAttribute : ValidationAttribute
{
    private static readonly char[] InvalidFileNameChars = Path.GetInvalidFileNameChars();

    public FileNameAttribute()
        : base(() => "The {0} field is not a valid file name.")
    {
    }

    public override bool IsValid(object? value)
    {
        // Required attribute should be used to assert a value is not null.
        if (value is null)
        {
            return true;
        }

        if (value is not string s)
        {
            return false;
        }

        return s.All(c => !InvalidFileNameChars.Contains(c));
    }
}
