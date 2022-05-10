using DohrniiFoundation.Helpers;
using Xamarin.Forms;

namespace DohrniiFoundation.Models.Lessons
{
    /// <summary>
    /// This model class is to bind the progress model 
    /// </summary>
    public class ProgressBarModel : ObservableObject
    {
        #region Private Properties
        private Color borderCircleFrameColor;
        private Color boxviewFrameColor;
        private Color frameCircleBgColor;
        private double stackWidth;
        #endregion

        #region Public Properties
        public string LessonsName { get; set; }
        public double StackWidth
        {
            get { return stackWidth; }
            set
            {
                stackWidth = value;
                OnPropertyChanged(nameof(StackWidth));
            }
        }
        public Color BorderCircleFrameColor
        {
            get { return borderCircleFrameColor; }
            set
            {
                if (borderCircleFrameColor != value)
                {
                    borderCircleFrameColor = value;
                    OnPropertyChanged(nameof(BorderCircleFrameColor));
                }
            }
        }
        public Color BoxviewFrameColor
        {
            get { return boxviewFrameColor; }
            set
            {
                if (boxviewFrameColor != value)
                {
                    boxviewFrameColor = value;
                    OnPropertyChanged(nameof(BoxviewFrameColor));
                }
            }
        }
        public Color FrameCircleBgColor
        {
            get { return frameCircleBgColor; }
            set
            {
                if (frameCircleBgColor != value)
                {
                    frameCircleBgColor = value;
                    OnPropertyChanged(nameof(FrameCircleBgColor));
                }
            }
        }
        #endregion
    }
}
