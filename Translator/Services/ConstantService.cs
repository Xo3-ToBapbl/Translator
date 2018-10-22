using System;
using System.Collections.Generic;
using System.Text;

namespace Translator.Services
{
    public static class ConstantService
    {
        public class AppPropertiesKeys
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
    }
}
