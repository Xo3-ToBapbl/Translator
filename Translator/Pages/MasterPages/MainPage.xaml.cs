using Translator.Core.Models;
using Translator.Core.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Translator.Pages.MasterPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage(User user=null)
        {
            InitializeComponent();

            MainPageDetail.Title = Constants.AppName;

            if (user != null)
                MainPageDetail.Title = user.Name;
        }
    }
}