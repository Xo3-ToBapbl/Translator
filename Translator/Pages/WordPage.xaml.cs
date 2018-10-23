using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translator.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Translator.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WordPage : ContentPage
	{
		public WordPage (WordViewModel viewModel)
		{
			InitializeComponent ();

		    viewModel.Navigation = this.Navigation;
		    this.BindingContext = viewModel;
		}
	}
}