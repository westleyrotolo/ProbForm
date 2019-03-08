using System;
namespace ProbForm.Models
{
    public static class ToTitleExtension
    {
        public static string ToTitleCase(this string value)
        {
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
        }
    }
}
