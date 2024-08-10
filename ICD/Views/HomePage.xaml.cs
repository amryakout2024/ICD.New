using ICD.ViewModels;
using System.Collections.ObjectModel;
using System.Globalization;
using UraniumUI.Material;
namespace ICD.Views;

public partial class HomePage : ContentPage
{
	private readonly HomeVM _homeVM;

	public HomePage(HomeVM homeVM)
	{
		InitializeComponent();

		_homeVM = homeVM;

		BindingContext = _homeVM;

	}

	protected async override void OnAppearing()
	{
		base.OnAppearing();

		await _homeVM.InitializeDrugsAsync();

		if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar")
		{
			this.FlowDirection = FlowDirection.LeftToRight;
		}

	}

	protected override bool OnBackButtonPressed()
	{
		//var leave = await DisplayAlert("Leave lobby?", "Are you sure you want to leave the lobby?", "Yes", "No");

		//if (leave)
		//{
		//	await handleLeaveAsync();
		//	return base.OnBackButtonPressed();
		//}
		//else
		//{
		//	return false;
		//}

#if ANDROID
			App.Current.CloseWindow(App.Current.MainPage.Window);
			App.Current.Quit();
#endif

		return true;
	}

    private void cb_CheckChanged(object sender, EventArgs e)
    {
		//var b = (UraniumUI.Material.Controls.CheckBox)sender;
		//if (b.IsChecked==true)
		//{
		//	if (_homeVM.CheckedDrugs.Count>0)
		//	{
		//		lr.IsVisible = false;
		//		br.IsVisible = false;
		//	}
		//}
		//else
		//{
  //          if (_homeVM.CheckedDrugs.Count <1)
  //          {
  //              lr.IsVisible = true;
  //              br.IsVisible = true;
  //          }
  //      }
    }
}