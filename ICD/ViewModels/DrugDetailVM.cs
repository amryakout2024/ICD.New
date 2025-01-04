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

namespace ICD.ViewModels
{
    [QueryProperty(nameof(Drug),nameof(Drug))]
    public partial class DrugDetailVM(DataContext dataContext):BaseVM
    {
        private readonly DataContext _dataContext = dataContext;
        [ObservableProperty]
        private Drug _drug;

        [ObservableProperty]
        private List<Drug> _drugs=new List<Drug>();

        [ObservableProperty]
        private string _drugName;

        private List<Drug> DrugsWithoutFilter = new List<Drug>();

        [ObservableProperty]
        private List<TradeDrug> _tradeDrugs =new List<TradeDrug>();

        private List<TradeDrug> TradeDrugsWithoutFilter = new List<TradeDrug>();

        [ObservableProperty]
        private string _countCheckedDrugs;

        public ObservableCollection<Drug> CheckedDrugs;

        [ObservableProperty]
        private bool _isDrugSelected;

        [ObservableProperty]
        private bool _isLabelVisible;

        [ObservableProperty]
        private bool _isRefreshing = false;

        [ObservableProperty]
        private bool _isButtonVisible;

        public async Task Init()
        {
            DrugName = Drug.DrugName;

            DrugsWithoutFilter = await _dataContext.LoadAllDrugsAsync();

            Drugs = DrugsWithoutFilter.Where(x=>x.DrugName==Drug.DrugName).ToList();

            TradeDrugsWithoutFilter = await _dataContext.LoadAllTradeDrugsAsync();

            TradeDrugs = TradeDrugsWithoutFilter.Where(x=>x.DrugName==Drug.DrugName).ToList();
            
            CheckedDrugs = new ObservableCollection<Drug>();
            IsDrugSelected = false;
            IsLabelVisible = false;
            IsButtonVisible = false;
            CountCheckedDrugs = "0";


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

            if (drug.IsCheckboxChecked == true)
            {
                CheckedDrugs.Add(drug);

                if (CheckedDrugs.Count > 0)
                {
                    IsLabelVisible = true;
                    IsButtonVisible = true;
                }
                CountCheckedDrugs = CheckedDrugs.Count.ToString();
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
        }

        [RelayCommand]
        private async Task ShareCheckedDrugs()
        {
            string txt = "";

            foreach (var drug in CheckedDrugs)
            {
                txt = @$"{txt.TrimStart()}
                 {drug.DrugName.TrimStart()} : {drug.DiagnosisCode}".TrimStart();
            }

            await Share.Default.RequestAsync(
                new ShareTextRequest(@$"{txt.TrimStart()} 
               sent from ICD-10 Application".TrimStart()
                 ));
        }

        [RelayCommand]
        private async Task ClearSelections()
        {

            //var d= Drugs.Where(x=>x.DrugId==1).SingleOrDefault();
            //         Drugs.Remove(Drugs.Where(x => x.DrugId == 1).SingleOrDefault());
            //         d.IsCheckboxChecked = false;
            //Drugs.Add(d);
            //         IsRefreshing = true;
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
