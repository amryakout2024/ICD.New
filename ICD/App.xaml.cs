using ICD.Models;
using ICD.ViewModels;
using ICD.Views;
using System.Globalization;
using SQLite;

namespace ICD
{
    public partial class App : Application
    {
        private readonly AppShellVM _appShellVM;

        private const string DbName = "ICD1";

        public static string DbPath = Path.Combine(FileSystem.Current.AppDataDirectory, DbName);

        private SQLiteConnection Database = new SQLiteConnection(DbPath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache);

        public App(AppShellVM appShellVM)
        {
            InitializeComponent();

            _appShellVM = appShellVM;

            MainPage = new AppShell(_appShellVM);

            CheckDatabase();
        }

        private async Task CheckDatabase()
        {
            //File.Delete(DbPath);
            ///
            //Shell.Current.GoToAsync($"//{nameof(HomePage)}", animate: true);

            try
            {
                Drug drug = Database.Table<Drug>().Where(x => x.DrugId == 1800).FirstOrDefault();

                TradeDrug tradeDrug = Database.Table<TradeDrug>().Where(x => x.TradeDrugId == 1).FirstOrDefault();

                if (drug != null && tradeDrug != null)
                {
                    Shell.Current.GoToAsync($"//{nameof(HomePage)}", animate: true);
                }
                else
                {
                    //File.Delete(DbPath);

                    Database.Table<Drug>().Delete();

                    Database.Table<TradeDrug>().Delete();

                    Shell.Current.GoToAsync($"//{nameof(LoadingPage)}", animate: true);
                }


            }
            catch (Exception)
            {
                //File.Delete(DbPath);

                Shell.Current.GoToAsync($"//{nameof(LoadingPage)}", animate: true);
            }
        }
    }
}
