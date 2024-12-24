namespace WPF.Material.Components;

/// <summary>
/// Represents a compact navigation control that provides a way to navigate between different destinations.
/// </summary>
public class NavigationRail : ItemsControl
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NavigationRail"/> class.
    /// </summary>
    public static readonly DependencyProperty TopActionsProperty = DependencyProperty.Register(
        nameof(TopActions),
        typeof(NavigationRailActions),
        typeof(NavigationRail),
        new PropertyMetadata(null));

    /// <summary>
    /// Initializes a new instance of the <see cref="TopActionsTemplate"/> class.
    /// </summary>
    public static readonly DependencyProperty TopActionsTemplateProperty = DependencyProperty.Register(
        nameof(TopActionsTemplate),
        typeof(DataTemplate),
        typeof(NavigationRail),
        new PropertyMetadata(null));
    
    /// <summary>
    /// Identifies the <see cref="BottomActionsContent"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty BottomActionsContentProperty = DependencyProperty.Register(
        nameof(BottomActionsContent),
        typeof(NavigationRailActions),
        typeof(NavigationRail),
        new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="BottomActionsTemplate"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty BottomActionsTemplateProperty = DependencyProperty.Register(
        nameof(BottomActionsTemplate),
        typeof(DataTemplate),
        typeof(NavigationRail),
        new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Alignment"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AlignmentProperty = DependencyProperty.Register(
        nameof(Alignment),
        typeof(NavigationRailAlignment),
        typeof(NavigationRail),
        new PropertyMetadata(NavigationRailAlignment.Top));

    /// <summary>
    /// Identifies the <see cref="ShowDivider"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ShowDividerProperty = DependencyProperty.Register(
        nameof(ShowDivider),
        typeof(bool),
        typeof(NavigationRail),
        new PropertyMetadata(false));
    
    /// <summary>
    /// Identifies the <see cref="DividerForeground"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty DividerForegroundProperty = DependencyProperty.Register(
        nameof(DividerForeground),
        typeof(Brush),
        typeof(NavigationRail),
        new PropertyMetadata(Brushes.Black));
    
    private static readonly DependencyPropertyKey SelectedIndexPropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(SelectedIndex),
        typeof(int),
        typeof(NavigationRail),
        new PropertyMetadata(-1));
    
    /// <summary>
    /// Identifies the <see cref="SelectedIndex"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SelectedIndexProperty = SelectedIndexPropertyKey.DependencyProperty;

    static NavigationRail()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(NavigationRail),
            new FrameworkPropertyMetadata(typeof(NavigationRail)));
    }

    /// <summary>
    /// Gets or sets the top actions of the navigation rail. The default value is <see langword="null"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Content)]
    public NavigationRailActions? TopActions
    {
        get => (NavigationRailActions?)GetValue(TopActionsProperty);
        set => SetValue(TopActionsProperty, value);
    }

    /// <summary>
    /// Gets or sets the template used to display the top actions content. The default value is <see langword="null"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Content)]
    public DataTemplate? TopActionsTemplate
    {
        get => (DataTemplate?)GetValue(TopActionsTemplateProperty);
        set => SetValue(TopActionsTemplateProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the bottom actions of the navigation rail. The default value is <see langword="null"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Content)]
    public NavigationRailActions? BottomActionsContent
    {
        get => (NavigationRailActions?)GetValue(BottomActionsContentProperty);
        set => SetValue(BottomActionsContentProperty, value);
    }

    /// <summary>
    /// Gets or sets the template used to display the bottom actions content. The default value is
    /// <see langword="null"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Content)]
    public DataTemplate? BottomActionsTemplate
    {
        get => (DataTemplate?)GetValue(BottomActionsTemplateProperty);
        set => SetValue(BottomActionsTemplateProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the alignment of the destination items in the navigation rail. The default value is
    /// <see cref="NavigationRailAlignment.Top"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Layout)]
    public NavigationRailAlignment Alignment
    {
        get => (NavigationRailAlignment)GetValue(AlignmentProperty);
        set => SetValue(AlignmentProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the vertical divider should be shown. The default value is
    /// <see langword="false"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public bool ShowDivider
    {
        get => (bool)GetValue(ShowDividerProperty);
        set => SetValue(ShowDividerProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="Brush"/> that paints the vertical divider. The default value is
    /// <see langword="Brushes.Black"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Brush)]
    public Brush? DividerForeground
    {
        get => (Brush?)GetValue(DividerForegroundProperty);
        set => SetValue(DividerForegroundProperty, value);
    }
    
    /// <summary>
    /// Gets the index of the current selected destination item. It returns -1 if no destination is currently selected.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Common)]
    public int SelectedIndex
    {
        get => (int)GetValue(SelectedIndexProperty);
        private set => SetValue(SelectedIndexPropertyKey, value);
    }
}