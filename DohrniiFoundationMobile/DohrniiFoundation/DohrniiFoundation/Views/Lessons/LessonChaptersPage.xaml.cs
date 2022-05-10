using Microsoft.AppCenter.Crashes;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DohrniiFoundation.Views.Lessons
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LessonChaptersPage : ContentPage
    {
        #region Constructor
        public LessonChaptersPage()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        /// <summary>
        /// This method is to get the lesson selected command in the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LessonsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                ((ListView)sender).SelectedItem = null;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        /// <summary>
        /// This method is to get the class selected command in the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClassesList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                ((ListView)sender).SelectedItem = null;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion
    }
}