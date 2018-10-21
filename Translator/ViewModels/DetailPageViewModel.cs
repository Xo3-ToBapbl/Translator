using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Translator.Extensions;

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


        public DetailPageViewModel()
        {

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
    }
}
