using CoreAnimation;
using CoreGraphics;
using DohrniiFoundation.Controls;
using DohrniiFoundation.iOS.Renderer;
using Foundation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TopTwoCornerFrame), typeof(TopTwoCornerFrameRenderer))]
namespace DohrniiFoundation.iOS.Renderer
{
    /// <summary>
    /// Renderer to set the corner radius of each corner of the frame
    /// </summary>
    public class TopTwoCornerFrameRenderer: FrameRenderer
    {
        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            UpdateCornerRadius();
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

        // A very basic way of retrieving same one value for all of the corners
        private double RetrieveCommonCornerRadius(CornerRadius cornerRadius)
        {
            var commonCornerRadius = cornerRadius.TopLeft;
            if (commonCornerRadius <= 0)
            {
                commonCornerRadius = cornerRadius.TopRight;
                if (commonCornerRadius <= 0)
                {
                    commonCornerRadius = cornerRadius.BottomLeft;
                    if (commonCornerRadius <= 0)
                    {
                        commonCornerRadius = cornerRadius.BottomRight;
                    }
                }
            }
            return commonCornerRadius;
        }

        private UIRectCorner RetrieveRoundedCorners(CornerRadius cornerRadius)
        {
            UIRectCorner roundedCorners = default;
            if (cornerRadius.TopLeft > 0)
            {
                roundedCorners |= UIRectCorner.TopLeft;
            }
            if (cornerRadius.TopRight > 0)
            {
                roundedCorners |= UIRectCorner.TopRight;
            }
            if (cornerRadius.BottomLeft > 0)
            {
                roundedCorners |= UIRectCorner.BottomLeft;
            }
            if (cornerRadius.BottomRight > 0)
            {
                roundedCorners |= UIRectCorner.BottomRight;
            }
            return roundedCorners;
        }
        private void UpdateCornerRadius()
        {
            var cornerRadius = (Element as TopTwoCornerFrame)?.CornerRadius;
            if (!cornerRadius.HasValue)
            {
                return;
            }
            var roundedCornerRadius = RetrieveCommonCornerRadius(cornerRadius.Value);
            if (roundedCornerRadius <= 0)
            {
                return;
            }
            UIRectCorner roundedCorners = RetrieveRoundedCorners(cornerRadius.Value);
            UIBezierPath path = UIBezierPath.FromRoundedRect(Bounds, roundedCorners, new CGSize(roundedCornerRadius, roundedCornerRadius));
            CAShapeLayer mask = new CAShapeLayer { Path = path.CGPath };
            NativeView.Layer.Mask = mask;
        }
    }
}