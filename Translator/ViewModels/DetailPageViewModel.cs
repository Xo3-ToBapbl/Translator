using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Translator.Enums;
using Translator.Extensions;
using Translator.Pages.PopUpPages;
using Translator.Services;
using Xamarin.Forms;

namespace Translator.ViewModels
{
    public class DetailPageViewModel: ViewModel
    {
        private List<WordViewModel> allItems;
        private ObservableCollection<WordViewModel> items;
        private double addNewWordButtonOpacity;

        public ObservableCollection<WordViewModel> Items
        {
            get => items;
            set
            {
                if (items == value) return;

                items = value;
                this.OnPropertyChanged();
            }
        }
        public string SearchText
        {
            set
            {
                if (value!= null)
                {
                    Items = allItems
                        .Where(item => item.Original.Contains(value))
                        .ToObservableCollection();
                }
            }
        }

        public double AddNewWordButtonOpacity
        {
            get => addNewWordButtonOpacity;
            set
            {
                if (value == addNewWordButtonOpacity) return;

                addNewWordButtonOpacity = value;
                OnPropertyChanged();
            }
        }

        public ICommand ToolbarFilterCommand { get; set; }
        public ICommand SortWordsCommand { get; set; }
        public ICommand AddNewWordButtonCommand { get; set; }
        public ICommand AddNewWordCommand { get; set; }


        public DetailPageViewModel()
        {
            addNewWordButtonOpacity = ConstantService.Opacityes.HalfTransperent;

            AddNewWordButtonCommand = new Command(
                execute: async () =>
                {
                    AddNewWordButtonOpacity = ConstantService.Opacityes.FullVisible;

                    var addNewWordMenu = new AddNewWordMenu(this);
                    await PopupNavigation.Instance.PushAsync(addNewWordMenu);
                });

            AddNewWordCommand = new Command<AddWordTypes>(
                execute: async (addWordsType) =>
                {
                    await PopupNavigation.Instance.PopAsync();
                });

            ToolbarFilterCommand = new Command(
                execute: async () =>
                {
                    var filterWordsMenu = new FilterWordsMenu(this);
                    await PopupNavigation.Instance.PushAsync(filterWordsMenu);
                });

            SortWordsCommand = new Command<WordsFilterTypes>(
                execute: (wordsFilterTypes) =>
                {
                    SortWords(wordsFilterTypes);
                    App.Current
                       .Properties[ConstantService.AppPropertiesKeys.WordsFilterType] = wordsFilterTypes.ToString();

                    PopupNavigation.Instance.PopAsync();
                });
        }


        public void OnAppearing()
        {
            allItems = App.WordsRepository
                .GetAll()
                .Select(word => word.ToViewModel(this))
                .ToList();

            SortWords(GetWordsFilterType());
        }


        private WordsFilterTypes GetWordsFilterType()
        {
            string wordsFilterKey = ConstantService.AppPropertiesKeys.WordsFilterType;
            if (!App.Current.Properties.TryGetValue(wordsFilterKey, out var wordsFilter))
                return WordsFilterTypes.None;

            string wordsFilterString = (string) wordsFilter;
            return Enum.TryParse(wordsFilterString, true, out WordsFilterTypes wordsFilterType)
                ? wordsFilterType
                : WordsFilterTypes.None;
        }

        private void SortWords(WordsFilterTypes wordsFilterTypes)
        {
            switch (wordsFilterTypes)
            {
                case WordsFilterTypes.Alphabetical:
                    allItems = allItems
                        .OrderBy(item => item.Original)
                        .ToList();
                    break;
                case WordsFilterTypes.AlphabeticalDescending:
                    allItems = allItems
                        .OrderByDescending(item => item.Original)
                        .ToList();
                    break;
                case WordsFilterTypes.Date:
                    allItems = allItems
                        .OrderBy(item => item.DateAdded)
                        .ToList();
                    break;
                case WordsFilterTypes.DateDescending:
                    allItems = allItems
                        .OrderByDescending(item => item.DateAdded)
                        .ToList();
                    break;
                case WordsFilterTypes.None:
                    break;
                default:
                    break;
            }

            Items = allItems.ToObservableCollection();
        }
    }
}
