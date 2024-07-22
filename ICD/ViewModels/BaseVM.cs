using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICD.ViewModels
{
	public partial class BaseVM:ObservableObject
	{
		[ObservableProperty]
		private bool _isBusy;
	}
}
