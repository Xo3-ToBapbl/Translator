using System.Collections.ObjectModel;

namespace Translator.ViewModels
{
    public class MasterViewModel: ViewModel
    {
        public ObservableCollection<string> MenuItems { get; set; }


        public MasterViewModel()
        {
            MenuItems = new ObservableCollection<string>
            {
                "Добавить",
                "Статистика",
                "Настройка",
            };
        }
    }
}
