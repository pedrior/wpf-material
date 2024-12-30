namespace WPF.Material.Components;

/// <summary>
/// A lightweight panel that arranges its children in a single line.
/// </summary>
public class NaivePanel : Panel
{
    static NaivePanel()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(NaivePanel),
            new FrameworkPropertyMetadata(typeof(NaivePanel)));
    }

    protected override Size MeasureOverride(Size availableSize)
    {
        var size = new Size();
        
        var children = InternalChildren;
        for (var i = 0; i < children.Count; i++)
        {
            var child = children[i];
            child.Measure(availableSize);
            
            var childSize = child.DesiredSize;

            size.Width = Math.Max(size.Width, childSize.Width);
            size.Height = Math.Max(size.Height, childSize.Height);
        }

        return size;
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        var children = InternalChildren;
        for (var i = 0; i < children.Count; i++)
        {
            var child = children[i];
            if (child.Visibility is not Visibility.Collapsed)
            {
                child.Arrange(new Rect(finalSize));
            }
        }

        return finalSize;
    }
}