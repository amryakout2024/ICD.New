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

        private ObservableCollection<Drug> CheckedDrugs { get; set; }

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

        private bool IsFromUpdateCheckBox;

        public async Task Init()
        {
            DrugName = Drug.DrugName;

            AdministrationRoute = Drug.AdministrationRoute;

            TradeDrugsWithoutFilter = DataContext.TradeDrugs;

            DrugsWithoutFilter = DataContext.Drugs;

            Drugs = new ObservableCollection<Drug>( DrugsWithoutFilter.Where(x => x.DrugName == Drug.DrugName && x.AdministrationRoute == Drug.AdministrationRoute).ToList());

            TradeDrugs =new ObservableCollection<TradeDrug>( TradeDrugsWithoutFilter.Where(x => x.DrugName == Drug.DrugName && x.AdministrationRoute == Drug.AdministrationRoute).DistinctBy(x => new { x.TradeDrugName, x.AdministrationRoute }).ToList());
            
            CheckedDrugs = new ObservableCollection<Drug>();
            IsDrugSelected = false;
            IsLabelVisible = false;
            IsButtonVisible = false;
            IsFromUpdateCheckBox = false;
            IsAllCheckboxChecked = false;

        }
        [RelayCommand]
        private async Task CheckAllDrugs()
        {
            //CheckedDrugs.Clear();

            ObservableCollection<Drug> Drugs2 = new ObservableCollection<Drug>();

            if (IsAllCheckboxChecked)
            {
                CheckedDrugs.Clear();
                foreach (Drug drug in Drugs)
                {
                    drug.IsCheckboxChecked = true;

                    Drugs2.Add(drug);

                    CheckedDrugs.Add(drug);

                }

                Drugs = new ObservableCollection<Drug>(Drugs2);

                IsButtonVisible = true;
                IsFromUpdateCheckBox = false;
            }
            else
            {
                if (IsFromUpdateCheckBox == false)
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
                else
                {
                    IsFromUpdateCheckBox = false;
                }

            }
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
            }
            else
            {
                CheckedDrugs.Remove(drug);
                if (CheckedDrugs.Count < 1)
                {
                    IsLabelVisible = false;
                    IsButtonVisible = false;
                }
            }
            if (CheckedDrugs.Count < Drugs.Count)
            {
                IsFromUpdateCheckBox = true;

                IsAllCheckboxChecked = false;
            }
            else
            {
                IsAllCheckboxChecked = true;
            }
        }
        [RelayCommand]
        private async Task ShareCheckedDrugs()
        {
            
            string txt =(Drug.TradeDrugName!=null)
                ? $"{Drug.TradeDrugName} : {DrugName} =>" + Environment.NewLine+Environment.NewLine 
                : $"{DrugName} =>" + Environment.NewLine+Environment.NewLine ;

            foreach (var drug in CheckedDrugs)
            {
                txt = @$"{txt} {drug.Indication} : {drug.DiagnosisCode}" + Environment.NewLine + Environment.NewLine;
            }
            txt = txt + Environment.NewLine + "Download ICD-10 App from PlayStore";
            await Share.Default.RequestAsync(new ShareTextRequest
            {
                Text = $"{txt}",
                Title="",
                Uri = "https://play.google.com/store/apps/details?id=com.amr.icd&pli=1"
            });
        }

        [RelayCommand]
        private async Task CopyDrug(Drug drug)
        {
            string txt = (Drug.TradeDrugName != null) 
                ? $"{Drug.TradeDrugName} : {drug.DrugName} =>" +Environment.NewLine+Environment.NewLine+$"{drug.Indication} : {drug.DiagnosisCode}" 
                : $"{drug.DrugName} =>" + $": " + Environment.NewLine+Environment.NewLine + $"{drug.Indication} : {drug.DiagnosisCode}";

            await Clipboard.SetTextAsync(txt);
        }

    }
}
