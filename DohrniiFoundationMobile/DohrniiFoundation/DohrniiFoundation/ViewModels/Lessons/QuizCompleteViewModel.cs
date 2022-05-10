using DohrniiFoundation.Helpers;
using DohrniiFoundation.Views.Lessons;
using Microsoft.AppCenter.Crashes;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace DohrniiFoundation.ViewModels.Lessons
{
    /// <summary>
    /// This view model is to bind the UI and functionality
    /// </summary>
    public class QuizCompleteViewModel : ObservableObject
    {
        #region Private Properties
        private bool firstRandomGridVisible;
        private bool secondRandomGridVisible;
        #endregion

        #region Public Properties
        public bool FirstRandomGridVisible
        {
            get { return firstRandomGridVisible; }
            set
            {
                firstRandomGridVisible = value;
                OnPropertyChanged(nameof(FirstRandomGridVisible));
            }
        }
        public bool SecondRandomGridVisible
        {
            get { return secondRandomGridVisible; }
            set
            {
                secondRandomGridVisible = value;
                OnPropertyChanged(nameof(SecondRandomGridVisible));
            }
        }
        public ICommand CompleteCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// This view model is to initialize the command and get random screen of quiz complete
        /// </summary>
        public QuizCompleteViewModel()
        {
            try
            {
                CompleteCommand = new Command(CompleteClick);
                if (Utilities.ChapterQuizRandom == 0)
                {
                    FirstRandomGridVisible = true;
                    SecondRandomGridVisible = false;
                }
                else if (Utilities.ChapterQuizRandom == 1)
                {
                    FirstRandomGridVisible = false;
                    SecondRandomGridVisible = true;
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// This method is to navigate when complete button is click
        /// </summary>
        private void CompleteClick()
        {
            try
            {
                Utilities.IsFromQuizCompletePage = true;
                Application.Current.MainPage.Navigation.PushModalAsync(new LessonChaptersPage());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion
    }
}
