using System.Collections;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using Translator.Core.Models;
using Translator.Pages.MasterPages;
using Translator.Resources;
using Xamarin.Forms;

namespace Translator.ViewModels
{
    public class SettingsPageViewModel: ViewModel
    {
        private Localization choosedLangauge;

        public ObservableCollection<Localization> Languages { get; set; }

        public Localization ChoosedLanguage
        {
            get => choosedLangauge;
            set
            {
                if (value != null)
                {
                    if (value.Key != choosedLangauge.Key)
                    {
                        choosedLangauge = value;
                        ChangeAppLanguage(value);
                        OnPropertyChanged();
                    }

                    CloseSettingsPage();
                }
            }
        }

        public INavigation Navigation { get; set; }
        public ICommand CancelSettingsCommand { get; set; }


        public SettingsPageViewModel(INavigation navigation)
        {
            choosedLangauge = App.Localization;
            Navigation = navigation;
            Languages = new ObservableCollection<Localization>();

            var resourceSet = AppLanguages.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true);
            foreach (DictionaryEntry entry in resourceSet)
            {
                Languages.Add(new Localization(
                    entry.Key.ToString(), entry.Value.ToString()));
            }

            CancelSettingsCommand = new Command(CloseSettingsPage);
        }

        private void ChangeAppLanguage(Localization localization)
        {
            App.Localization = localization;
            App.Current.Properties[nameof(Localization)] = localization;
            App.Current.MainPage = new MainPage();
        }

        private async void CloseSettingsPage()
        {
            await Navigation.PopModalAsync();
        }
    }
}
