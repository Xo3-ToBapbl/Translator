using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Translator.ViewModels
{
    public class DetailPageViewModel: ViewModel
    {
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
    }
}
