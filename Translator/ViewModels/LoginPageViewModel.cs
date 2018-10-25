using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Translator.Pages.MasterPages;
using Xamarin.Forms;

namespace Translator.ViewModels
{
    public class LoginPageViewModel: ViewModel
    {
        public ICommand FacebookLoginCommand { get; set; }
        public ICommand CancelCommand { get; set; }


        public LoginPageViewModel()
        {
            FacebookLoginCommand = new Command(
                execute: () =>
            {

            });

            CancelCommand = new Command(
                execute: () =>
                {
                    App.Current.MainPage = new MainPage();
                });
        }
    }
}
