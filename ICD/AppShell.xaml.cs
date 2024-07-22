using ICD.ViewModels;

namespace ICD
{
    public partial class AppShell : Shell
    {
        private readonly AppShellVM _appShellVM;

        public AppShell(AppShellVM appShellVM)
        {
            InitializeComponent();

            _appShellVM = appShellVM;

            BindingContext = _appShellVM = new AppShellVM();
        }
    }
}
