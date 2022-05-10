using DohrniiFoundation.Controls;
using DohrniiFoundation.iOS.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEditor), typeof(CustomEditorRenderer))]
namespace DohrniiFoundation.iOS.Renderer {
   public class CustomEditorRenderer : EditorRenderer
    {
        /// <summary>
        /// This method is used to remove the underline of the editor in android
        /// </summary>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                CustomEditor _element = e.NewElement as CustomEditor;
                Control.BackgroundColor = UIKit.UIColor.White;
            }
        }
    }
}