using DohrniiFoundation.Controls;
using DohrniiFoundation.iOS.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NoOutlineEntry), typeof(NoOutlineEntryRenderer))]
namespace DohrniiFoundation.iOS.Renderer
{
    /// <summary>
    /// Renderer to remove the border style and and change the cursor color of entry
    /// </summary>
  public class NoOutlineEntryRenderer: EntryRenderer
    {
        public NoOutlineEntryRenderer()
        {
        }
        /// <summary>
        /// Method to remove the border style and change the cursor color
        /// </summary>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.TintColor = Color.FromHex("#36C0BE").ToUIColor();
                Control.BorderStyle = UIKit.UITextBorderStyle.None;
            }
        }
    }
}