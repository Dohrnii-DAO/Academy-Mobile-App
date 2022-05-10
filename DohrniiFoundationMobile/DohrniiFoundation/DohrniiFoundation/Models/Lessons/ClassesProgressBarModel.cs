using DohrniiFoundation.Helpers;
using Xamarin.Forms;

namespace DohrniiFoundation.Models.Lessons
{
    /// <summary>
    /// This model class is to bind the chapters progress
    /// </summary>
    public class ClassesProgressBarModel : ObservableObject
    {
        #region Private Properties
        private Color passedProgressBgFrameColor;
        private Color passedBorderFrameColor;
        private Color borderFrameColor;
        private double stackWidth;
        #endregion

        #region Private Properties
        public string ClassName { get; set; }
        public Color PassedProgressBgFrameColor
        {
            get { return passedProgressBgFrameColor; }
            set
            {
                if (passedProgressBgFrameColor != value)
                {
                    passedProgressBgFrameColor = value;
                    this.OnPropertyChanged(nameof(PassedProgressBgFrameColor));
                }
            }
        }
        public Color PassedBorderFrameColor
        {
            get { return passedBorderFrameColor; }
            set
            {
                if (passedBorderFrameColor != value)
                {
                    passedBorderFrameColor = value;
                    this.OnPropertyChanged(nameof(PassedBorderFrameColor));
                }
            }
        }
        public Color BorderFrameColor
        {
            get { return borderFrameColor; }
            set
            {
                if (borderFrameColor != value)
                {
                    borderFrameColor = value;
                    this.OnPropertyChanged(nameof(BorderFrameColor));
                }
            }
        }
        public double StackWidth
        {
            get { return stackWidth; }
            set
            {
                stackWidth = value;
                this.OnPropertyChanged(nameof(StackWidth));
            }
        }
        #endregion
    }
}
