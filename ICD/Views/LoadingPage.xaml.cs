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

    }

    private void LoadDataBtn_Pressed(object sender, EventArgs e)
    {
        lblToStartLoad.IsVisible = false;
        lblDonotClose.IsVisible = true;
        lblFewMinutes.IsVisible = true;
        LoadDataBtn.Text = "Loading....";

    }

}