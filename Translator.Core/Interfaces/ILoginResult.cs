using System;
using System.Collections.Generic;
using System.Text;

namespace Translator.Core.Interfaces
{
    public interface ILoginResult<T> where T:class
    {
        bool HasError { get; }
        string ErrorMessage { get; }
        T Result { get; }
    }
}
