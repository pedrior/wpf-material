using WPF.Material.Environment;

namespace WPF.Material.Controls;

/// <summary>
/// Represents an icon button that can be toggled.
/// </summary>
public class ToggleIconButton : ToggleButtonBase
{
    /// <summary>
    /// Identifies the <see cref="Type"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
        nameof(Type),
        typeof(IconButtonType),
        typeof(ToggleIconButton),
        new PropertyMetadata(default(IconButtonType)));

    static ToggleIconButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(ToggleIconButton),
            new FrameworkPropertyMetadata(typeof(ToggleIconButton)));
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