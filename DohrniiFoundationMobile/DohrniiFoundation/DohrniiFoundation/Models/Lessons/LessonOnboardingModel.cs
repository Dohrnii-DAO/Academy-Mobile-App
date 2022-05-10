using DohrniiFoundation.Helpers;

namespace DohrniiFoundation.Models.Lessons
{
    /// <summary>
    /// This model is used to bind the onboarding content
    /// </summary>
    public class LessonOnboardingModel : ObservableObject
    {
        #region Private Properties
        private bool zeroIndexFrameVisible;
        private bool firstIndexFrameVisible;
        private bool secondIndexFrameVisible;
        private bool thirdIndexFrameVisible;
        private bool secondDescriptionVisible;
        private bool secondIndexImageVisible;
        #endregion

        #region Public Properties
        public string Title { get; set; }
        public string Question { get; set; }
        public string ShortTitle { get; set; }
        public string Description { get; set; }
        public string SecondDescription { get; set; }      

        public bool ZeroIndexFrameVisible
        {
            get { return zeroIndexFrameVisible; }
            set
            {
                if (zeroIndexFrameVisible != value)
                {
                    zeroIndexFrameVisible = value;
                    this.OnPropertyChanged(nameof(ZeroIndexFrameVisible));
                }
            }
        }
        public bool FirstIndexFrameVisible
        {
            get { return firstIndexFrameVisible; }
            set
            {
                if (firstIndexFrameVisible != value)
                {
                    firstIndexFrameVisible = value;
                    this.OnPropertyChanged(nameof(FirstIndexFrameVisible));
                }
            }
        }
        public bool SecondIndexFrameVisible
        {
            get { return secondIndexFrameVisible; }
            set
            {
                if (secondIndexFrameVisible != value)
                {
                    secondIndexFrameVisible = value;
                    OnPropertyChanged(nameof(SecondIndexFrameVisible));
                }
            }
        }
        public bool ThirdIndexFrameVisible
        {
            get { return thirdIndexFrameVisible; }
            set
            {
                if (thirdIndexFrameVisible != value)
                {
                    thirdIndexFrameVisible = value;
                    this.OnPropertyChanged(nameof(ThirdIndexFrameVisible));
                }
            }
        }
        public bool SecondDescriptionVisible
        {
            get { return secondDescriptionVisible; }
            set
            {
                if (secondDescriptionVisible != value)
                {
                    secondDescriptionVisible = value;
                    OnPropertyChanged(nameof(SecondDescriptionVisible));
                }
            }
        }
        public bool SecondIndexImageVisible
        {
            get { return secondIndexImageVisible; }
            set
            {
                if (secondIndexImageVisible != value)
                {
                    secondIndexImageVisible = value;
                    this.OnPropertyChanged(nameof(SecondIndexImageVisible));
                }
            }
        }
        #endregion
    }
}
