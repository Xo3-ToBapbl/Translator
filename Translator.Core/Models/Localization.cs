using System;
using System.Collections.Generic;
using System.Text;

namespace Translator.Core.Models
{
    public class Localization
    {
        public string Key { get; private set; }
        public string Value { get; private set; }


        public Localization(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
