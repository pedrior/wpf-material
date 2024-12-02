namespace WPF.Material.Components;

/// <summary>
/// Represents a button control, which reacts to the <see cref="Button.Click"/> and displays only an icon.
/// </summary>
public class IconButton : System.Windows.Controls.Button
{
    /// <summary>
    /// Identifies the <see cref="Appearance"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AppearanceProperty = DependencyProperty.Register(
        nameof(Appearance),
        typeof(IconButtonAppearance),
        typeof(IconButton),
        new PropertyMetadata(default(IconButtonAppearance)));
    
    static IconButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(IconButton),
            new FrameworkPropertyMetadata(typeof(IconButton)));
    }

    /// <summary>
    /// Gets or sets the appearance of this icon button.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public IconButtonAppearance Appearance
    {
        get => (IconButtonAppearance)GetValue(AppearanceProperty);
        set => SetValue(AppearanceProperty, value);
    }
}