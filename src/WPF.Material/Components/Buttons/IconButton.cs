namespace WPF.Material.Components;

/// <summary>
/// Represents a control that behaves like a <see cref="Button"/>, but is intended to only display an icon that
/// represents the action it performs. An <see cref="IconButton"/> is typically used to initiate optional supplementary
/// actions such as bookmarking an item, deleting a document, or refreshing a page, considering that the icon provides
/// enough meaning to the user.
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
        new PropertyMetadata(IconButtonType.Standard));

    static IconButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(IconButton),
            new FrameworkPropertyMetadata(typeof(IconButton)));
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