using Android.Content;
using DohrniiFoundation.Controls;
using DohrniiFoundation.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(NoOutlineEntry), typeof(NoOutlineEntryRenderer))]
namespace DohrniiFoundation.Droid.Renderer
{
    /// <summary>
    /// Renderer to remove the border style 
    /// </summary>
    public class NoOutlineEntryRenderer : EntryRenderer
    {
        public NoOutlineEntryRenderer(Context context) : base(context)
        {
        }

        /// <summary>
        /// Method to remove the border style 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = null;
            }
        }
    }
}