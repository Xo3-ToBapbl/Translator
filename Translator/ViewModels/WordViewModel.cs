using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Translator.Enums;
using Translator.Interfaces;
using Translator.Models;
using Xamarin.Forms;

namespace Translator.ViewModels
{
    public class WordViewModel : ViewModel
    {
        private const int throttlePeriod = 1000;
        private string original;
        private string currentTranslation;
        private bool isBusy;
        private List<TranslationViewModel> translations;

        public int Id { get; set; }
        public string Original
        {
            get => original;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    original = value;
                    OnPropertyChanged();

                    if (AddWordTypes == AddWordTypes.RemoteAPI)
                    {
                        TranslateWithDelay();
                    }
                }
            }
        }
        public string CurrentTranslation
        {
            get => currentTranslation;
            set
            {
                if (value != null)
                {
                    currentTranslation = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool IsBusy
        {
            get => isBusy;
            set
            {
                if (value != isBusy)
                {
                    isBusy = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateTime DateAdded { get; set; }
        public List<TranslationViewModel> Translations
        {
            get
            {
                if (translations == null)
                    translations = new List<TranslationViewModel>();

                return translations;
            }
            set
            {
                if (value != null && value != translations)
                    translations = value;
            }
        }
        public string TranslationsString
        {
            get
            {
                if (Translations.Count != 0)
                    return string.Join(", ", Translations);

                return string.Empty;
            }
        }
        public DetailPageViewModel RootViewModel { get; set; }

        public INavigation Navigation { get; set; }
        public AddWordTypes AddWordTypes { get; private set; }
        public ICommand AddTranslationCommand { get; }
        public ICommand UpdateTranslationCommand { get; }
        public ICommand DeleteTranslationCommand { get; }
        public ICommand SaveWordCommand { get; }
        public ICommand CancelWordCommand { get; }


        public WordViewModel(AddWordTypes addWordTypes)
        {
            AddWordTypes = addWordTypes;


        }


        private async void TranslateWithDelay()
        {
            var beforeTranslating = Original;
            await Task.Delay(throttlePeriod);

            if (beforeTranslating == Original)
            {
                IsBusy = true;
                TranslationResponse translation = await DependencyService
                    .Get<ITranslationRemoteService>()
                    .GetTranslation(Original);

                if (!translation.HasError)
                    CurrentTranslation = translation.Text;

                IsBusy = false;
            }
        }
    }
}
