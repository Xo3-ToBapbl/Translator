using System;
using System.Windows.Input;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Translator.Core.Interfaces;
using Translator.Core.Models;
using Translator.Core.Services;
using Translator.Pages.MasterPages;
using Translator.Pages.PopUpPages;
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
                        Constants.FacebookAPI.ClientId,
                        Constants.FacebookAPI.Scope,
                        new Uri(Constants.FacebookAPI.AuthorizeUrl),
                        new Uri(Constants.FacebookAPI.RedirectUrl),
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
                var request = new OAuth2Request("GET", new Uri(Constants.FacebookAPI.DataRequestUrl), null, e.Account);
                var response = await request.GetResponseAsync();
                if (response != null)
                {
                    string userJson = await response.GetResponseTextAsync();

                    User user = JsonConvert.DeserializeObject<User>(userJson);
                    App.Current.MainPage = new MainPage(user);
                }
                else
                    OnAuthError(null,
                        new AuthenticatorErrorEventArgs(Constants.ErrorMessages.AuthenticationError));
            }
            else
                OnAuthError(null,
                    new AuthenticatorErrorEventArgs(Constants.ErrorMessages.AuthenticationError));
        }

        private async void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            var page = new ErrorsPopUpPage(e.Message);

            await PopupNavigation.Instance.PushAsync(page);
        }
    }
}

