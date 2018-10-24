using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private const int throttlePeriod = 1500;
        private string original;
        private string currentTranslation;
        private bool isBusy;
        private ObservableCollection<TranslationViewModel> translations;

        public int Id { get; set; }
        public string Original
        {
            get => original;
            set
            {
                if (value != null)
                {
                    original = value;
                    OnPropertyChanged();

                    if (AddWordTypes == AddWordTypes.RemoteAPI)
                    {
                        if (value != string.Empty)
                            TranslateWithDelay();
                        else
                            Translations.Clear();
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
        public ObservableCollection<TranslationViewModel> Translations
        {
            get => translations;
            set
            {
                if (value == null) return;

                translations = value;
                this.OnPropertyChanged();
            }
        }

        public INavigation Navigation { get; set; }
        public AddWordTypes AddWordTypes { get; private set; }
        public ICommand AddTranslationCommand { get; }
        public ICommand UpdateTranslationCommand { get; }
        public ICommand DeleteTranslationCommand { get; }
        public ICommand SaveWordCommand { get; }
        public ICommand CancelWordCommand { get; }


        public WordViewModel(AddWordTypes addWordTypes)
        {
            translations = new ObservableCollection<TranslationViewModel>();
            AddWordTypes = addWordTypes;

            AddTranslationCommand = new Command(
                execute:()=>
                {
                    AddTranslation(CurrentTranslation);
                },
                canExecute: () =>
                {
                    if (!string.IsNullOrWhiteSpace(CurrentTranslation) &&
                        !Translations.Select(item => item.Value).Contains(CurrentTranslation))
                        return true;

                    CurrentTranslation = string.Empty;
                    return false;
                });
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
                {
                    Translations.Clear();
                    AddTranslation(translation.Text);
                }

                IsBusy = false;
            }
        }

        private void AddTranslation(string translation)
        {
            var translationViewModel = new TranslationViewModel()
            {
                Value = translation,
            };

            this.Translations.Add(translationViewModel);
            CurrentTranslation = string.Empty;
        }
    }
}
