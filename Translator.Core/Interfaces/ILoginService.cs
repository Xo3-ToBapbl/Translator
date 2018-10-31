using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Translator.Core.Interfaces
{
    public interface ILoginService<T> where T:class
    {
        Task<ILoginResult<T>> LogIn();
    }
}
