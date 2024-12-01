namespace WPF.Material.Extensions;

internal static class DesignTimeExtensions
{
    public static bool IsInDesignMode(this DependencyObject element) =>
        DesignerProperties.GetIsInDesignMode(element);
}