using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Translator.Pages.PopUpPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ErrorsPopUpPage : PopupPage
    {
	    public ErrorsPopUpPage(string errorMessage)
	    {
	        InitializeComponent();

	        labelErrorMessage.Text = errorMessage;
	    }

	    private void BtnClose_Clicked(object sender, EventArgs e)
	    {
	        PopupNavigation.Instance.PopAsync();
	    }
    }
}