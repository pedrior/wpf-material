namespace WPF.Material.Components;

/// <summary>
/// Represents a control that behaves like an <see cref="IconButton"/>, but can be toggled between two states. A
/// <see cref="ToggleIconButton"/> is typically used to represent a binary state, such as enabling or disabling a
/// feature or marking an item as a favorite.
/// </summary>
public class ToggleIconButton : System.Windows.Controls.Primitives.ToggleButton
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
    /// Gets or sets the type of the icon button, which determines its appearance and emphasis. The default value is
    /// <see cref="IconButtonType.Standard"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public IconButtonType Type
    {
        get => (IconButtonType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }
}