using WPF.Material.Gallery.ViewModels;

namespace WPF.Material.Gallery.Views;

public partial class GetStartedView
{
    public GetStartedView()
    {
        InitializeComponent();
        DataContext = App.Current.Services.GetRequiredService<GetStartedViewModel>();
    }
}