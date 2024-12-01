using WPF.Material.Gallery.ViewModels;

namespace WPF.Material.Gallery.Views;

public partial class ShellView
{
    public ShellView()
    {
        InitializeComponent();
        DataContext = App.Current.Services.GetRequiredService<ShellViewModel>();
    }
}