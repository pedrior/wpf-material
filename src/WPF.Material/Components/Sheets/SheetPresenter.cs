using System.Windows.Markup;

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
        SetContainerBinding(sheet, Elevation.LevelProperty);
        
        SetContainerBinding(sheet, Container.ShapeFamilyProperty, Shape.FamilyProperty);
        SetContainerBinding(sheet, Container.ShapeStyleProperty, Shape.StyleProperty);
        SetContainerBinding(sheet, Container.ShapeCornerProperty, Shape.CornerProperty);
        SetContainerBinding(sheet, Container.ShapeRadiusProperty, Shape.RadiusProperty);
        SetContainerBinding(
            sheet,
            Container.UseStyleOnRadiusOverrideProperty,
            Shape.UseStyleOnRadiusOverrideProperty);
        
        SetContainerBinding(
            sheet,
            Container.UseCornersOnRadiusOverrideProperty,
            Shape.UseCornersOnRadiusOverrideProperty);
    }

    private void SetContainerBinding(object source, DependencyProperty targetDp, DependencyProperty? sourceDp = null) =>
        BindingOperations.SetBinding(container, targetDp, new Binding
        {
            Source = source,
            Path = new PropertyPath(sourceDp ?? targetDp)
        });

    private void ClearContainerBindings() => BindingOperations.ClearAllBindings(container);
}