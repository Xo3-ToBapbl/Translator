﻿using System.Linq;
using Translator.Core.Enums;
using Translator.Core.Models;
using Translator.ViewModels;

namespace Translator.Extensions
{
    public static class ModelConverterExtension
    {
        public static WordViewModel ToViewModel(this Word word, DetailPageViewModel rootViewModel)
        {
            return new WordViewModel(AddWordTypes.Manual, rootViewModel)
            {
                Id = word.Id,
                Original = word.Original,
                DateAdded = word.DateAdded,
                Translations = word.Translations.Select(item => item.ToViewModel()).ToObservableCollection(),
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
