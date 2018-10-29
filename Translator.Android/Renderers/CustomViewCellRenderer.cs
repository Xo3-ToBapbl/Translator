using Translator.Controls;
using Translator.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomViewCell), typeof(CustomViewCellRenderer))]
namespace Translator.Droid.Renderers
{
    public class CustomViewCellRenderer: ViewCellRenderer
    {
    }
}