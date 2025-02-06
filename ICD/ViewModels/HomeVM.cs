using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ICD.Helpers;
using ICD.Models;
//using MoreLinq.Extensions;
using ICD.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ICD.ViewModels
{
	public partial class HomeVM(DataContext dataContext) : BaseVM
	{
		private readonly DataContext _dataContext = dataContext;

        [ObservableProperty]
        private ObservableCollection<Drug> _drugs;      

        private ObservableCollection<Drug> DrugsWithoutFilter;

        [ObservableProperty]
        private ObservableCollection<Drug> _drugsFalse;

        [ObservableProperty]
		private ObservableCollection<TradeDrug> _tradeDrugs;

		private ObservableCollection<TradeDrug> TradeDrugsWithoutFilter=new ObservableCollection<TradeDrug>();
        
        [ObservableProperty]
        private string _searchName;

        [ObservableProperty]
        private string _placeHolderText;

		[ObservableProperty]
		private string _diagnosisCode;

        [ObservableProperty]
        private bool _isRefreshing = false;

        //[NotifyCanExecuteChangedFor(nameof(SearchCommand))]
        [ObservableProperty]
        private bool _isTradeRadioButtonChecked;

        //[NotifyCanExecuteChangedFor(nameof(SearchCommand))]
        [ObservableProperty]
        private bool _isScientificRadioButtonChecked;

        [ObservableProperty]
        private bool _isBackdropViewPresented;

        [ObservableProperty]
        private Drug _selectedDrug;

        [ObservableProperty]
        private int _countDrugs;

        public async Task Init()
        {
            IsBackdropViewPresented=false;

            TradeDrugsWithoutFilter = DataContext.TradeDrugs;

            DrugsWithoutFilter = DataContext.Drugs;

            Drugs =new ObservableCollection<Drug>( MoreLinq.MoreEnumerable.DistinctBy(DrugsWithoutFilter,x => new { x.DrugName ,x.AdministrationRoute}).ToList());

            TradeDrugs =new ObservableCollection<TradeDrug>( MoreLinq.MoreEnumerable.DistinctBy(TradeDrugsWithoutFilter,x => new { x.TradeDrugName, x.AdministrationRoute }).ToList());

            PlaceHolderText = "Enter Scientific Name";
            
            CountDrugs = 0;

            IsTradeRadioButtonChecked = false;

            IsScientificRadioButtonChecked = true;

        }

        partial void OnIsTradeRadioButtonCheckedChanged(bool value)
        {
            //SearchName = "";
            if (IsTradeRadioButtonChecked)
            {
                PlaceHolderText = "Enter Trade Name";
            }
            else
            {
                PlaceHolderText = "Enter Scientific Name";
            }
            //Configuration 
            Search();
        }

        partial void OnIsScientificRadioButtonCheckedChanged(bool value)
        {
            //SearchName = "";
            //Configuration 
            Search();
        }

        [RelayCommand]
		private async Task Search()
        {
            if (!IsTradeRadioButtonChecked)
            {
                PlaceHolderText = "Enter Scientific Name";

                if (!string.IsNullOrEmpty(SearchName))
                {
                    //IsBusy = true;
                    //Drugs = DrugsWithoutFilter.DistinctBy(x => new { x.DrugName, x.AdministrationRoute }).Where(x => x.DrugName.Contains(SearchName.ToAllFirstLetterInUpper())).ToList();
                    Drugs =new ObservableCollection<Drug>( MoreLinq.MoreEnumerable.DistinctBy(DrugsWithoutFilter, x => new { x.DrugName, x.AdministrationRoute }).Where(x => x.DrugName.Contains(SearchName.ToAllFirstLetterInUpper())).ToList());

                    CountDrugs = Drugs.Count();

                    //IsBusy = false;
                }
                else
                {
                    Drugs =new ObservableCollection<Drug>( MoreLinq.MoreEnumerable.DistinctBy(DrugsWithoutFilter, x => new { x.DrugName, x.AdministrationRoute }).ToList());

                    CountDrugs = 0;

                }

            }
            else
            {
                PlaceHolderText = "Enter Trade Name";

                if (!string.IsNullOrEmpty(SearchName))
                {
                    //IsBusy = true;
                    //TradeDrugs = TradeDrugsWithoutFilter.DistinctBy(x => new { x.TradeDrugName, x.AdministrationRoute }).Where(x => x.TradeDrugName.Contains(SearchName.ToAllFirstLetterInUpper())).ToList();
                    TradeDrugs =new ObservableCollection<TradeDrug>( MoreLinq.MoreEnumerable.DistinctBy(TradeDrugsWithoutFilter, x => new { x.TradeDrugName, x.AdministrationRoute }).Where(x => x.TradeDrugName.Contains(SearchName.ToAllFirstLetterInUpper())).ToList());

                    CountDrugs = TradeDrugs.Count();
                    //IsBusy = false;
                }
                else
                {
                    TradeDrugs =new ObservableCollection<TradeDrug>( MoreLinq.MoreEnumerable.DistinctBy(TradeDrugsWithoutFilter, x => new { x.TradeDrugName, x.AdministrationRoute }).ToList());

                    CountDrugs = 0;

                }

            }
        }

        [RelayCommand]
        private async Task ShowDrugDetails(Drug drug)
        {
            var parameter = new Dictionary<string, object>
            {
                [nameof(DrugDetailVM.Drug)] = drug
            };
             await GoToAsyncWithStackAndParameter(nameof(DrugDetailPage),true,parameter);
        }

        [RelayCommand]
        private async Task ShowTradeDrugDetails(TradeDrug tradeDrug)
        {
            try
            {
                var drug = DrugsWithoutFilter.Where(x => x.DrugName == tradeDrug.DrugName && x.AdministrationRoute == tradeDrug.AdministrationRoute).FirstOrDefault();

                if (drug != null)
                {
                    drug.TradeDrugName = tradeDrug.TradeDrugName;
                    var parameter = new Dictionary<string, object>
                    {
                        [nameof(DrugDetailVM.Drug)] = drug
                    };
                    await GoToAsyncWithStackAndParameter(nameof(DrugDetailPage), true, parameter);

                }
                else
                {
                    await Toast.Make("Not found , Try Search by Scientific Name", ToastDuration.Short).Show();
                }

            }
            catch (Exception)
            {
                await Toast.Make("Not found , Try Search by Scientific Name", ToastDuration.Short).Show();
            }

        }

		[RelayCommand]
		private async Task ShareTheApplication()
        {

			await Share.Default.RequestAsync(new ShareTextRequest
			{
				Uri = "https://play.google.com/store/apps/details?id=com.amr.icd&pli=1"
			});

        }

        [RelayCommand]
        private async Task ShowAboutPage()
        {
            await GoToAsyncWithStack(nameof(AboutPage), true);
        }

        [RelayCommand]
        private async Task CheckCameraPermission()
        {
            if (IsBackdropViewPresented==false)
            {
                PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.Camera>();

                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.Camera>();
                }

                if (status == PermissionStatus.Granted)
                {
                    IsBackdropViewPresented = true;
                }
            }
            else
            {
                IsBackdropViewPresented = false;

            }


        }

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
