using ICD.ViewModels;
using System.Collections.ObjectModel;
using System.Globalization;
using UraniumUI.Material;
using UraniumUI.Pages;
namespace ICD.Views;

public partial class HomePage : UraniumContentPage
{
	private readonly HomeVM _homeVM;

	public HomePage(HomeVM homeVM)
	{
		InitializeComponent();

		_homeVM = homeVM;

		BindingContext = _homeVM;

		SetBannerId();
	}

    private void SetBannerId()
    {
//#if ANDROID
//		myAds.AdsId="ca-app-pub-3829937021524038/7874998548";
//#endif
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

}