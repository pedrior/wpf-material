using System.Windows.Documents;
using WPF.Material.Typography;
using Material_Typography_FontStyle = WPF.Material.Typography.FontStyle;

namespace WPF.Material.Assists;

/// <summary>
/// Provides attached properties for typography management.
/// </summary>
public static class TypographyAssist
{
    /// <summary>
    /// Identifies the Style attached property.
    /// </summary>
    public static readonly DependencyProperty StyleProperty = DependencyProperty.RegisterAttached(
        "Style",
        typeof(Material_Typography_FontStyle),
        typeof(TypographyAssist),
        new PropertyMetadata(default(Material_Typography_FontStyle), StyleChanged));

    /// <summary>
    /// Sets the value of the <see cref="StyleProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="StyleProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetStyle(DependencyObject element, Material_Typography_FontStyle value) =>
        element.SetValue(StyleProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="StyleProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="StyleProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="StyleProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static Material_Typography_FontStyle GetStyle(DependencyObject element) =>
        (Material_Typography_FontStyle)element.GetValue(StyleProperty);

    private static void StyleChanged(DependencyObject element, DependencyPropertyChangedEventArgs e)
    {
        var style = (Material_Typography_FontStyle)e.NewValue;

        if (style.ShouldUseLineHeight)
        {
            Block.SetLineHeight(element, style.LineHeight);
            Block.SetLineStackingStrategy(element, LineStackingStrategy.BlockLineHeight);
        }

        TextElement.SetFontSize(element, style.FontSize);
        TextElement.SetFontWeight(element, style.FontWeight);
        TextElement.SetFontFamily(element, Typefaces.GetTypefaceOrDefault(style.Typeface));
    }
}