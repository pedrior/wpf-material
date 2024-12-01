using System.Diagnostics.Contracts;
// ReSharper disable All

internal static class Resources
{
    [Pure]
    public static Uri PackUri(string relativePath) => 
        new($"pack://application:,,,/WPF.Material;component/{relativePath}", UriKind.Absolute);
}