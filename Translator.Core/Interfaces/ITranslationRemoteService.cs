using System.Threading.Tasks;
using Translator.Core.Models;

namespace Translator.Core.Interfaces
{
    public interface ITranslationRemoteService
    {
        Task<TranslationResponse> GetTranslation(string translatedString);
    }
}
