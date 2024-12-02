namespace WPF.Material.Components;

/// <summary>
/// Represents a floating action button (FAB) component. The FAB behaves like a normal <see cref="Button"/>, but is
/// generally used for primary actions in the UI.
/// </summary>
public class FloatingActionButton : System.Windows.Controls.Button
{
    /// <summary>
    /// Identifies the <see cref="Appearance"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AppearanceProperty = DependencyProperty.Register(
        nameof(Appearance),
        typeof(FloatingActionButtonAppearance),
        typeof(FloatingActionButton),
        new PropertyMetadata(default(FloatingActionButtonAppearance)));

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
    /// Gets or sets the appearance of this FAB.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public FloatingActionButtonAppearance Appearance
    {
        get => (FloatingActionButtonAppearance)GetValue(AppearanceProperty);
        set => SetValue(AppearanceProperty, value);
    }

    /// <summary>
    /// Gets or sets the size of this FAB.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Layout)]
    public FloatingActionButtonSize Size
    {
        get => (FloatingActionButtonSize)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether this FAB should be extended with a label.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public bool IsExtended
    {
        get => (bool)GetValue(IsExtendedProperty);
        set => SetValue(IsExtendedProperty, value);
    }
}