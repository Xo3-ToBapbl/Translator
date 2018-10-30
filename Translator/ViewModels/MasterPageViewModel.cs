using System.Windows.Input;
using Translator.Pages;
using Xamarin.Forms;

namespace Translator.ViewModels
{
    public class MasterPageViewModel:ViewModel
    {
        public INavigation Navigation { get; set; }
        public ICommand SettingsCommand { get; set; }


        public MasterPageViewModel(INavigation navigation)
        {
            Navigation = navigation;

            SettingsCommand = new Command(ShowSettingsPage);
        }


        private async void ShowSettingsPage()
        {
            var page = new SettingsPage();

            await Navigation.PushModalAsync(page);
        }
    }
}
