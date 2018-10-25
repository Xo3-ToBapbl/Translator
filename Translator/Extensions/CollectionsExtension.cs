using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace Translator.Extensions
{
    public static class CollectionsExtension
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
        {
            return new ObservableCollection<T>(source);
        }

        public static string ToQueryString(this NameValueCollection source)
        {
            var array = (from key in source.AllKeys
                            from value in source.GetValues(key)
                            select $"{HttpUtility.UrlEncode(key)}={HttpUtility.UrlEncode(value)}")
                                .ToArray();

            return "?" + string.Join("&", array);
        }
    }
}
