using WPF.Material.Gallery.ViewModels;

namespace WPF.Material.Gallery.Views;

public partial class HomeView
{
    public HomeView()
    {
        InitializeComponent();
        DataContext = App.Current.Services.GetRequiredService<HomeViewModel>();
    }
}