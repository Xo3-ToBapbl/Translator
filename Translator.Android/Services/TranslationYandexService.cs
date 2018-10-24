using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Translator.Droid.Services;
using Translator.Extensions;
using Translator.Interfaces;
using Translator.Models;
using Translator.Services;
using Xamarin.Forms;
using Uri = System.Uri;

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
                Scheme = "https",
                Host = "translate.yandex.net",
                Path = "/api/v1.5/tr.json/translate"
            };

            baseQuery = ConstantService.AppKeys.YandexQueryKeys.ToQueryString();
        }


        public async Task<TranslationResponse> GetTranslation(string translatedString)
        {
            if (string.IsNullOrWhiteSpace(translatedString))
                return new TranslationResponse()
                {
                    HasError = true,
                    Message = ConstantService.ErrorMessages.EmptyTranslatedString,
                };

            if (!IsConnected)
                return new TranslationResponse()
                {
                    HasError = true,
                    Message = ConstantService.ErrorMessages.HasNoConnection,
                };

            WebRequest request = GetRequest(translatedString);
            YandexResponce yandexResponce = await GetResponce(request);

            if (yandexResponce.HasError)
            {
                return new TranslationResponse()
                {
                    HasError = yandexResponce.HasError,
                    Message = ConstantService.ErrorMessages.ServerError,
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