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
using System.Windows.Input;

namespace ICD.ViewModels
{
	public partial class HomeVM(DataContext dataContext) : BaseVM
	{
		private readonly DataContext _dataContext = dataContext;

		[ObservableProperty]
		private List<Drug> _drugs;

        [ObservableProperty]
        private List<Drug> _drugsFalse;

        [ObservableProperty]
		private List<TradeDrug> _tradeDrugs;

		private List<Drug> DrugsWithoutFilter;

		private List<TradeDrug> TradeDrugsWithoutFilter;

        [ObservableProperty]
        private string _searchName;

        [ObservableProperty]
        private string _countCheckedDrugs;

        [ObservableProperty]
        private string _placeHolderText;

        public ObservableCollection<Drug> CheckedDrugs;

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
        private bool _isLabelVisible;

        [ObservableProperty]
        private bool _isRefreshing = false;

        [ObservableProperty]
        private bool _isButtonVisible;

        [ObservableProperty]
		[NotifyCanExecuteChangedFor(nameof(SearchCommand))]
		private bool _isTradeRadioButtonChecked;

        partial void OnIsTradeRadioButtonCheckedChanged(bool value)
        {
			//Configuration 
			Search();
        }

        [ObservableProperty]
		private Drug _selectedDrug;

		[ObservableProperty]
		private int _countDrugs;

		public async Task InitializeDrugsAsync() 
		{
			DrugsWithoutFilter = await _dataContext.LoadAllDrugsAsync();
			Drugs = DrugsWithoutFilter;
			DrugsFalse = Drugs;
			CountDrugs =Drugs.Count();

			TradeDrugsWithoutFilter = await _dataContext.LoadAllTradeDrugsAsync();
			TradeDrugs = TradeDrugsWithoutFilter;

            IsDrugSelected = false;
			IsTradeRadioButtonChecked = false;

            CheckedDrugs = new ObservableCollection<Drug>();
			IsDrugSelected=false;
			IsLabelVisible=false;
			IsButtonVisible=false;
			CountCheckedDrugs = "0";

			//Shell.SetBackButtonBehavior(this, new BackButtonBehavior()
			//{
			//	Command = searchComman()
			//});
				 
        }

        private ICommand searchComman()
        {
            throw new NotImplementedException();
        }

        [RelayCommand]
		private async Task Search()
		{
            if (!IsTradeRadioButtonChecked)
            {
				PlaceHolderText = "Enter Active Ingredient";

                if (!string.IsNullOrEmpty(SearchName))
				{
					//IsBusy = true;
					Drugs = DrugsWithoutFilter.Where(x => x.DrugName.ToLower().Contains(SearchName.ToLower())).ToList();

					CountDrugs = Drugs.Count();

					//IsBusy = false;
				}
				else
				{
					Drugs = DrugsWithoutFilter;

					CountDrugs = 0;

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
        private async Task ShareDrug(Drug drug)
        {
			await Share.Default.RequestAsync(
				new ShareTextRequest(
					@$"{drug.DrugName.TrimStart()} : {drug.DiagnosisCode} 
		sent from ICD-10 Application".TrimStart()
                    ));
        }

		[RelayCommand]
		private async Task ShareCheckedDrugs()
		{
			if (CheckedDrugs.Count>10)
			{
				await Shell.Current.DisplayAlert("Error","Can not share more than 10 records","Ok",FlowDirection.LeftToRight);
				return;
			}
			string txt = "";
			foreach (var d in CheckedDrugs)
			{
				txt = @$"{txt.TrimStart()}
        {d.DrugName.TrimStart()} : {d.DiagnosisCode}".TrimStart();
			}

			await Share.Default.RequestAsync(
				new ShareTextRequest(@$"{txt.TrimStart()} 
		    sent from ICD-10 Application".TrimStart()
        ));

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

        [RelayCommand]
        private async Task ClearSelections()
        {
			Drugs = DrugsWithoutFilter;
			//var d= Drugs.Where(x=>x.DrugId==1).SingleOrDefault();
   //         Drugs.Remove(Drugs.Where(x => x.DrugId == 1).SingleOrDefault());
   //         d.IsCheckboxChecked = false;
			//Drugs.Add(d);
   //         IsRefreshing = true;
        }

        [RelayCommand]
        private async Task UpdateCheckedDrug(Drug drug)
        {

			if (drug.IsCheckboxChecked == true)
			{
				CheckedDrugs.Add(drug);
                if (CheckedDrugs.Count>0)
                {
					IsLabelVisible = true;
                    IsButtonVisible = true;
                }
                CountCheckedDrugs=CheckedDrugs.Count.ToString();
            }
			else 
			{
				CheckedDrugs.Remove(drug);
                if (CheckedDrugs.Count < 1)
                {
                    IsLabelVisible = false;
                    IsButtonVisible = false;
                }

                CountCheckedDrugs = CheckedDrugs.Count.ToString();
            }

   //         var d = Drugs.Where(x => x.DrugId == drug.DrugId).FirstOrDefault();
   //         d.IsCheckboxChecked = true;
   //         d.IsButtonVisible = false;
   //         Drugs.Remove(drug);
			////Drugs.Add(d);
			//var c= Drugs.Count();
            //var d2 = Drugs.Where(x => x.DrugId == d.DrugId).FirstOrDefault();
			
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
