using WPF.Material.Environment;

namespace WPF.Material.Controls;

/// <summary>
/// Represents a button control, which reacts to the <see cref="Button.Click"/> event.
/// </summary>
public class Button : System.Windows.Controls.Button
{
    /// <summary>
    /// Identifies the <see cref="Type"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
        nameof(Type),
        typeof(ButtonType),
        typeof(Button),
        new PropertyMetadata(ButtonType.Filled));

    static Button()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(Button),
            new FrameworkPropertyMetadata(typeof(Button)));
    }

    /// <summary>
    /// Gets or sets the type of the button.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public ButtonType Type
    {
        get => (ButtonType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }
}