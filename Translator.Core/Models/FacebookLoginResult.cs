using System;
using System.Collections.Generic;
using System.Text;
using Translator.Core.Interfaces;

namespace Translator.Core.Models
{
    public class FacebookLoginResult : ILoginResult<FacebookResult>
    {
        private readonly bool hasError;
        private readonly string errorMessage;
        private readonly FacebookResult result;

        public bool HasError => hasError;
        public string ErrorMessage => errorMessage;
        public FacebookResult Result => result;


        public FacebookLoginResult(bool hasError, string errorMessage, FacebookResult result)
        {
            this.hasError = hasError;
            this.errorMessage = errorMessage;
            this.result = result;
        }
    }
}
