namespace WPF.Material.Controls;

/// <summary>
/// Represents a panel that arranges its children in a single layer. The children are arranged in the order they are
/// added to the <see cref="NaivePanel.Children"/> collection. To control the order of the children, use the
/// <see cref="Panel.ZIndexProperty"/> attached property.
/// </summary>
public class NaivePanel : Panel
{
    static NaivePanel()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(NaivePanel),
            new FrameworkPropertyMetadata(typeof(NaivePanel)));
    }

    protected override Size MeasureOverride(Size constraints)
    {
        var children = InternalChildren;
        var childrenCount = children.Count;
        
        var width = 0.0;
        var height = 0.0;

        for (var i = 0; i < childrenCount; i++)
        {
            var child = children[i];
            if (child.Visibility is Visibility.Collapsed)
            {
                continue;
            }
            
            child.Measure(constraints);
            
            var childSize = child.DesiredSize;

            width = Math.Max(width, childSize.Width);
            height = Math.Max(height, childSize.Height);
        }

        return new Size(width, height);
    }

    protected override Size ArrangeOverride(Size constraints)
    {
        var children = InternalChildren;
        var childrenCount = children.Count;
        
        for (var i = 0; i < childrenCount; i++)
        {
            var child = children[i];
            if (child.Visibility is Visibility.Collapsed)
            {
                continue;
            }
            
            child.Arrange(new Rect(0.0, 0.0, constraints.Width, constraints.Height));
        }

        return constraints;
    }
}