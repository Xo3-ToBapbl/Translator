using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace Translator.Services
{
    public static class Constants
    {
        public const string AppName = "Translator";
        public const string AppResourcePath = "Translator.Resources.AppResources";

        public class Localization
        {
            public const string En = "en";
            public const string Ru = "ru";
        }

        public class FacebookAPI
        {
            public const string ClientId = "341627873076993";
            public const string Scope = "email";
            public const string AuthorizeUrl = "https://www.facebook.com/dialog/oauth/";
            public const string RedirectUrl = "https://www.facebook.com/connect/login_success.html";
            public const string DataRequestUrl = "https://graph.facebook.com/me";
        }

        public class YandexAPI
        {
            public const string Scheme = "https";
            public const string Host = "translate.yandex.net";
            public const string Path = "/api/v1.5/tr.json/translate";
            public static NameValueCollection QueryKeys = new NameValueCollection
            {
                { "key", "trnsl.1.1.20181023T063802Z.4e09c09340d7c4b3.3865608bacb4685a4ef5b1da339c5958d758324b" },
                { "lang", "en-ru" },
                { "text", "" },
            };
        }

        public class AppKeys
        {
            public const string WordsFilterType = "WordsFilterType";
        }

        public class DataBase
        {
            public const string Name = "MyDictionary.db";

            public class TableNames
            {
                public const string OriginalWords = "OriginalWords";
                public const string Translations = "Translations";
            }
        }

        public class Opacityes
        {
            public const double HalfTransperent = 0.5;
            public const double FullVisible = 1;
        }

        public class ErrorMessages
        {
            public const string ServerError = "Серверная ошибка. Попробуйте позже.";
            public const string HasNoConnection = "Отсутствует подключение к сети.";
            public const string EmptyTranslatedString = "Вы пытаетесь перевести пустую строку.";
            public const string EmptyWord = "Вы не ввели слово.";
            public const string EmptyTranslation = "Вы не ввели перевод.";
            public const string WordAlreadyExist = "Данное слово уже содержится в словаре.";
            public const string AuthenticationError = "Ошибка аутентификации.";
        }
    }
}
