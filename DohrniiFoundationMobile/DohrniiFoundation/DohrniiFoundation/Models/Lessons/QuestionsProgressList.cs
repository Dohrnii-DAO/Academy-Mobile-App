using DohrniiFoundation.Helpers;
using Xamarin.Forms;

namespace DohrniiFoundation.Models.Lessons
{
    /// <summary>
    /// This model class is used to handle the progess of the answer selected of the class questions
    /// </summary>
    public class QuestionsProgressList : ObservableObject
    {
        #region Private Properties
        private double stackWidth;
        private Color progressBarFrameBorderColor;
        private Color progressBarFrameBgColor;
        #endregion

        #region Public Properties
        public string QuestionsName { get; set; }
        public double StackWidth
        {
            get { return stackWidth; }
            set
            {
                stackWidth = value;
                OnPropertyChanged(nameof(StackWidth));
            }
        }
        public Color ProgressBarFrameBorderColor
        {
            get
            {
                return progressBarFrameBorderColor;
            }
            set
            {
                if (progressBarFrameBorderColor != value)
                {
                    progressBarFrameBorderColor = value;
                    OnPropertyChanged(nameof(ProgressBarFrameBorderColor));
                }
            }
        }
        public Color ProgressBarFrameBgColor
        {
            get
            {
                return progressBarFrameBgColor;
            }
            set
            {
                if (progressBarFrameBgColor != value)
                {
                    progressBarFrameBgColor = value;
                    OnPropertyChanged(nameof(ProgressBarFrameBgColor));
                }
            }
        }
        #endregion
    }
}
