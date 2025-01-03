using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ICD.Models;
using ICD.Views;
using SQLite;
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
        private const string DbName = "ICD508";

        public static string DbPath = Path.Combine(FileSystem.Current.AppDataDirectory, DbName);

        private SQLiteConnection Database = new SQLiteConnection(DbPath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache);

        private readonly DataContext _dataContext = dataContext;

        public async Task Init()
        {
            try
            {
                //File.Delete(DbPath);

                Drug drug = Database.Table<Drug>().Where(x => x.DrugId == 6000).FirstOrDefault();
                TradeDrug tradeDrug = Database.Table<TradeDrug>().Where(x => x.TradeDrugId == 5000).FirstOrDefault();

                if (drug != null && tradeDrug != null)
                {
                    await GoToAsyncWithShell(nameof(HomePage), animate: true);
                }
                else
                {
                    Database.Table<Drug>().Delete();
                    Database.Table<TradeDrug>().Delete();
                }

            }
            catch (Exception)
            {

            }
        }
        [RelayCommand]
        private async Task LoadData()
        {
            await _dataContext.LoadAllDrugsAsync();
            await _dataContext.LoadAllTradeDrugsAsync();
            await GoToAsyncWithShell(nameof(HomePage), animate: true);
        }
    }
}
