using Rg.Plugins.Popup.Pages;
using Translator.ViewModels;
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