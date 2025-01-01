using ICD.ViewModels;
using UraniumUI.Pages;

namespace ICD.Views;

public partial class DrugDetailPage : UraniumContentPage
{
    private readonly DrugDetailVM _drugDetailVM;

    public DrugDetailPage(DrugDetailVM drugDetailVM)
	{
		InitializeComponent();
        
        _drugDetailVM = drugDetailVM;

        BindingContext = _drugDetailVM;

    }
}