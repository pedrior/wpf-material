namespace WPF.Material.Extensions;

internal static class VisualTreeExtensions
{
    public static T? FindVisualParent<T>(this DependencyObject? child) where T : DependencyObject
    {
        while (child is not null)
        {
            if (child is T parent)
            {
                return parent;
            }

            child = VisualTreeHelper.GetParent(child);
        }

        return null;
    }
}