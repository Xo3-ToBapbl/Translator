using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Translator.Models;
using Translator.Pages.MasterPages;
using Translator.Pages.PopUpPages;
using Translator.Services;
using Xamarin.Auth;
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
                    var authenticator = new OAuth2Authenticator(
                        ConstantService.FacebookAPI.ClientId,
                        ConstantService.FacebookAPI.Scope,
                        new Uri(ConstantService.FacebookAPI.AuthorizeUrl),
                        new Uri(ConstantService.FacebookAPI.RedirectUrl),
                        null,
                        false);

                    authenticator.Completed += OnAuthCompleted;
                    authenticator.Error += OnAuthError;

                    var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
                    presenter.Login(authenticator);
                });

            CancelCommand = new Command(
                execute: () =>
                {
                    App.Current.MainPage = new MainPage();
                });
        }

        private async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (sender is OAuth2Authenticator authenticator)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            if (e.IsAuthenticated)
            {
                var request = new OAuth2Request("GET", new Uri(ConstantService.FacebookAPI.DataRequestUrl), null,e.Account);
                var response = await request.GetResponseAsync();
                if (response != null)
                {
                    string userJson = await response.GetResponseTextAsync();

                    User user = JsonConvert.DeserializeObject<User>(userJson);
                    App.Current.MainPage = new MainPage(user);
                }
                else
                    OnAuthError(null, 
                        new AuthenticatorErrorEventArgs(ConstantService.ErrorMessages.AuthenticationError));
            }
            else
                OnAuthError(null, 
                    new AuthenticatorErrorEventArgs(ConstantService.ErrorMessages.AuthenticationError));
        }

        private async void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            var page = new ErrorsPopUpPage(e.Message);

            await PopupNavigation.Instance.PushAsync(page);
        }

    }
}
