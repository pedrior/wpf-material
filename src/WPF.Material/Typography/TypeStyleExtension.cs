using System.Windows.Markup;

namespace WPF.Material.Typography;

/// <summary>
/// A markup extension that provides a <see cref="FontStyle"/> based on a <see cref="TypeStyle"/>, with optional style
/// overrides for typeface, size, height and weight.
/// </summary>
/// <remarks>
/// This may not work well with XAML Preview in JetBrains Rider (up to 2024.2.5), but it works fine in Visual Studio
/// (tested in VS2022).
/// </remarks>
[MarkupExtensionReturnType(typeof(FontStyle))]
public sealed class TypeStyleExtension : MarkupExtension
{
    /// <summary>
    /// Gets or sets the type style to use. Default is <see cref="TypeStyle.BodyMedium"/>.
    /// </summary>
    public TypeStyle Style { get; init; } = TypeStyle.BodyMedium;

    /// <summary>
    /// Overrides the style's typeface.
    /// </summary>
    public string? Typeface { get; init; }

    /// <summary>
    /// Overrides the style's font size.
    /// </summary>
    public int? Size { get; init; }

    /// <summary>
    /// Overrides the style's line height. Set to 0 to indicate no line height.
    /// </summary>
    public double? Height { get; init; }

    /// <summary>
    /// Overrides the style's font weight.
    /// </summary>
    public FontWeight? Weight { get; set; }

    /// <inheritdoc />
    public override object ProvideValue(IServiceProvider _) => TypeScale.GetFontStyle(Style)
        .With(Typeface, Size, Height, Weight);
}