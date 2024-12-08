using System.Windows.Controls.Primitives;

namespace WPF.Material.Components;

/// <summary>
/// Represents a group of segmented toggle buttons that allows users to select one or multiple options.
/// </summary>
public class SegmentedButtons : ItemsControl
{
    /// <summary>
    /// Identifies the <see cref="RequireSelection"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty RequireSelectionProperty = DependencyProperty.Register(
        nameof(RequireSelection),
        typeof(bool),
        typeof(SegmentedButtons),
        new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="IsMultiSelect"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsMultiSelectProperty = DependencyProperty.Register(
        nameof(IsMultiSelect),
        typeof(bool),
        typeof(SegmentedButtons),
        new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="Spacing"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SpacingProperty = SpacedPanel.SpacingProperty.AddOwner(
        typeof(SegmentedButtons),
        new PropertyMetadata(0.0, null, CoerceSpacing));

    /// <summary>
    /// Identifies the <see cref="Orientation"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty OrientationProperty = SpacedPanel.OrientationProperty.AddOwner(
        typeof(SegmentedButtons),
        new PropertyMetadata(Orientation.Horizontal));

    /// <summary>
    /// Identifies the <see cref="IsUniformWidth"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsUniformWidthProperty = DependencyProperty.Register(
        nameof(IsUniformWidth),
        typeof(bool),
        typeof(SegmentedButtons),
        new PropertyMetadata(false));

    private static readonly DependencyPropertyKey HasSelectedItemsPropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(HasSelectedItems),
        typeof(bool),
        typeof(SegmentedButtons),
        new PropertyMetadata(false));
    
    /// <summary>
    /// Identifies the <see cref="HasSelectedItems"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty HasSelectedItemsProperty = HasSelectedItemsPropertyKey.DependencyProperty;
    
    internal static readonly DependencyProperty IsFirstItemProperty = DependencyProperty.RegisterAttached(
        "IsFirstItem",
        typeof(bool),
        typeof(SegmentedButtons),
        new PropertyMetadata(false));

    internal static readonly DependencyProperty IsLastItemProperty = DependencyProperty.RegisterAttached(
        "IsLastItem",
        typeof(bool),
        typeof(SegmentedButtons),
        new PropertyMetadata(false));

    internal static readonly DependencyProperty GroupOrientationProperty = DependencyProperty.RegisterAttached(
        "GroupOrientation",
        typeof(Orientation),
        typeof(SegmentedButtons),
        new PropertyMetadata(Orientation.Horizontal));

    private readonly HashSet<int> selectedIndices = new();

    /// <summary>
    /// Initializes static members of the <see cref="SegmentedButtons"/> class.
    /// </summary>
    static SegmentedButtons()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(SegmentedButtons),
            new FrameworkPropertyMetadata(typeof(SegmentedButtons)));
    }

    public SegmentedButtons()
    {
        ItemContainerGenerator.StatusChanged += ItemContainerGeneratorStatusChanged;
        ItemContainerGenerator.ItemsChanged += ItemContainerGeneratorItemsChanged;
    }

    /// <summary>
    /// Gets or sets a value that indicates whether the user must select an option.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Common)]
    public bool RequireSelection
    {
        get => (bool)GetValue(RequireSelectionProperty);
        set => SetValue(RequireSelectionProperty, value);
    }

    /// <summary>
    /// Gets or sets the selection mode of the toggle button group.
    /// </summary>
    /// <remarks>
    /// The selection mode <see cref="SelectionMode.Extended"/> is not supported.
    /// </remarks>
    [Bindable(true)]
    [Category(UICategory.Common)]
    public bool IsMultiSelect
    {
        get => (bool)GetValue(IsMultiSelectProperty);
        set => SetValue(IsMultiSelectProperty, value);
    }

    /// <summary>
    /// Gets or sets the amount of space between the toggle buttons.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Layout)]
    public double Spacing
    {
        get => (double)GetValue(SpacingProperty);
        set => SetValue(SpacingProperty, value);
    }

    /// <summary>
    /// Gets or sets the orientation of the toggle button group. 
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Layout)]
    public Orientation Orientation
    {
        get => (Orientation)GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }
    
    /// <summary>
    /// Gets or sets a value that indicates whether all toggle buttons should have a uniform width, given the widest
    /// button in the group.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Layout)]
    public bool IsUniformWidth
    {
        get => (bool)GetValue(IsUniformWidthProperty);
        set => SetValue(IsUniformWidthProperty, value);
    }
    
    /// <summary>
    /// Gets a value that indicates whether the group has selected items.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Common)]
    public bool HasSelectedItems
    {
        get => (bool)GetValue(HasSelectedItemsProperty);
        private set => SetValue(HasSelectedItemsPropertyKey, value);
    }
    
    /// <summary>
    /// Gets the indices of the selected items in the group.
    /// </summary>
    public IReadOnlySet<int> SelectedIndices => selectedIndices;

    private void ItemContainerGeneratorStatusChanged(object? sender, EventArgs e)
    {
        if (ItemContainerGenerator.Status is not GeneratorStatus.ContainersGenerated)
        {
            return;
        }
        
        InvalidateItemsSelection();
        InvalidateItemsGroupInformation();
    }

    private void ItemContainerGeneratorItemsChanged(object sender, ItemsChangedEventArgs e)
    {
        if (ItemContainerGenerator.Status is not GeneratorStatus.ContainersGenerated)
        {
            return;
        }
        
        InvalidateItemsSelection();
        InvalidateItemsGroupInformation();
    }

    internal void ToggleItem(SegmentedButtonItem item)
    {
        var index = ItemContainerGenerator.IndexFromContainer(item);
        if (index is -1)
        {
            return;
        }

        if (IsMultiSelect)
        {
            HandleToggleItemInMultiSelectMode(item, index);
        }
        else
        {
            HandleToggleItemInSingleSelectMode(item, index);
        }
    }

    private void HandleToggleItemInMultiSelectMode(SegmentedButtonItem item, int itemIndex)
    {
        if (IsItemSelected(itemIndex))
        {
            if (!RequireSelection || selectedIndices.Count is not 1)
            {
                SetItemSelected(item, itemIndex, false);
            }
        }
        else
        {
            SetItemSelected(item, itemIndex, true);
        }
    }
    
    private void HandleToggleItemInSingleSelectMode(SegmentedButtonItem item, int itemIndex)
    {
        if (IsItemSelected(itemIndex))
        {
            if (!RequireSelection)
            {
                SetItemSelected(item, itemIndex, false);
            }
        }
        else
        {
            if (selectedIndices.Count is 1)
            {
                SetItemSelected(selectedIndices.First(), false);
            }

            SetItemSelected(item, itemIndex, true);
        }
    }

    private bool IsItemSelected(int itemIndex) => selectedIndices.Contains(itemIndex);

    private void SetItemSelected(int itemIndex, bool isSelected)
    {
        if (ItemContainerGenerator.ContainerFromIndex(itemIndex) is SegmentedButtonItem item)
        {
            SetItemSelected(item, itemIndex, isSelected);
        }
    }

    private void SetItemSelected(SegmentedButtonItem item, int itemIndex, bool isSelected)
    {
        item.IsChecked = isSelected;
        if (isSelected)
        {
            selectedIndices.Add(itemIndex);
        }
        else
        {
            selectedIndices.Remove(itemIndex);
        }
        
        HasSelectedItems = selectedIndices.Count > 0;
    }
    
    private void InvalidateItemsSelection()
    {
        for (var i = 0; i < Items.Count; i++)
        {
            if (ItemContainerGenerator.ContainerFromIndex(i) is not SegmentedButtonItem { IsChecked: true } item)
            {
                continue;
            }

            if (IsMultiSelect)
            {
                selectedIndices.Add(i);
            }
            else
            {
                if (selectedIndices.Count is 1)
                {
                    SetItemSelected(selectedIndices.First(), false);
                }

                SetItemSelected(item, i, true);
            }
            
            HasSelectedItems = true;
        }
    }

    private void InvalidateItemsGroupInformation()
    {
        for (var i = 0; i < Items.Count; i++)
        {
            if (ItemContainerGenerator.ContainerFromIndex(i) is not SegmentedButtonItem item)
            {
                continue;
            }

            SetIsFirstItem(item, i is 0);
            SetIsLastItem(item, i == Items.Count - 1);
            SetGroupOrientation(item, Orientation);
            
            // For some reason, I'm not able to bind the attached properties in the style, so I'm doing it here.
            if (item.GetBindingExpression(SpacedPanel.StretchHorizontallyProperty) is null)
            {
                item.SetBinding(SpacedPanel.StretchHorizontallyProperty, new Binding
                {
                    Source = this,
                    Path = new PropertyPath(IsUniformWidthProperty),
                    Mode = BindingMode.OneWay
                });
            }
        }
    }

    internal static void SetIsFirstItem(DependencyObject element, bool value) =>
        element.SetValue(IsFirstItemProperty, value);

    internal static bool GetIsFirstItem(DependencyObject element) => (bool)element.GetValue(IsFirstItemProperty);

    internal static void SetIsLastItem(DependencyObject element, bool value) =>
        element.SetValue(IsLastItemProperty, value);

    internal static bool GetIsLastItem(DependencyObject element) => (bool)element.GetValue(IsLastItemProperty);

    internal static void SetGroupOrientation(DependencyObject element, Orientation value) =>
        element.SetValue(GroupOrientationProperty, value);

    internal static Orientation GetGroupOrientation(DependencyObject element) =>
        (Orientation)element.GetValue(GroupOrientationProperty);

    private static object CoerceSpacing(DependencyObject element, object value) => Math.Max(0.0, (double)value);
}