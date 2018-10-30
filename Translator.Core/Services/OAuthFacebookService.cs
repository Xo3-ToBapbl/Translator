using System;
using System.Collections.Generic;
using System.Text;
using Translator.Services;
using Xamarin.Auth;

namespace Translator.Core.Services
{
    public class OAuthFacebookService
    {
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
            throw new NotImplementedException();
        }

        private void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {

        }
    }
}
