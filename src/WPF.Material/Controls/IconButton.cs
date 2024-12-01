using WPF.Material.Environment;

namespace WPF.Material.Controls;

/// <summary>
/// Represents a button control, which reacts to the <see cref="Button.Click"/> and displays only an icon.
/// </summary>
public class IconButton : System.Windows.Controls.Button
{
    /// <summary>
    /// Identifies the <see cref="Type"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
        nameof(Type),
        typeof(IconButtonType),
        typeof(IconButton),
        new PropertyMetadata(default(IconButtonType)));
    
    static IconButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(IconButton),
            new FrameworkPropertyMetadata(typeof(IconButton)));
    }

    /// <summary>
    /// Gets or sets the type of the icon button.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public IconButtonType Type
    {
        get => (IconButtonType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }
}