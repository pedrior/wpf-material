using System.Windows.Input;
using WPF.Material.Gallery.ViewModels;

namespace WPF.Material.Gallery.Views;

public partial class ShellView
{
    public ShellView()
    {
        InitializeComponent();
        DataContext = App.Current.Services.GetRequiredService<ShellViewModel>();
    }

    private void NavigationRailSideSheet_OnMouseLeave(object sender, MouseEventArgs e)
    {
        //NavigationRailSideSheet.IsOpen = false;
    }

    private void NavigationRailItem_MouseEnter(object sender, MouseEventArgs e)
    {
       // NavigationRailSideSheet.IsOpen = true;

    }
}