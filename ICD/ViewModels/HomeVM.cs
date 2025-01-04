using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ICD.Helpers;
using ICD.Models;
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
        private List<Drug> _drugs;      

        private List<Drug> DrugsWithoutFilter;

        [ObservableProperty]
        private List<Drug> _drugsFalse;

        [ObservableProperty]
		private List<TradeDrug> _tradeDrugs;

		private List<TradeDrug> TradeDrugsWithoutFilter=new List<TradeDrug>();
        
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
        private Drug _selectedDrug;

        [ObservableProperty]
        private int _countDrugs;


        public async Task Init()
        {

            DrugsWithoutFilter = await _dataContext.LoadAllDrugsAsync();

            Drugs = DrugsWithoutFilter.DistinctBy(x => x.DrugName).ToList();
            
            TradeDrugsWithoutFilter = await _dataContext.LoadAllTradeDrugsAsync();
            
            TradeDrugs = TradeDrugsWithoutFilter;

            PlaceHolderText = "Enter Scientific Name";
            
            CountDrugs = 0;

            IsTradeRadioButtonChecked = false;

            IsScientificRadioButtonChecked = true;

        }

        partial void OnIsTradeRadioButtonCheckedChanged(bool value)
        {
            SearchName = "";
			//Configuration 
			//Search();
        }
        
        partial void OnIsScientificRadioButtonCheckedChanged(bool value)
        {
            SearchName = "";
			//Configuration 
			//Search();
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
                    Drugs = DrugsWithoutFilter.DistinctBy(x => x.DrugName).Where(x => x.DrugName.Contains(SearchName.ToLower())).ToList();

                    CountDrugs = Drugs.Count();

                    //IsBusy = false;
                }
                else
                {
                    Drugs = DrugsWithoutFilter.DistinctBy(x => x.DrugName).ToList();

                    CountDrugs = 0;

                }

            }
            else
            {
                PlaceHolderText = "Enter Trade Name";

                if (!string.IsNullOrEmpty(SearchName))
                {
                    //IsBusy = true;
                    TradeDrugs = TradeDrugsWithoutFilter.Where(x => x.TradeDrugName.ToLower().Contains(SearchName.ToLower())).ToList();

                    CountDrugs = TradeDrugs.Count();
                    //IsBusy = false;
                }
                else
                {
                    TradeDrugs = TradeDrugsWithoutFilter;

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
            var drug = DrugsWithoutFilter.Where(x => x.DrugName == tradeDrug.DrugName).FirstOrDefault();
            if (drug != null) 
            {
                var parameter = new Dictionary<string, object>
                {
                    [nameof(DrugDetailVM.Drug)] = drug
                };
                await GoToAsyncWithStackAndParameter(nameof(DrugDetailPage), true, parameter);

            }
            else
            {
                await Toast.Make("Not Found , Try Search by Scientific Name",ToastDuration.Short).Show();
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
        private async Task CloseApp()
        {
#if ANDROID
            App.Current.CloseWindow(App.Current.MainPage.Window);
            App.Current.Quit();
#endif
        }
	}
}
