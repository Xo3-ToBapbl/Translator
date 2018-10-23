using System;
using System.Collections.Generic;
using System.Text;

namespace Translator.Models
{
    public class TranslationResponse
    {
        public string[] Text { get; set; }
        public bool HasError { get; set; }
        public string Message { get; set; }

        public string TranslationsString
        {
            get
            {
                if (Text != null && Text.Length != 0)
                    return string.Join(", ", Text);

                return string.Empty;
            }
        }
    }
}
