using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Translator.Core.Interfaces;
using Translator.Core.Models;
using Xamarin.Auth;

namespace Translator.Core.Services
{
    public class OAuthFacebookService: ILoginService<FacebookResult>
    {
        private bool hasResult;
        private ILoginResult<FacebookResult> loginResult;


        public async Task<ILoginResult<FacebookResult>> LogIn()
        {
            Authenticate();
            await CheckResult();

            return loginResult;
        }

        private void Authenticate()
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
        }

        private void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            loginResult = new FacebookLoginResult(false, e.Message, null);
            hasResult = true;
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
                    string result = await response.GetResponseTextAsync();
                    FacebookResult facebookResult = JsonConvert.DeserializeObject<FacebookResult>(result);
                    loginResult = new FacebookLoginResult(false, "", facebookResult);
                    hasResult = true;
                }
                else
                    OnAuthError(null,
                        new AuthenticatorErrorEventArgs(Constants.ErrorMessages.AuthenticationError));
            }
            else
                OnAuthError(null,
                    new AuthenticatorErrorEventArgs(Constants.ErrorMessages.AuthenticationError));
        }

        private async Task CheckResult()
        {
            while (!hasResult)
                await Task.Delay(1000);
        }
    }
}
