using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Translator.Models;

namespace Translator.Interfaces
{
    public interface ITranslationRemoteService
    {
        Task<TranslationResponse> GetTranslation(string translatedString);
    }
}
