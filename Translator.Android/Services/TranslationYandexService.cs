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


        public TranslationYandexService()
        {
            jsonSerializer = new JsonSerializer();
            uriBuilder = new UriBuilder
            {
                Scheme = "https",
                Host = "translate.yandex.net",
                Path = "/api/v1.5/tr.json/translate"
            };

            baseQuery =
                "?key=trnsl.1.1.20181023T063802Z.4e09c09340d7c4b3.3865608bacb4685a4ef5b1da339c5958d758324b&lang=en-ru&text=";
        }


        public async Task<TranslationResponse> GetTranslation(string translatedString)
        {
            if (string.IsNullOrEmpty(translatedString) || !IsConnected) return null;

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
                Text = yandexResponce.Text,
            };
        }

        private bool IsConnected
        {
            get
            {
                var connectivityManager = (ConnectivityManager) Android.App.Application
                    .Context.GetSystemService(Android.App.Application.ConnectivityService);

                return connectivityManager.ActiveNetworkInfo.IsConnected;
            }
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