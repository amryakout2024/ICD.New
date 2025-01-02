using CommunityToolkit.Mvvm.ComponentModel;
using ICD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
       
        public async Task Init()
        {
            DrugName = Drug.DrugName;

            DrugsWithoutFilter = await _dataContext.LoadAllDrugsAsync();

            Drugs = DrugsWithoutFilter.Where(x=>x.DrugName==Drug.DrugName).ToList();

            TradeDrugsWithoutFilter = await _dataContext.LoadAllTradeDrugsAsync();

            TradeDrugs = TradeDrugsWithoutFilter.Where(x=>x.DrugName==Drug.DrugName).ToList();


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
