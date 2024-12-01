using WPF.Material.Gallery.ViewModels;

namespace WPF.Material.Gallery.Views;

public partial class ComponentsView
{
    public ComponentsView()
    {
        InitializeComponent();
        DataContext = App.Current.Services.GetRequiredService<ComponentsViewModel>();
    }
}