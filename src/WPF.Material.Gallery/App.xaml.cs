using System.Windows;
using WPF.Material.Gallery.ViewModels;
using WPF.Material.Gallery.Views;
using WPF.Material.Styles;

namespace WPF.Material.Gallery;

public sealed partial class App
{
    public App()
    {
        InitializeComponent();
    }

    public new static App Current => (App)Application.Current;

    public IServiceProvider Services { get; } = ConfigureServices();

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        new ShellView()
            .Show();
    }

    private static ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        // Services
        services.AddSingleton(ThemeService.Instance);

        // ViewModels
        services.AddSingleton<ShellViewModel>();
        services.AddTransient<HomeViewModel>();
        services.AddTransient<GetStartedViewModel>();
        services.AddTransient<FoundationsViewModel>();
        services.AddTransient<StylesViewModel>();
        services.AddTransient<ComponentsViewModel>();
        
        return services.BuildServiceProvider();
    }
}