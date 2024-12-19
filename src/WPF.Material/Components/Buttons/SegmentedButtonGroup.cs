using System.Windows.Controls.Primitives;

namespace WPF.Material.Components;

/// <summary>
/// Represents a group of <see cref="SegmentedButton"/>s arranged either horizontally or vertically, allowing selecting
/// one or more buttons based on the selection mode.
/// </summary>
public class SegmentedButtonGroup : ItemsControl
{
    /// <summary>
    /// Identifies the <see cref="RequireSelection"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty RequireSelectionProperty = DependencyProperty.Register(
        nameof(RequireSelection),
        typeof(bool),
        typeof(SegmentedButtonGroup),
        new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="IsMultiSelect"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsMultiSelectProperty = DependencyProperty.Register(
        nameof(IsMultiSelect),
        typeof(bool),
        typeof(SegmentedButtonGroup),
        new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="Spacing"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SpacingProperty = SpacedPanel.SpacingProperty.AddOwner(
        typeof(SegmentedButtonGroup),
        new PropertyMetadata(0.0, null, CoerceSpacing));

    /// <summary>
    /// Identifies the <see cref="Orientation"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty OrientationProperty = SpacedPanel.OrientationProperty.AddOwner(
        typeof(SegmentedButtonGroup),
        new PropertyMetadata(Orientation.Horizontal));

    /// <summary>
    /// Identifies the <see cref="IsUniformWidth"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsUniformWidthProperty = DependencyProperty.Register(
        nameof(IsUniformWidth),
        typeof(bool),
        typeof(SegmentedButtonGroup),
        new PropertyMetadata(false));

    private static readonly DependencyPropertyKey HasSelectedItemsPropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(HasSelectedButtons),
        typeof(bool),
        typeof(SegmentedButtonGroup),
        new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="HasSelectedButtons"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty HasSelectedButtonsProperty = HasSelectedItemsPropertyKey.DependencyProperty;

    internal static readonly DependencyProperty IsFirstItemProperty = DependencyProperty.RegisterAttached(
        "IsFirstItem",
        typeof(bool),
        typeof(SegmentedButtonGroup),
        new PropertyMetadata(false));

    internal static readonly DependencyProperty IsLastItemProperty = DependencyProperty.RegisterAttached(
        "IsLastItem",
        typeof(bool),
        typeof(SegmentedButtonGroup),
        new PropertyMetadata(false));

    internal static readonly DependencyProperty GroupOrientationProperty = DependencyProperty.RegisterAttached(
        "GroupOrientation",
        typeof(Orientation),
        typeof(SegmentedButtonGroup),
        new PropertyMetadata(Orientation.Horizontal));

    private readonly HashSet<int> selectedButtonIndices = new();

    /// <summary>
    /// Initializes static members of the <see cref="SegmentedButtonGroup"/> class.
    /// </summary>
    static SegmentedButtonGroup()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(SegmentedButtonGroup),
            new FrameworkPropertyMetadata(typeof(SegmentedButtonGroup)));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SegmentedButtonGroup"/> class.
    /// </summary>
    public SegmentedButtonGroup()
    {
        ItemContainerGenerator.StatusChanged += ItemContainerGeneratorStatusChanged;
        ItemContainerGenerator.ItemsChanged += ItemContainerGeneratorItemsChanged;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the group requires at least one button to be selected. The default
    /// value is <see langword="false"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Common)]
    
    public bool RequireSelection
    {
        get => (bool)GetValue(RequireSelectionProperty);
        set => SetValue(RequireSelectionProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the group allows multiple buttons to be selected. The default value is
    /// <see langword="false"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Common)]
    public bool IsMultiSelect
    {
        get => (bool)GetValue(IsMultiSelectProperty);
        set => SetValue(IsMultiSelectProperty, value);
    }

    /// <summary>
    /// Gets or sets the amount of space between the buttons in the group.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Layout)]
    public double Spacing
    {
        get => (double)GetValue(SpacingProperty);
        set => SetValue(SpacingProperty, value);
    }

    /// <summary>
    /// Gets or sets the orientation of the group.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Layout)]
    public Orientation Orientation
    {
        get => (Orientation)GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the buttons in the group should have a uniform width based on the
    /// widest button in the group.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Layout)]
    public bool IsUniformWidth
    {
        get => (bool)GetValue(IsUniformWidthProperty);
        set => SetValue(IsUniformWidthProperty, value);
    }

    /// <summary>
    /// Gets a value indicating whether the group has at least one selected button.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Common)]
    public bool HasSelectedButtons
    {
        get => (bool)GetValue(HasSelectedButtonsProperty);
        private set => SetValue(HasSelectedItemsPropertyKey, value);
    }

    /// <summary>
    /// Gets the indices of the selected buttons in the group.
    /// </summary>
    public IReadOnlySet<int> SelectedButtonIndices => selectedButtonIndices;

    private void ItemContainerGeneratorStatusChanged(object? sender, EventArgs e)
    {
        if (ItemContainerGenerator.Status is GeneratorStatus.ContainersGenerated)
        {
            InvalidateGroupInformation();
        }
    }

    private void ItemContainerGeneratorItemsChanged(object sender, ItemsChangedEventArgs e)
    {
        if (ItemContainerGenerator.Status is GeneratorStatus.ContainersGenerated)
        {
            InvalidateGroupInformation();
        }
    }

    internal void Select(SegmentedButton button)
    {
        var index = ItemContainerGenerator.IndexFromContainer(button);
        if (index is -1)
        {
            return;
        }

        if (IsMultiSelect)
        {
            SelectInMultiSelectMode(button, index);
        }
        else
        {
            SelectInSingleSelectMode(button, index);
        }
    }

    private void SelectInMultiSelectMode(SegmentedButton button, int buttonIndex)
    {
        if (IsButtonSelected(buttonIndex))
        {
            if (!RequireSelection || selectedButtonIndices.Count is not 1)
            {
                SetButtonSelected(button, buttonIndex, false);
            }
        }
        else
        {
            SetButtonSelected(button, buttonIndex, true);
        }
    }

    private void SelectInSingleSelectMode(SegmentedButton button, int buttonIndex)
    {
        if (IsButtonSelected(buttonIndex))
        {
            if (!RequireSelection)
            {
                SetButtonSelected(button, buttonIndex, false);
            }
        }
        else
        {
            if (selectedButtonIndices.Count is 1)
            {
                SetButtonSelected(selectedButtonIndices.First(), false);
            }

            SetButtonSelected(button, buttonIndex, true);
        }
    }

    private bool IsButtonSelected(int buttonIndex) => selectedButtonIndices.Contains(buttonIndex);

    private void SetButtonSelected(int buttonIndex, bool isSelected)
    {
        if (ItemContainerGenerator.ContainerFromIndex(buttonIndex) is SegmentedButton item)
        {
            SetButtonSelected(item, buttonIndex, isSelected);
        }
    }

    private void SetButtonSelected(SegmentedButton button, int buttonIndex, bool isSelected)
    {
        button.IsChecked = isSelected;
        if (isSelected)
        {
            selectedButtonIndices.Add(buttonIndex);
        }
        else
        {
            selectedButtonIndices.Remove(buttonIndex);
        }

        HasSelectedButtons = selectedButtonIndices.Count > 0;
    }

    private void InvalidateGroupInformation()
    {
        for (var i = 0; i < Items.Count; i++)
        {
            if (ItemContainerGenerator.ContainerFromIndex(i) is not SegmentedButton button)
            {
                continue;
            }

            SetIsFirstItem(button, i is 0);
            SetIsLastItem(button, i == Items.Count - 1);
            SetGroupOrientation(button, Orientation);

            // For some reason, I'm not able to bind the attached properties in the style, so I'm doing it here.
            if (button.GetBindingExpression(SpacedPanel.StretchHorizontallyProperty) is null)
            {
                button.SetBinding(SpacedPanel.StretchHorizontallyProperty, new Binding
                {
                    Source = this,
                    Path = new PropertyPath(IsUniformWidthProperty),
                    Mode = BindingMode.OneWay
                });
            }

            if (button.IsChecked is not true)
            {
                continue;
            }

            if (IsMultiSelect)
            {
                selectedButtonIndices.Add(i);
            }
            else
            {
                if (selectedButtonIndices.Count is 1)
                {
                    SetButtonSelected(selectedButtonIndices.First(), false);
                }

                SetButtonSelected(button, i, true);
            }

            HasSelectedButtons = true;
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