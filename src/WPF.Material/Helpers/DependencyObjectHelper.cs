namespace WPF.Material.Helpers;

internal static class DependencyObjectHelper
{
    public static T? FindChild<T>(DependencyObject? element) where T : DependencyObject
    {
        switch (element)
        {
            case null:
                return null;
            case T child:
                return child;
        }

        var count = VisualTreeHelper.GetChildrenCount(element);
        for (var i = 0; i < count; i++)
        {
            var child = VisualTreeHelper.GetChild(element, i);
            var result = FindChild<T>(child);
            if (result is not null)
            {
                return result;
            }
        }
        
        return null;
    }
}