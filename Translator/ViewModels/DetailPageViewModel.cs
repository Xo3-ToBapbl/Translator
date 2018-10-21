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
using Xamarin.Forms;

namespace Translator.ViewModels
{
    public class DetailPageViewModel: ViewModel
    {
        private List<WordViewModel> allItems;
        private ObservableCollection<WordViewModel> items;

        public ObservableCollection<WordViewModel> Items
        {
            get { return items; }
            set
            {
                if (items != value)
                {
                    items = value;
                    this.OnPropertyChanged();
                }
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
        public ICommand ToolbarFilterCommand { get; set; }
        public ICommand SortWordsCommand { get; set; }


        public DetailPageViewModel()
        {
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

                    PopupNavigation.Instance.PopAsync();
                });
        }


        public void OnAppearing()
        {
            allItems = App.WordsRepository
                .GetAll()
                .Select(word => word.ToViewModel(this))
                .OrderBy(word => word.Original)
                .ToList();

            Items = allItems.ToObservableCollection();
        }

        private void SortWords(WordsFilterTypes wordsFilterTypes)
        {
            switch (wordsFilterTypes)
            {
                case WordsFilterTypes.Alhabetical:
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
                default:
                    break;
            }

            Items = allItems.ToObservableCollection();
        }
    }
}
