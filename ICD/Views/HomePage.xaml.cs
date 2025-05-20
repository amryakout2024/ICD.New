using ICD.ViewModels;
using ICD.Models;
using ICD.Views;
using System.Collections.ObjectModel;
using System.Globalization;
using UraniumUI.Material;
using UraniumUI.Material.Controls;
using UraniumUI.Pages;
using ZXing.Net.Maui;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ICD.Views;

public partial class HomePage : UraniumContentPage
{
	private readonly HomeVM _homeVM;

	private readonly DataContext _dataContext;

    private ObservableCollection<TradeDrug> TradeDrugsWithoutFilter = new ObservableCollection<TradeDrug>();
    private ObservableCollection<Drug> DrugsWithoutFilter=new ObservableCollection<Drug>();

    public HomePage(HomeVM homeVM,DataContext dataContext)
	{
		InitializeComponent();

		_homeVM = homeVM;
        _dataContext = dataContext;
        
		BindingContext = _homeVM;


        SetBannerId();

        cameraBarcodeReaderView.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormats.All,
            AutoRotate = true,
            TryInverted = true,
            Multiple=false,
        };
    }
    protected  void BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        var result = e.Results?.FirstOrDefault();
        if (result is null) return;

        Dispatcher.DispatchAsync(async () =>
        {
            try
            {
                var tradeDrug = TradeDrugsWithoutFilter.Where(x => x.Gtin == result.Value.Substring(3, 14)).FirstOrDefault();

                var drug = DrugsWithoutFilter.Where(x => x.DrugName == tradeDrug.DrugName && x.AdministrationRoute == tradeDrug.AdministrationRoute).FirstOrDefault();

                if (tradeDrug != null && drug != null)
                {
                    backdrop.IsPresented = false;

                    drug.TradeDrugName = tradeDrug.TradeDrugName;

                    var parameter = new Dictionary<string, object>
                    {
                        [nameof(DrugDetailVM.Drug)] = drug
                    };

                    await Shell.Current.GoToAsync(nameof(DrugDetailPage), true, parameter);

                }
                else
                {
                    await Toast.Make("Not Available , Try Search by Scientific Name Or Trade Name", ToastDuration.Short).Show();
                }

            }
            catch (Exception)
            {
                //await Toast.Make("Not Available , Try Search by Scientific Name Or Trade Name", ToastDuration.Short).Show();
            }
        });
    }
    private void SetBannerId()
    {
#if ANDROID
        myAds.AdUnitId = "ca-app-pub-3829937021524038/7874998548";
#endif
    }

    protected async override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        await _dataContext.Init();

        TradeDrugsWithoutFilter =  DataContext.TradeDrugs;
        
        DrugsWithoutFilter =  DataContext.Drugs;

        if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar")
        {
            this.FlowDirection = FlowDirection.LeftToRight;
        }

        await _homeVM.Init();

    }

    protected override bool OnBackButtonPressed()
	{

#if ANDROID
			App.Current.CloseWindow(App.Current.MainPage.Window);
			App.Current.Quit();
#endif

		return true;
	}
}