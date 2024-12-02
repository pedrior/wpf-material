using WPF.Material.Styles;

namespace WPF.Material.Gallery.ViewModels;

internal sealed partial class ShellViewModel(ThemeService themeService, IServiceProvider services) : ObservableObject
{
    [ObservableProperty] private object? content = GetOpeningContent(services);

    [ObservableProperty] private bool isDarkTheme = GetIsDarkTheme(themeService);

    [RelayCommand]
    private void Navigate(Type type) => Content = services.GetRequiredService(type);

    [RelayCommand]
    private void SwitchAppTheme() => themeService.SwitchTheme();

    private static HomeViewModel GetOpeningContent(IServiceProvider services) =>
        services.GetRequiredService<HomeViewModel>();

    private static bool GetIsDarkTheme(ThemeService themeService) =>
        themeService.Theme.Brightness is Brightness.Dark;
}