﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FluentValidation.Results;
using Rg.Plugins.Popup.Services;
using Translator.Enums;
using Translator.Extensions;
using Translator.Interfaces;
using Translator.Models;
using Translator.Pages.PopUpPages;
using Translator.ViewModels.Validations;
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


        public WordViewModel(AddWordTypes addWordTypes, DetailPageViewModel rootViewModel)
        {
            translations = new ObservableCollection<TranslationViewModel>();
            RootViewModel = rootViewModel;
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

            DeleteTranslationCommand = new Command<TranslationViewModel>(
                execute: (viewModel) =>
                {
                    Translations.Remove(viewModel);
                });

            UpdateTranslationCommand = new Command<TranslationViewModel>(
                execute: (viewModel) =>
                {
                    CurrentTranslation = viewModel.Value;
                    Translations.Remove(viewModel);
                });

            SaveWordCommand = new Command(
                execute: () =>
                {
                    var wordValidator = new WordViewModelValidator();
                    var validationResult = wordValidator.Validate(this);

                    if (validationResult.IsValid)
                        SaveWord();
                    else
                        ShowPopUpAlert(validationResult.ToString("\n"));
                });

            CancelWordCommand = new Command(
                execute: () =>
                {
                    Navigation.PopModalAsync();
                });
        }

        private async void ShowPopUpAlert(string errorMessage)
        {
            var page = new SaveNewWordPopUpPage(errorMessage);

            await PopupNavigation.Instance.PushAsync(page);
        }

        private async void SaveWord()
        {
            DateAdded = DateTime.Now;

            //if (this.Id == 0)
            //    App.WordsRepository.Add(this.ToModel());
            //else
            //    App.WordsRepository.Update(this.ToModel());

            await Navigation.PopModalAsync();
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
