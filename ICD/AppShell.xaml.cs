using ICD.ViewModels;
using ICD.Models;
using ICD.Views;
using SQLite;

namespace ICD
{
    public partial class AppShell : Shell
    {

        public AppShell()
        {
            InitializeComponent();

            RegisterRoutes();
        }
        private readonly static Type[] _routablePageTypes =
        [
            typeof(LoadingPage),
            typeof(HomePage),
            typeof(DrugDetailPage),
            typeof(AboutPage),
        ];

        private static void RegisterRoutes()
        {
            foreach (var pageType in _routablePageTypes)
            {
                Routing.RegisterRoute(pageType.Name, pageType);
            }
        }

    }
}
