﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace Translator.Services
{
    public static class ConstantService
    {
        public class AppKeys
        {
            public const string WordsFilterType = "WordsFilterType";
            public static NameValueCollection YandexQueryKeys = new NameValueCollection
            {
                { "key", "trnsl.1.1.20181023T063802Z.4e09c09340d7c4b3.3865608bacb4685a4ef5b1da339c5958d758324b" },
                { "lang", "en-ru" },
                { "text", "" },
            };
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
            public const string ServerError = "Ошибка на сервере. Попробуйте позже";
        }
    }
}
