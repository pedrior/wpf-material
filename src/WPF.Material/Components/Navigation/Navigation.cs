using System.Windows.Controls.Primitives;

namespace WPF.Material.Components;

/// <summary>
/// Provides a base class for navigation elements.
/// </summary>
public abstract class Navigation : ItemsControl
{
    private static readonly DependencyPropertyKey SelectedIndexPropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(SelectedIndex),
        typeof(int),
        typeof(Navigation),
        new PropertyMetadata(-1));
    
    /// <summary>
    /// Identifies the <see cref="SelectedIndex"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SelectedIndexProperty = SelectedIndexPropertyKey.DependencyProperty;

    private static readonly DependencyPropertyKey SelectedItemPropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(SelectedItem),
        typeof(NavigationRailItem),
        typeof(Navigation),
        new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="SelectedItem"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SelectedItemProperty = SelectedItemPropertyKey.DependencyProperty;

    /// <summary>
    /// Identifies the <see cref="SelectionChanged"/> routed event.
    /// </summary>
    public static readonly RoutedEvent SelectionChangedEvent = EventManager.RegisterRoutedEvent(
        nameof(SelectionChanged),
        RoutingStrategy.Bubble,
        typeof(SelectionChangedEventHandler),
        typeof(Navigation));

    private int lastSelectedIndex = -1;
    private NavigationRailItem? lazySelectedItem;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="NavigationRail"/> class.
    /// </summary>
    protected Navigation()
    {
        Items.CurrentChanged += OnCurrentChanged;
        ItemContainerGenerator.StatusChanged += OnStatusChanged;
    }
    
    /// <summary>
    /// Occurs when the selected item changes.
    /// </summary>
    public event SelectionChangedEventHandler SelectionChanged
    {
        add => AddHandler(SelectionChangedEvent, value);
        remove => RemoveHandler(SelectionChangedEvent, value);
    }
    
    /// <summary>
    /// Gets the currently selected item index.
    /// </summary>
    [Category(UICategory.Common)]
    public int SelectedIndex
    {
        get => (int)GetValue(SelectedIndexProperty);
        private set => SetValue(SelectedIndexPropertyKey, value);
    }

    /// <summary>
    /// Gets the currently selected item.
    /// </summary>
    [Category(UICategory.Common)]
    public NavigationRailItem? SelectedItem
    {
        get => (NavigationRailItem?)GetValue(SelectedItemProperty);
        private set => SetValue(SelectedItemPropertyKey, value);
    }

    internal void SetItemIsSelected(NavigationRailItem? item)
    {
        if (ReferenceEquals(Items.CurrentItem, item))
        {
            return;
        }

        switch (ItemContainerGenerator.Status)
        {
            case GeneratorStatus.ContainersGenerated:
                Items.MoveCurrentTo(item);
                break;
            case GeneratorStatus.NotStarted or GeneratorStatus.GeneratingContainers:
                lazySelectedItem = item;
                break;
        }
    }

    private void OnStatusChanged(object? sender, EventArgs e)
    {
        if (lazySelectedItem is null || ItemContainerGenerator.Status is not GeneratorStatus.ContainersGenerated)
        {
            return;
        }

        Items.MoveCurrentTo(lazySelectedItem);
        lazySelectedItem = null;
    }

    private void OnCurrentChanged(object? sender, EventArgs e)
    {
        SelectedIndex = Items.CurrentPosition;
        SelectedItem = (NavigationRailItem)Items.CurrentItem;

        RaiseEvent(new SelectionChangedEventArgs(
            SelectionChangedEvent,
            removedItems: lastSelectedIndex is -1
                ? Array.Empty<object>()
                : new[] { Items.GetItemAt(lastSelectedIndex) },
            addedItems: new[] { Items.CurrentItem }));

        lastSelectedIndex = Items.CurrentPosition;
    }
}