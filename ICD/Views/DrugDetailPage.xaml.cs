using ICD.ViewModels;
using UraniumUI.Pages;

namespace ICD.Views;

public partial class DrugDetailPage : UraniumContentPage
{
    private readonly DrugDetailVM _drugDetailVM;

    public DrugDetailPage(DrugDetailVM drugDetailVM)
	{
		InitializeComponent();
        
        _drugDetailVM = drugDetailVM;

        BindingContext = _drugDetailVM;

        SetBannerId();

    }
    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        await _drugDetailVM.Init();
    }
    private void SetBannerId()
    {
        //#if ANDROID
        //		myAds.AdsId="ca-app-pub-3829937021524038/7874998548";
        //#endif
    }
    protected override bool OnBackButtonPressed()
    {
#if ANDROID
        Shell.Current.GoToAsync($"//{nameof(HomePage)}", animate: true);
#endif
        return true;
    }

}