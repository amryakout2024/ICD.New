using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ICD.Models;
using ICD.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ICD.ViewModels
{
	public partial class HomeVM(DataContext dataContext) : BaseVM
	{
		private readonly DataContext _dataContext = dataContext;

		[ObservableProperty]
		private List<Drug> _drugs;

		[ObservableProperty]
		private List<TradeDrug> _tradeDrugs;

		private List<Drug> DrugsWithoutFilter;

		private List<TradeDrug> TradeDrugsWithoutFilter;

        [ObservableProperty]
        private string _searchName;

        [ObservableProperty]
        private string _placeHolderText;

        [ObservableProperty]
		private string _drugName;

		[ObservableProperty]
		private string _diagnosisCode;

		[ObservableProperty]
		private string _diagnosisName1;

        [ObservableProperty]
        private string _diagnosisName2;

        [ObservableProperty]
        private string _diagnosisName3;

        [ObservableProperty]
        private bool _isDrugSelected;

		[ObservableProperty]
		[NotifyCanExecuteChangedFor(nameof(SearchCommand))]
		private bool _isTradeRadioButtonChecked;

        partial void OnIsTradeRadioButtonCheckedChanged(bool value)
        {
			Search();
        }

        [ObservableProperty]
		private Drug _selectedDrug;

		[ObservableProperty]
		private int _countDrugs;

		public async Task InitializeDrugsAsync() 
		{
			DrugsWithoutFilter = await _dataContext.LoadAllDrugsAsync();
			//DrugsWithoutFilter = new List<Drug>()
			//{
			//	new Drug(){DrugId=1,DrugName="nf",DiagnosisId="hhf",DiagnosisName="gtj"},
			//	new Drug(){DrugId=2,DrugName="nf",DiagnosisId="hhf",DiagnosisName="gtj"},
			//	new Drug(){DrugId=3,DrugName="nf",DiagnosisId="hhf",DiagnosisName="gtj"},
			//	new Drug(){DrugId=4,DrugName="nf",DiagnosisId="hhf",DiagnosisName="gtj"},
			//	new Drug(){DrugId=5,DrugName="nf",DiagnosisId="hhf",DiagnosisName="gtj"},
			//	new Drug(){DrugId=6,DrugName="nf",DiagnosisId="hhf",DiagnosisName="gtj"},
			//};
			Drugs = DrugsWithoutFilter;
			CountDrugs =Drugs.Count();

			TradeDrugsWithoutFilter = await _dataContext.LoadAllTradeDrugsAsync();
			//TradeDrugsWithoutFilter = new List<TradeDrug>()
			//{
			//	new TradeDrug(){TradeDrugId=1,TradeDrugName="srt",DrugName="e4",DiagnosisId="fre",DiagnosisName="sw"},
			//	new TradeDrug(){TradeDrugId=2,TradeDrugName="srt",DrugName="e4",DiagnosisId="fre",DiagnosisName="sw"},
			//	new TradeDrug(){TradeDrugId=3,TradeDrugName="srt",DrugName="e4",DiagnosisId="fre",DiagnosisName="sw"},
			//	new TradeDrug(){TradeDrugId=4,TradeDrugName="srt",DrugName="e4",DiagnosisId="fre",DiagnosisName="sw"},
			//	new TradeDrug(){TradeDrugId=5,TradeDrugName="srt",DrugName="e4",DiagnosisId="fre",DiagnosisName="sw"},
			//	new TradeDrug(){TradeDrugId=6,TradeDrugName="srt",DrugName="e4",DiagnosisId="fre",DiagnosisName="sw"},
			//	new TradeDrug(){TradeDrugId=7,TradeDrugName="srt",DrugName="e4",DiagnosisId="fre",DiagnosisName="sw"},
			//};

			TradeDrugs = TradeDrugsWithoutFilter;

            IsDrugSelected = false;
			IsTradeRadioButtonChecked = false;

        }

        [RelayCommand]
		private async Task Search()
		{
            if (!IsTradeRadioButtonChecked)
            {
				PlaceHolderText = "Enter Active Ingredient";

                if (!string.IsNullOrEmpty(SearchName))
				{
					IsBusy = true;
					Drugs = DrugsWithoutFilter.Where(x => x.DrugName.ToLower().Contains(SearchName.ToLower())).ToList();

					CountDrugs = Drugs.Count();
					IsBusy = false;
				}
				else
				{
					Drugs = DrugsWithoutFilter;

					CountDrugs = Drugs.Count();

				}
 
            }
            else
            {
                PlaceHolderText = "Enter Trade Name";

                if (!string.IsNullOrEmpty(SearchName))
                {
                    IsBusy = true;
                    TradeDrugs = TradeDrugsWithoutFilter.Where(x => x.TradeDrugName.ToLower().Contains(SearchName.ToLower())).ToList();

                    CountDrugs = TradeDrugs.Count();
                    IsBusy = false;
                }
                else
                {
                    TradeDrugs = TradeDrugsWithoutFilter;

                    CountDrugs = TradeDrugs.Count();

                }

            }
        }

		[RelayCommand]
		private async Task GotoHomePage()
		{
			IsDrugSelected = false;
		}

		[RelayCommand]
		private async Task ShowDrugDetailsForm(Drug drug)
		{
			//DrugName = drug.DrugName;
			//DiagnosisId = drug.DiagnosisId;
			//DiagnosisName = drug.DiagnosisName;

			//IsDrugSelected = true;
		}

		[RelayCommand]
		private async Task NavigateDrugDetailsPage(Drug drug)
		{
			//var parameter = new Dictionary<string, object>
			//{
			//	[nameof(DrugDetailsVM.Drug)] = drug,
			//};
			//await Shell.Current.GoToAsync($"//{nameof(DrugDetailsPage)}",animate:true,parameter);
		}
	}
}
