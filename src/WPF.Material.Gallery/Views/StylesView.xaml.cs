using WPF.Material.Gallery.ViewModels;

namespace WPF.Material.Gallery.Views;

public partial class StylesView
{
    public StylesView()
    {
        InitializeComponent();
        DataContext = App.Current.Services.GetRequiredService<StylesViewModel>();
    }
}