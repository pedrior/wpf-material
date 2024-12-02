using System.Windows.Documents;
using MaterialFontStyle = WPF.Material.Styles.FontStyle;

namespace WPF.Material.Styles;

/// <summary>
/// Provides attached properties for managing the typography aspects of a component.
/// </summary>
public static class Typography
{
    /// <summary>
    /// Identifies the Style attached property.
    /// </summary>
    public static readonly DependencyProperty StyleProperty = DependencyProperty.RegisterAttached(
        "Style",
        typeof(MaterialFontStyle),
        typeof(Typography),
        new PropertyMetadata(default(MaterialFontStyle), StyleChanged));

    /// <summary>
    /// Sets the value of the <see cref="StyleProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="StyleProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetStyle(DependencyObject element, MaterialFontStyle value) => 
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
    public static MaterialFontStyle GetStyle(DependencyObject element) => 
        (MaterialFontStyle)element.GetValue(StyleProperty);

    private static void StyleChanged(DependencyObject element, DependencyPropertyChangedEventArgs e)
    {
        var style = (MaterialFontStyle)e.NewValue;

        if (style.ShouldApplyLineHeight)
        {
            Block.SetLineHeight(element, style.LineHeight);
            Block.SetLineStackingStrategy(element, LineStackingStrategy.BlockLineHeight);
        }

        if (style.FontSize > 0.0)
        {
            TextElement.SetFontSize(element, style.FontSize);
        }

        TextElement.SetFontWeight(element, style.FontWeight);
        TextElement.SetFontFamily(element, Typefaces.GetTypefaceOrDefault(style.Typeface));
    }
}