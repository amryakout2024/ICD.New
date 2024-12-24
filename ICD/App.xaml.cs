using ICD.Models;
using ICD.ViewModels;
using ICD.Views;
using System.Globalization;
using SQLite;

namespace ICD
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

        }

    }
}
