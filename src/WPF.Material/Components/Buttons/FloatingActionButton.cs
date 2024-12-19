namespace WPF.Material.Components;

/// <summary>
/// Represents a control that behaves like a <see cref="Button"/>, but is intended to float above the content to promote
/// the most common or important action on a screen, such as creating a new item, composing some content, or starting a
/// primary task.
/// </summary>
public class FloatingActionButton : System.Windows.Controls.Button
{
    /// <summary>
    /// Identifies the <see cref="Type"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
        nameof(Type),
        typeof(FloatingActionButtonType),
        typeof(FloatingActionButton),
        new PropertyMetadata(default(FloatingActionButtonType)));

    /// <summary>
    /// Identifies the <see cref="Size"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SizeProperty = DependencyProperty.Register(
        nameof(Size),
        typeof(FloatingActionButtonSize),
        typeof(FloatingActionButton),
        new PropertyMetadata(default(FloatingActionButtonSize)));

    /// <summary>
    /// Identifies the <see cref="IsExtended"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsExtendedProperty = DependencyProperty.Register(
        nameof(IsExtended),
        typeof(bool),
        typeof(FloatingActionButton),
        new PropertyMetadata(false));

    static FloatingActionButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(FloatingActionButton),
            new FrameworkPropertyMetadata(typeof(FloatingActionButton)));
    }

    /// <summary>
    /// Gets or sets the type of the FAB, which determines its appearance and emphasis. The default value is
    /// <see cref="FloatingActionButtonType.Surface"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public FloatingActionButtonType Type
    {
        get => (FloatingActionButtonType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    /// <summary>
    /// Gets or sets the size of the FAB. The default value is <see cref="FloatingActionButtonSize.Standard"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Layout)]
    public FloatingActionButtonSize Size
    {
        get => (FloatingActionButtonSize)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the FAB is extended with a label. An extended FAB does not support
    /// specifying a <see cref="Size"/>. The default value is <see langword="false"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Common)]
    public bool IsExtended
    {
        get => (bool)GetValue(IsExtendedProperty);
        set => SetValue(IsExtendedProperty, value);
    }
}