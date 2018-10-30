using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Translator.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Translator.Pages.MasterPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageMaster : ContentPage
    {
        public MainPageMaster()
        {
            InitializeComponent();

            var viewModel = new MasterPageViewModel(this.Navigation);
            this.BindingContext = viewModel;
        }

        private void MenuItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView) sender).SelectedItem = null;
        }
    }
}