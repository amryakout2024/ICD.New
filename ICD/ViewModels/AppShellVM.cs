using CommunityToolkit.Mvvm.Input;

namespace ICD.ViewModels
{
    public partial class AppShellVM : BaseVM
    {

        [RelayCommand]
        private async Task CloseApp()
        {
#if ANDROID
            App.Current.CloseWindow(App.Current.MainPage.Window);
            App.Current.Quit();
#endif
        }
    }
}
