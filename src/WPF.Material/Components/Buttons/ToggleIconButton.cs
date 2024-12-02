namespace WPF.Material.Components;

/// <summary>
/// Represents an icon button that can be toggled.
/// </summary>
public class ToggleIconButton : System.Windows.Controls.Primitives.ToggleButton
{
    /// <summary>
    /// Identifies the <see cref="Appearance"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AppearanceProperty = DependencyProperty.Register(
        nameof(Appearance),
        typeof(IconButtonAppearance),
        typeof(ToggleIconButton),
        new PropertyMetadata(default(IconButtonAppearance)));

    static ToggleIconButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(ToggleIconButton),
            new FrameworkPropertyMetadata(typeof(ToggleIconButton)));
        
        IsThreeStateProperty.OverrideMetadata(
            typeof(ToggleIconButton),
            new FrameworkPropertyMetadata(false, null, CoerceIsThreeState));
    }

    /// <summary>
    /// Gets or sets the type of the icon button.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public IconButtonAppearance Appearance
    {
        get => (IconButtonAppearance)GetValue(AppearanceProperty);
        set => SetValue(AppearanceProperty, value);
    }

    private static object CoerceIsThreeState(DependencyObject element, object value) => false;
}