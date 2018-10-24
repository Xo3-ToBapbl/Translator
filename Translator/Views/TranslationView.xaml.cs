using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Translator.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TranslationView : ContentView
    {
        public TranslationView()
        {
            InitializeComponent();
        }
    }
}