using System.Windows.Markup;
using WPF.Material.Styles;

namespace WPF.Material.Components;

/// <summary>
/// Displays a <see cref="Sheet"/> element.
/// </summary>
[ContentProperty(nameof(Sheet))]
public class SheetPresenter : FrameworkElement
{
    private readonly NaivePanel panel;
    private readonly Container container;

    private DependencyProperty? currentTargetSizeProperty;

    /// <summary>
    /// Initializes a new instance of <see cref="SheetPresenter"/>.
    /// </summary>
    public SheetPresenter()
    {
        panel = new NaivePanel();
        container = new Container
        {
            Background = null,
            BorderBrush = null,
            BorderThickness = default
        };

        panel.Children.Add(container);

        AddVisualChild(panel);
    }

    /// <summary>
    /// Gets or sets the <see cref="Sheet"/> to display.
    /// </summary>
    [Bindable(false)]
    [Category(UICategory.Common)]
    public Sheet? Sheet
    {
        get => (Sheet?)container.Content;
        set => SetSheet(value);
    }

    protected override int VisualChildrenCount => 1;

    protected override Visual GetVisualChild(int _) => panel;

    protected override Size MeasureOverride(Size constraints)
    {
        panel.Measure(constraints);
        return panel.DesiredSize;
    }

    protected override Size ArrangeOverride(Size constraints)
    {
        panel.Arrange(new Rect(constraints));
        return constraints;
    }

    internal Container GetContainer() => container;

    private void SetSheet(Sheet? sheet)
    {
        if (Sheet is { } sheetBeingRemoved)
        {
            DetachRemovedSheet(sheetBeingRemoved);
        }

        ResetHostState(sheet);

        if (sheet is null)
        {
            container.Content = null;
        }
        else
        {
            container.Content = sheet;
            sheet.SetCurrentHost(this);

            CreateContainerBindings(sheet);

            currentTargetSizeProperty = sheet.TargetSizeProperty;
        }
    }

    private void DetachRemovedSheet(Sheet sheetBeingRemoved)
    {
        ClearContainerBindings();

        sheetBeingRemoved.RemoveCurrentHost();
    }

    private void ResetHostState(Sheet? newSheet)
    {
        container.SetAnimatedPropertyValue(
            newSheet?.TargetSizeProperty ?? currentTargetSizeProperty,
            0.0);
    }

    private void CreateContainerBindings(Sheet sheet)
    {
        SetContainerBinding(sheet, HorizontalAlignmentProperty);
        SetContainerBinding(sheet, Control.BackgroundProperty);
        SetContainerBinding(sheet, Shape.FamilyProperty);
        SetContainerBinding(sheet, Shape.StyleProperty);
        SetContainerBinding(sheet, Shape.CornerProperty);
        SetContainerBinding(sheet, Shape.RadiusProperty);
        SetContainerBinding(sheet, Shape.UseStyleOnRadiusOverrideProperty);
        SetContainerBinding(sheet, Shape.UseCornersOnRadiusOverrideProperty);
        SetContainerBinding(sheet, Elevation.LevelProperty);
    }

    private void SetContainerBinding(object source, DependencyProperty property) =>
        BindingOperations.SetBinding(container, property, new Binding
        {
            Source = source,
            Path = new PropertyPath(property)
        });

    private void ClearContainerBindings() => BindingOperations.ClearAllBindings(container);
}