using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;
using Translator.Services;
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
            cultureInfo = new CultureInfo(App.Localization);
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
