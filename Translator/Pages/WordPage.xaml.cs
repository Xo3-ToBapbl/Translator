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
	    private WordViewModel viewModel;


		public WordPage (WordViewModel viewModel)
		{
			InitializeComponent ();

		    viewModel.Navigation = this.Navigation;
		    this.viewModel = viewModel;
		    this.BindingContext = viewModel;
		}


	    private void ImageDeleteTranslation_Tapped(object sender, EventArgs e)
	    {
	        if (e is TappedEventArgs tappedEventArgs)
	            viewModel
	                .DeleteTranslationCommand
	                .Execute(tappedEventArgs.Parameter);
	    }

	    private void LabelTranslation_Tapped(object sender, EventArgs e)
	    {
	        if (e is TappedEventArgs tappedEventArgs)
	            viewModel
	                .UpdateTranslationCommand
	                .Execute(tappedEventArgs.Parameter);
        }
	}
}