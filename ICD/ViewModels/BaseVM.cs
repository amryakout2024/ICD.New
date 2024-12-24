using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICD.ViewModels
{
    public partial class BaseVM : ObservableObject
    {

        [ObservableProperty]
        private bool _isBusy;

        protected async Task GoToAsyncBack() =>
            await Shell.Current.GoToAsync("///..");

        protected async Task GoToAsyncWithStack(string url, bool animate) =>
            await Shell.Current.GoToAsync(url, animate);

        protected async Task GoToAsyncWithStackAndParameter(string url, bool animate, Dictionary<string, object> parameter) =>
            await Shell.Current.GoToAsync(url, animate, parameter);

        protected async Task GoToAsyncWithShell(string url, bool animate) =>
             await Shell.Current.GoToAsync($"//{url}", animate);

        protected async Task GoToAsync(string url, bool animate, IDictionary<string, object> parameters) =>
            await Shell.Current.GoToAsync(url, animate, parameters);

        protected async Task<bool> ShowAlertToConfirmAsync(string title, string message) =>
             await Shell.Current.DisplayAlert(title, message, "Yes", "No");



        protected async Task ShowErrorAlertAsync(string errorMessage) =>
            await Shell.Current.DisplayAlert("Error", errorMessage, "Ok");
        protected async Task ShowAlertAsync(string message) =>
            await ShowAlertAsync("Alert", message);
        protected async Task ShowAlertAsync(string title, string message) =>
            await Shell.Current.DisplayAlert(title, message, "Ok");

        //protected async Task ShowToastAsync(string message) => await Toast.Make(message).Show();


        //protected async Task HandleApiExceptionAsync(ApiException ex, Action? signout = null)
        //{
        //    if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        //    {
        //        // User is not logged in
        //        await ShowErrorAlertAsync("Session Expired.");
        //        signout?.Invoke();
        //        await GoToAsync($"//{nameof(OnboardingPage)}");
        //        return;
        //    }
        //    await ShowErrorAlertAsync(ex.Message);
        //}	
        //}
    }
}
