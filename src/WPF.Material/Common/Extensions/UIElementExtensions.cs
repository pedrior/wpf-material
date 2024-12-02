namespace WPF.Material.Common;

internal static class UIElementExtensions
{
    public static void SetAnimatedPropertyValue(this UIElement element, DependencyProperty? property, object? value)
    {
        if (property is null)
        {
            return;
        }
        
        // Remove any existing animation.
        element.BeginAnimation(property, null);
        
        element.SetValue(property, value);
    }
}