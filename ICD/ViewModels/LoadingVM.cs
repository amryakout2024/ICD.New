using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ICD.Models;
using ICD.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICD.ViewModels
{
    public partial class LoadingVM(DataContext dataContext): BaseVM
    {
        private readonly DataContext _dataContext = dataContext;

        [RelayCommand]
        private async Task LoadData()
        {
            await _dataContext.LoadAllDrugsAsync();
            await _dataContext.LoadAllTradeDrugsAsync();
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}", animate: true);
        }
    }
}
