using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using Translator.Controls;
using Translator.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace Translator.Droid.Renderers
{
    class CustomEntryRenderer: EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetPadding(15, 0, 0, 0);
                Control.Gravity = GravityFlags.CenterVertical;
                Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
            }
        }
    }
}