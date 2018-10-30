using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translator.Services;
using Translator.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Translator.Pages.PopUpPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddNewWordMenu : PopupPage
	{
	    private DetailPageViewModel viewModel;

        public AddNewWordMenu (DetailPageViewModel viewModel)
		{
			InitializeComponent ();

		    this.viewModel = viewModel;
		    this.BindingContext = viewModel;
		}

	    protected override void OnDisappearing()
	    {
	        base.OnDisappearing();

	        viewModel.AddNewWordButtonOpacity = Constants.Opacityes.HalfTransperent;
	    }
	}
}