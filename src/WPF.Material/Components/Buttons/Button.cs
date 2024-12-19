namespace WPF.Material.Components;

/// <summary>
/// Represents a control that reacts to user clicks. A <see cref="Button"/> is typically used to initiate actions, such
/// as sending an email, saving a document, or submitting a form.
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
    /// Gets or sets the type of the button, which determines its appearance and emphasis. The default value is
    /// <see cref="ButtonType.Filled"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public ButtonType Type
    {
        get => (ButtonType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }
}