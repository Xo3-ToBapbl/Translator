using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translator.Models;
using Translator.Services;
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

            MainPageDetail.Title = ConstantService.AppName;

            if (user != null)
                MainPageDetail.Title = user.Name;
        }
    }
}