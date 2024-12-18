using System.Diagnostics;

namespace WPF.Material.Gallery.ViewModels;

internal sealed partial class HomeViewModel : ObservableObject
{
    [RelayCommand]
    private static void OpenInBrowser(string url)
    {
        try
        {
            Process.Start(new ProcessStartInfo(url)
            {
                UseShellExecute = true
            });
        }
        catch
        {
            // Ignored
        }
    }
}