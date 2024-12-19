namespace WPF.Material.Components;

/// <summary>
/// A panel that arranges its children in a single line and wraps them to the next line when the available width is
/// exceeded. The panel supports horizontal and vertical spacing between children and horizontal content alignment.
/// </summary>
public class WrapPanel : Panel
{
    /// <summary>
    /// Identifies the <see cref="HorizontalContentAlignment"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty HorizontalContentAlignmentProperty = DependencyProperty.Register(
        nameof(HorizontalContentAlignment),
        typeof(HorizontalAlignment),
        typeof(WrapPanel),
        new FrameworkPropertyMetadata(HorizontalAlignment.Center, FrameworkPropertyMetadataOptions.AffectsArrange));

    /// <summary>
    /// Identifies the <see cref="HorizontalSpacing"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty HorizontalSpacingProperty = DependencyProperty.Register(
        nameof(HorizontalSpacing),
        typeof(double),
        typeof(WrapPanel),
        new FrameworkPropertyMetadata(
            0.0,
            FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure,
            null,
            CoerceSpacing));

    /// <summary>
    /// Identifies the <see cref="VerticalSpacing"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty VerticalSpacingProperty = DependencyProperty.Register(
        nameof(VerticalSpacing),
        typeof(double),
        typeof(WrapPanel),
        new FrameworkPropertyMetadata(
            0.0,
            FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure,
            null,
            CoerceSpacing));
    
    static WrapPanel()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(WrapPanel),
            new FrameworkPropertyMetadata(typeof(WrapPanel)));
    }

    /// <summary>
    /// Gets or sets the horizontal alignment of the content within the panel. The Default value is
    /// <see cref="HorizontalAlignment.Center"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Layout)]
    public HorizontalAlignment HorizontalContentAlignment
    {
        get => (HorizontalAlignment)GetValue(HorizontalContentAlignmentProperty);
        set => SetValue(HorizontalContentAlignmentProperty, value);
    }

    /// <summary>
    /// Gets or sets the horizontal spacing between children. The Default value is 0.0.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Layout)]
    public double HorizontalSpacing
    {
        get => (double)GetValue(HorizontalSpacingProperty);
        set => SetValue(HorizontalSpacingProperty, value);
    }

    /// <summary>
    /// Gets or sets the vertical spacing between children. The Default value is 0.0.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Layout)]
    public double VerticalSpacing
    {
        get => (double)GetValue(VerticalSpacingProperty);
        set => SetValue(VerticalSpacingProperty, value);
    }

    protected override Size MeasureOverride(Size availableSize)
    {
        var width = 0.0;
        var height = 0.0;
        
        var rowWidth = 0.0;
        var rowHeight = 0.0;
        
        var children = InternalChildren;
        for (var index = 0; index < children.Count; index++)
        {
            var child = children[index];

            // Let the child determine its size
            child.Measure(availableSize);

            var childSize = child.DesiredSize;

            // Add horizontal spacing between children
            var horizontalSpacing = rowWidth > 0.0 ? HorizontalSpacing : 0.0;

            // Check if the current row has enough space for the next child
            if (rowWidth + childSize.Width + horizontalSpacing > availableSize.Width)
            {
                // Add vertical spacing between rows
                var verticalSpacing = index > 0 ? VerticalSpacing : 0.0;

                // Row exceeded available width, move to the next row and update total size
                width = Math.Max(rowWidth, width);
                height += rowHeight + verticalSpacing;

                // Start a new row with the moved child
                rowWidth = childSize.Width;
                rowHeight = childSize.Height;
            }
            else
            {
                // Accumulate the width and height for the current row
                rowWidth += childSize.Width + horizontalSpacing;
                rowHeight = Math.Max(childSize.Height, rowHeight);
            }
        }

        // Add the final row size to the total panel size
        width = Math.Max(rowWidth, width);
        height += rowHeight;

        return new Size(width, height);
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        var rowWidth = 0.0;
        var rowHeight = 0.0;
        
        var verticalOffset = 0.0;

        var firstChildInRowIndex = 0;
        
        var children = InternalChildren;
        for (var index = 0; index < children.Count; index++)
        {
            var childSize = children[index].DesiredSize;
            var horizontalSpacing = rowWidth > 0 ? HorizontalSpacing : 0.0;

            // Check if the current row has enough space for the next child
            if (rowWidth + childSize.Width + horizontalSpacing > finalSize.Width)
            {
                // Arrange the current row and move to the next row
                ArrangeRow(
                    verticalOffset,
                    rowWidth,
                    rowHeight,
                    finalSize.Width,
                    firstChildInRowIndex,
                    index);

                rowWidth = childSize.Width;
                rowHeight = childSize.Height;
                verticalOffset += rowHeight + VerticalSpacing;

                firstChildInRowIndex = index;
            }
            else
            {
                // Accumulate the width and height for the current row
                rowWidth += childSize.Width + horizontalSpacing;
                rowHeight = Math.Max(childSize.Height, rowHeight);
            }
        }

        // Arrange the final row
        if (firstChildInRowIndex < children.Count)
        {
            ArrangeRow(
                verticalOffset,
                rowWidth,
                rowHeight,
                finalSize.Width,
                firstChildInRowIndex,
                children.Count);
        }

        return finalSize;
    }

    private void ArrangeRow(double y, double rowWidth, double rowHeight, double width, int childIndex, int childCount)
    {
        // Calculate the horizontal offset based on the horizontal content alignment
        var x = HorizontalContentAlignment switch
        {
            HorizontalAlignment.Center => (width - rowWidth) * 0.5,
            HorizontalAlignment.Right => width - rowHeight,
            _ => 0.0
        };

        var children = InternalChildren;

        // Arrange each child in the row
        for (var index = childIndex; index < childCount; index++)
        {
            var child = children[index];
            var childWidth = child.DesiredSize.Width;

            // Arrange the child
            child.Arrange(new Rect(x, y, childWidth, rowHeight));

            // Add horizontal spacing between children unless it's the last child in the row
            var horizontalSpacing = index < childCount - 1 ? HorizontalSpacing : 0.0;

            // Increment horizontal offset,
            x += childWidth + horizontalSpacing;
        }
    }
    
    private static object CoerceSpacing(DependencyObject element, object value) => 
        Math.Max(0.0, (double)value);
}