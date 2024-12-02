namespace WPF.Material.Styles;

/// <summary>
/// Provides extension methods for the <see cref="ThemeService"/> class.
/// </summary>
public static class ThemeServiceExtensions
{
    /// <summary>
    /// Switches the current theme of the application.
    /// </summary>
    /// <param name="themeService">The <see cref="ThemeService"/> instance.</param>
    /// <param name="light">The light theme to switch to. When <see langword="null"/>, the default light theme is used.</param>
    /// <param name="dark">The dark theme to switch to. When <see langword="null"/>, the default dark theme is used.</param>
    public static void SwitchTheme(this ThemeService themeService, Theme? light = null, Theme? dark = null) =>
        themeService.Theme = themeService.Theme.Brightness switch
        {
            Brightness.Light => dark ?? Theme.Dark,
           _ => light ?? Theme.Light
        };
}