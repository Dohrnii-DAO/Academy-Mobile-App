using Xamarin.Forms;

namespace DohrniiFoundation.Controls
{
    /// <summary>
    /// Class to inherit the frame and set the properties
    /// </summary>
    public class TopTwoCornerFrame : Frame
    {
        #region Properties
        public new CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
        public static new readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(TopTwoCornerFrame), typeof(CornerRadius), typeof(TopTwoCornerFrame));
        #endregion

        #region Constructor
        public TopTwoCornerFrame()
        {
            base.CornerRadius = 0;
        }
        #endregion       
    }
}
