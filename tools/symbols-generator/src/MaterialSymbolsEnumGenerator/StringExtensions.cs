using System.Globalization;

namespace MaterialSymbolsEnumGenerator;

public static class StringExtensions
{
    public static string ToMemberName(this string str)
    {
        str = str.Replace('_', ' ');

        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str)
            .Replace(" ", string.Empty)
            .AppendUnderscoreIfDigit();
    }

    private static string AppendUnderscoreIfDigit(this string str) =>
        char.IsDigit(str[0]) ? "_" + str : str;
}