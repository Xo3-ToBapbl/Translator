namespace Translator.ViewModels
{
    public class TranslationViewModel : ViewModel
    {
        private string value;


        public int Id { get; set; }

        public string Value
        {
            get => this.value;
            set
            {
                if (value != null)
                {
                    this.value = value;
                    OnPropertyChanged();
                }
            }
        }


        public override string ToString()
        {
            return Value;
        }
    }
}
