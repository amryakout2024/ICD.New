using ICD.ViewModels;
using UraniumUI.Pages;

namespace ICD.Views;

public partial class AboutPage : UraniumContentPage
{
    private readonly AboutVM _aboutVM;

    public AboutPage(AboutVM aboutVM)
	{
		InitializeComponent();

        _aboutVM = aboutVM;

        BindingContext= _aboutVM;

        if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar")
        {
            this.FlowDirection = FlowDirection.LeftToRight;
        }
    }
    protected override bool OnBackButtonPressed()
    {
#if ANDROID
        Shell.Current.GoToAsync($"//{nameof(HomePage)}", animate: true);
#endif
        return true;
    }

}