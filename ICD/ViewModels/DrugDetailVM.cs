using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ICD.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ICD.ViewModels
{
    [QueryProperty(nameof(Drug),nameof(Drug))]
    public partial class DrugDetailVM(DataContext dataContext):BaseVM
    {
        private readonly DataContext _dataContext = dataContext;
        [ObservableProperty]
        private Drug _drug;

        [ObservableProperty]
        private ObservableCollection<Drug> _drugs=new ObservableCollection<Drug>();

        [ObservableProperty]
        private string _drugName;

        [ObservableProperty]
        private string _administrationRoute;

        private ObservableCollection<Drug> DrugsWithoutFilter = new ObservableCollection<Drug>();

        [ObservableProperty]
        private ObservableCollection<TradeDrug> _tradeDrugs =new ObservableCollection<TradeDrug>();

        private ObservableCollection<TradeDrug> TradeDrugsWithoutFilter = new ObservableCollection<TradeDrug>();

        //[ObservableProperty]
        //private string _countCheckedDrugs;

        public ObservableCollection<Drug> CheckedDrugs;

        [ObservableProperty]
        private bool _isDrugSelected;

        [ObservableProperty]
        private bool _isLabelVisible;

        [ObservableProperty]
        private bool _isAllCheckboxChecked;

        [ObservableProperty]
        private bool _isRefreshing = false;

        [NotifyPropertyChangedFor(nameof( Drugs))]
        [ObservableProperty]
        private bool _isButtonVisible;

        public async Task Init()
        {
            DrugName = Drug.DrugName;

            AdministrationRoute = Drug.AdministrationRoute;

            TradeDrugsWithoutFilter = DataContext.TradeDrugs;

            DrugsWithoutFilter = DataContext.Drugs;

            Drugs = new ObservableCollection<Drug>( DrugsWithoutFilter.Where(x => x.DrugName == Drug.DrugName && x.AdministrationRoute == Drug.AdministrationRoute).ToList());

            TradeDrugs =new ObservableCollection<TradeDrug>( TradeDrugsWithoutFilter.Where(x => x.DrugName == Drug.DrugName && x.AdministrationRoute == Drug.AdministrationRoute).DistinctBy(x => new { x.TradeDrugName, x.AdministrationRoute }).ToList());
            //TradeDrugs = TradeDrugsWithoutFilter.Where(x=>x.DrugName==Drug.DrugName&&x.AdministrationRoute==Drug.AdministrationRoute).ToList();
            
            CheckedDrugs = new ObservableCollection<Drug>();
            IsDrugSelected = false;
            IsLabelVisible = false;
            IsButtonVisible = false;

            IsAllCheckboxChecked = false;

        }

        [RelayCommand]
        private async Task UpdateCheckedDrug(Drug drug)
        {
            if (drug.IsCheckboxChecked == true)
            {
                drug.IsCheckboxChecked = false;
            }
            else
            {
                drug.IsCheckboxChecked = true;
            }

            if (drug.IsCheckboxChecked == false)
            {
                CheckedDrugs.Add(drug);

                if (CheckedDrugs.Count > 0)
                {
                    IsLabelVisible = true;
                    IsButtonVisible = true;
                }
                IsAllCheckboxChecked=(CheckedDrugs.Count != Drugs.Count) ? false:true;

            }
            else
            {
                CheckedDrugs.Remove(drug);
                if (CheckedDrugs.Count < 1)
                {
                    IsLabelVisible = false;
                    IsButtonVisible = false;
                    IsAllCheckboxChecked = false;
                }
            }
        }
        [RelayCommand]
        private async Task ShareCheckedDrugs()
        {
            string txt =$"{DrugName} =>";

            foreach (var drug in CheckedDrugs)
            {
                txt = @$"{txt}" + Environment.NewLine + $"{drug.Indication} : {drug.DiagnosisCode}";
            }

            await Share.Default.RequestAsync(new ShareTextRequest
            {
                Text = $"{txt}" + Environment.NewLine,
                Title="",
                Uri = "https://play.google.com/store/apps/details?id=com.amr.icd&pli=1"
            });
        }

        [RelayCommand]
        private async Task CopyDrug(Drug drug)
        {
            await Clipboard.SetTextAsync($"{drug.DrugName} : "+Environment.NewLine+$"{drug.Indication} : {drug.DiagnosisCode}");
        }

        [RelayCommand]
        private async Task CheckAllDrugs()
        {
            CheckedDrugs.Clear();

            ObservableCollection<Drug> Drugs2 = new ObservableCollection<Drug>();

            if (IsAllCheckboxChecked)
            {
                foreach (Drug drug in Drugs)
                {
                    drug.IsCheckboxChecked = true;

                    Drugs2.Add(drug);

                    CheckedDrugs.Add(drug);

                }

                Drugs = new ObservableCollection<Drug>(Drugs2);

                IsButtonVisible = true;

            }
            else
            {
                foreach (Drug drug in Drugs)
                {
                    drug.IsCheckboxChecked = false;

                    Drugs2.Add(drug);

                    CheckedDrugs.Remove(drug);
                }
                Drugs = new ObservableCollection<Drug>(Drugs2);

                IsButtonVisible = false;

            }
        }

    }
}
//var parameter=Dictionary<string<drug>> 
//await GoToAsyncWithStack(nameof(DrugDetailPage),true);
//	await Share.Default.RequestAsync(
//		new ShareTextRequest(
//	
//	
//			@$"{drug.DrugName.TrimStart()} : {drug.DiagnosisCode} 
//sent from ICD-10 Application".TrimStart()
//));
//	await Share.Default.RequestAsync(
//		new ShareTextRequest(
//			@$"{drug.DrugName.TrimStart()} : {drug.DiagnosisCode} 
//sent from ICD-10 Application".TrimStart()
//));
