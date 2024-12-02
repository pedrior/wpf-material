namespace WPF.Material.Components;

/// <summary>
/// Represents a button component, a control that responds to user click/tap events.
/// </summary>
public class Button : System.Windows.Controls.Button
{
    /// <summary>
    /// Identifies the <see cref="Appearance"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AppearanceProperty = DependencyProperty.Register(
        nameof(Appearance),
        typeof(ButtonAppearance),
        typeof(Button),
        new PropertyMetadata(ButtonAppearance.Filled));

    static Button()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(Button),
            new FrameworkPropertyMetadata(typeof(Button)));
    }

    /// <summary>
    /// Gets or sets the appearance of this button.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public ButtonAppearance Appearance
    {
        get => (ButtonAppearance)GetValue(AppearanceProperty);
        set => SetValue(AppearanceProperty, value);
    }
}