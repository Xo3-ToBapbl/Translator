using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Translator.Core.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Translator.Extensions
{
    [ContentProperty(nameof(Text))]
    public class LocalizationExtension: IMarkupExtension
    {
        private static readonly ResourceManager ResourceManager =new ResourceManager(
            Constants.AppResourcePath,
            IntrospectionExtensions.GetTypeInfo(typeof(LocalizationExtension)).Assembly);

        private readonly CultureInfo cultureInfo;

        public string Text { get; set; }


        public LocalizationExtension()
        {
            cultureInfo = new CultureInfo(App.Localization.Key, false);
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return string.Empty;

            string localization = ResourceManager.GetString(Text, cultureInfo) ?? Text;

            return localization;
        }
    }
}
