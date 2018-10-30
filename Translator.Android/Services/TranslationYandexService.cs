using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Android.Net;
using Newtonsoft.Json;
using Translator.Core.Interfaces;
using Translator.Core.Models;
using Translator.Core.Services;
using Translator.Droid.Services;
using Translator.Extensions;
using Xamarin.Forms;

[assembly: Dependency(typeof(TranslationYandexService))]
namespace Translator.Droid.Services
{
    public class TranslationYandexService : ITranslationRemoteService
    {
        private class YandexResponce
        {
            public string Code { get; set; }
            public string Lang { get; set; }
            public string[] Text { get; set; }
            public bool HasError { get; set; }
            public string Message { get; set; }
        }

        private UriBuilder uriBuilder;
        private string baseQuery;
        private JsonSerializer jsonSerializer;

        private bool IsConnected
        {
            get
            {
                var connectivityManager = (ConnectivityManager) Android.App.Application
                    .Context.GetSystemService(Android.App.Application.ConnectivityService);

                return connectivityManager.ActiveNetworkInfo.IsConnected;
            }
        }


        public TranslationYandexService()
        {
            jsonSerializer = new JsonSerializer();
            uriBuilder = new UriBuilder
            {
                Scheme = Constants.YandexAPI.Scheme,
                Host = Constants.YandexAPI.Host,
                Path = Constants.YandexAPI.Path
            };

            baseQuery = Constants.YandexAPI.QueryKeys.ToQueryString();
        }


        public async Task<TranslationResponse> GetTranslation(string translatedString)
        {
            if (string.IsNullOrWhiteSpace(translatedString))
                return new TranslationResponse()
                {
                    HasError = true,
                    Message = Constants.ErrorMessages.EmptyTranslatedString,
                };

            if (!IsConnected)
                return new TranslationResponse()
                {
                    HasError = true,
                    Message = Constants.ErrorMessages.HasNoConnection,
                };

            WebRequest request = GetRequest(translatedString);
            YandexResponce yandexResponce = await GetResponce(request);

            if (yandexResponce.HasError)
            {
                return new TranslationResponse()
                {
                    HasError = yandexResponce.HasError,
                    Message = Constants.ErrorMessages.ServerError,
                };
            }

            return new TranslationResponse()
            {
                HasError = false,
                Text = yandexResponce.Text.First(),
            };
        }

        private WebRequest GetRequest(string translatedString)
        {
            uriBuilder.Query = baseQuery + translatedString;

            WebRequest request = WebRequest.Create(uriBuilder.Uri);
            request.ContentType = "application/json";
            request.Method = "GET";

            return request;
        }

        private async Task<YandexResponce> GetResponce(WebRequest request)
        {
            var yandexResponce = new YandexResponce();

            try
            {
                var webResponse = await request.GetResponseAsync() as HttpWebResponse;
                StreamReader reader;
                using (reader = new StreamReader(webResponse.GetResponseStream()))
                {
                    using (JsonTextReader jsonTextReader = new JsonTextReader(reader))
                    {
                        yandexResponce = jsonSerializer.Deserialize<YandexResponce>(jsonTextReader);
                    }
                }
            }
            catch (WebException e)
            {
                yandexResponce.HasError = true;
                yandexResponce.Message = string.Format($"{e.Status} {e.Response}");
            }

            return yandexResponce;
        }
    }
}