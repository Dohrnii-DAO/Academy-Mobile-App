using System;
using DohrniiFoundation.Helpers;
using DohrniiFoundation.Resources;
using Microsoft.AppCenter.Crashes;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using DohrniiFoundation.Views.Lessons;

namespace DohrniiFoundation.ViewModels
{
    /// <summary>
    /// View model to binding and handle functionality of the dashboard 
    /// </summary> 
    public class DashboardViewModel : ObservableObject
    {
        #region Properties
        public string Dashboardbg { get; set; } = StringConstant.Dashboardbg;
        public string Dashboardheader { get; set; } = StringConstant.Dashboardheader;
        public string Dashboardheaderimg { get; set; } = StringConstant.Dashboardheaderimg;
        public string Dashboardlearn { get; set; } = StringConstant.Dashboardlearn;
        public string Dashboardlearnimg { get; set; } = StringConstant.Dashboardlearnimg;
        public string Dashboardinvest { get; set; } = StringConstant.Dashboardinvest;
        public string Dashboardinvestimg { get; set; } = StringConstant.Dashboardinvestimg;
        public ICommand LearnTapCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the command
        /// </summary>
        public DashboardViewModel()
        {
            try
            {
                PageTitle = DFResources.WelcomeDhorniiText;
                LearnTapCommand = new Command(LearnClick);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method to click on learn to see other modules
        /// </summary>
        public async void LearnClick()
        {
            try
            {
                Preferences.Set(StringConstant.TabIdKey, 2);
                await Application.Current.MainPage.Navigation.PushModalAsync(new LessonsPage());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion
    }
}
