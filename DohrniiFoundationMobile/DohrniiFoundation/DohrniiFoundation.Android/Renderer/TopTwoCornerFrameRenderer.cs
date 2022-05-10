using Android.Content;
using Android.Graphics.Drawables;
using DohrniiFoundation.Controls;
using DohrniiFoundation.Droid.Renderer;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using FrameRenderer = Xamarin.Forms.Platform.Android.AppCompat.FrameRenderer;

[assembly: ExportRenderer(typeof(TopTwoCornerFrame), typeof(TopTwoCornerFrameRenderer))]
namespace DohrniiFoundation.Droid.Renderer
{
    /// <summary>
    /// Renderer to set the corner radius of each corner of the frame
    /// </summary>
    public class TopTwoCornerFrameRenderer : FrameRenderer
    {
        public TopTwoCornerFrameRenderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null && Control != null)
            {
                UpdateCornerRadius();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == nameof(TopTwoCornerFrame.CornerRadius) ||
                e.PropertyName == nameof(TopTwoCornerFrame))
            {
                UpdateCornerRadius();
            }
        }

        private void UpdateCornerRadius()
        {
            if (Control.Background is GradientDrawable backgroundGradient)
            {
                var cornerRadius = (Element as TopTwoCornerFrame)?.CornerRadius;
                if (!cornerRadius.HasValue)
                {
                    return;
                }

                var topLeftCorner = Context.ToPixels(cornerRadius.Value.TopLeft);
                var topRightCorner = Context.ToPixels(cornerRadius.Value.TopRight);
                var bottomLeftCorner = Context.ToPixels(cornerRadius.Value.BottomLeft);
                var bottomRightCorner = Context.ToPixels(cornerRadius.Value.BottomRight);

                var cornerRadii = new[]
                {
            topLeftCorner,
            topLeftCorner,

            topRightCorner,
            topRightCorner,

            bottomRightCorner,
            bottomRightCorner,

            bottomLeftCorner,
            bottomLeftCorner,
        };

                backgroundGradient.SetCornerRadii(cornerRadii);
            }
        }

    }
}