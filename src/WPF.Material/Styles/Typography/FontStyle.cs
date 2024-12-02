namespace WPF.Material.Styles;

/// <summary>
/// Represents a typographic style properties that can be applied to a component.
/// </summary>
/// <param name="Typeface">The font family to use.</param>
/// <param name="FontSize">The size of the font.</param>
/// <param name="LineHeight">
/// The height of the line. Use 0 to indicate that the line height should be disregarded.
/// </param>
/// <param name="FontWeight">The weight of the font.</param>
public readonly record struct FontStyle(
    string Typeface,
    double FontSize,
    double LineHeight,
    FontWeight FontWeight)
{
    internal bool ShouldApplyLineHeight => LineHeight > 0.0;

    internal FontStyle With(
        string? typeface = null,
        double? fontSize = null,
        double? lineHeight = null,
        FontWeight? fontWeight = null)
    {
        return new FontStyle(Typeface: typeface ?? Typeface,
            FontSize: fontSize ?? FontSize,
            FontWeight: fontWeight ?? FontWeight,
            LineHeight: lineHeight ?? LineHeight);
    }
}