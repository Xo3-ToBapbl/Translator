using System;
using System.Collections.Generic;
using System.Text;

namespace Translator.ViewModels
{
    public class WordViewModel : ViewModel
    {
        private string original;
        private List<TranslationViewModel> translations;


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
    }
}
