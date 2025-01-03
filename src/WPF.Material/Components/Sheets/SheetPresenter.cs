﻿using System.Windows.Markup;

namespace WPF.Material.Components;

/// <summary>
/// Hosts a <see cref="Sheet"/> component.
/// </summary>
[ContentProperty(nameof(Sheet))]
public class SheetPresenter : FrameworkElement
{
    /// <summary>
    /// Identifies the <see cref="ScrimBrush"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ScrimBrushProperty = DependencyProperty.Register(
        nameof(ScrimBrush),
        typeof(Brush),
        typeof(SheetPresenter),
        new PropertyMetadata(Brushes.Black));

    private const string ScrimResourceKey = "Material.Colors.Scrim";
    
    private readonly NaivePanel panel;
    private readonly System.Windows.Shapes.Rectangle overlay;
    private readonly Container container;

    private DependencyProperty? currentTargetSizeProperty;

    /// <summary>
    /// Initializes a new instance of <see cref="SheetPresenter"/>.
    /// </summary>
    public SheetPresenter()
    {
        SetResourceReference(ScrimBrushProperty, ScrimResourceKey);
        
        overlay = new System.Windows.Shapes.Rectangle
        {
            Opacity = 0.0,
            IsHitTestVisible = false
        };
        
        overlay.SetBinding(System.Windows.Shapes.Shape.FillProperty, new Binding
        {
            Source = this,
            Path = new PropertyPath(ScrimBrushProperty)
        });
        
        container = new Container
        {
            Background = null,
            BorderBrush = null,
            BorderThickness = default
        };

        panel = new NaivePanel
        {
            Children =
            {
                overlay,
                container
            }
        };

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
    
    /// <summary>
    /// Gets or sets the <see cref="Brush"/> that paints the scrim overlay when displaying a modal <see cref="Sheet"/>.
    /// The default value is <see cref="Brushes.Black"/>.
    /// </summary>
    [Bindable(false)]
    [Category(UICategory.Brush)]
    public Brush ScrimBrush
    {
        get => (Brush)GetValue(ScrimBrushProperty);
        set => SetValue(ScrimBrushProperty, value);
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
    
    internal System.Windows.Shapes.Rectangle GetOverlay() => overlay;

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