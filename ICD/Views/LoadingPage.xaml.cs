using ICD.ViewModels;

namespace ICD.Views;

public partial class LoadingPage : ContentPage
{
    private readonly LoadingVM _loadingVM;

    public LoadingPage(LoadingVM loadingVM)
	{
		InitializeComponent();

        _loadingVM = loadingVM;

        BindingContext =_loadingVM;

        SetBannerId();

    }
    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        await _loadingVM.Init();
    }
    private void SetBannerId()
    {
//#if ANDROID
//		myAds.AdsId="ca-app-pub-3829937021524038/7874998548";
//#endif
    }
    private void LoadDataBtn_Pressed(object sender, EventArgs e)
    {
        lblToStartLoad.IsVisible = false;
        lblDonotClose.IsVisible = true;
        lblFewMinutes.IsVisible = true;
        LoadDataBtn.Text = "Loading....";

    }

}