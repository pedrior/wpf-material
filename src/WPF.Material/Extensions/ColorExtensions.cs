using System.Runtime.CompilerServices;

namespace WPF.Material.Extensions;

/// <summary>
/// Provides a set of extension methods for <see cref="Color"/>.
/// </summary>
public static class ColorExtensions
{
    /// <summary>
    /// Converts a ARGB/RGB hex value to a <see cref="Color"/> Structure.
    /// </summary>
    /// <param name="hex">The color in hex format.</param>
    /// <param name="hasAlpha">A value indicating whether the color has an alpha channel.</param>
    /// <returns>A <see cref="Color"/> structure that represents the converted hex value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color ToColor(this int hex, bool hasAlpha = false)
    {
        var a = (byte)(hasAlpha ? hex >> 24 & 255 : 255);
        var r = (byte)(hex >> 16 & 255);
        var g = (byte)(hex >> 8 & 255);
        var b = (byte)(hex & 255);

        return Color.FromArgb(a, r, g, b);
    }

    internal static SolidColorBrush ToBrush(this Color color, bool freeze = true)
    {
        var brush = new SolidColorBrush(color);
        if (freeze)
        {
            brush.Freeze();
        }

        return brush;
    }
}