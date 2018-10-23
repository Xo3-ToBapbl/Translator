using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Translator.Enums;
using Translator.Models;
using Translator.ViewModels;

namespace Translator.Extensions
{
    public static class ModelConverterExtension
    {
        public static WordViewModel ToViewModel(this Word word, DetailPageViewModel rootViewModel)
        {
            return new WordViewModel(AddWordTypes.Manual)
            {
                Id = word.Id,
                Original = word.Original,
                DateAdded = word.DateAdded,
                Translations = word.Translations.Select(item => item.ToViewModel()).ToList(),
                RootViewModel = rootViewModel,
            };
        }

        public static Word ToModel(this WordViewModel viewModel)
        {
            return new Word
            {
                Id = viewModel.Id,
                Original = viewModel.Original,
                DateAdded = viewModel.DateAdded,
                Translations = viewModel.Translations.Select(item => item.ToModel()).ToList(),
            };
        }


        public static TranslationViewModel ToViewModel(this Translation translation)
        {
            return new TranslationViewModel
            {
                Id = translation.Id,
                Value = translation.Value,
            };
        }

        public static Translation ToModel(this TranslationViewModel viewModel)
        {
            return new Translation
            {
                Id = viewModel.Id,
                Value = viewModel.Value,
            };
        }
    }
}
