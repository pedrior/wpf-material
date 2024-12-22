using WPF.Material.Gallery.ViewModels;

namespace WPF.Material.Gallery.Views;

public partial class FoundationsView
{
    public FoundationsView()
    {
        InitializeComponent();
        DataContext = App.Current.Services.GetRequiredService<FoundationsViewModel>();
    }
}