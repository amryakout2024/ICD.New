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
    public partial class DrugDetailVM:BaseVM
    {
        [ObservableProperty]
        private Drug _drug;

        public async Task Init()
        {

        }
    }
}
