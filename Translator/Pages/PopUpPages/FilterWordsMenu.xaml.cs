using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translator.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Translator.Pages.PopUpPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FilterWordsMenu : PopupPage
	{
		public FilterWordsMenu(DetailPageViewModel viewModel)
		{
			InitializeComponent();

            this.BindingContext = viewModel;
		}
	}
}