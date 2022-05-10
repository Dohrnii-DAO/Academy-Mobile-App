using Xamarin.Forms;
using DohrniiFoundation.Controls;
using DohrniiFoundation.Droid.Renderer;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEditor), typeof(CustomEditorRenderer))]
namespace DohrniiFoundation.Droid.Renderer
{
    [System.Obsolete]
    public class CustomEditorRenderer : EditorRenderer
    {
        /// <summary>
        /// This method is used to remove the underline of the editor in iOS
        /// </summary>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                CustomEditor _element = e.NewElement as CustomEditor;
                Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
            }
        }
    }
}