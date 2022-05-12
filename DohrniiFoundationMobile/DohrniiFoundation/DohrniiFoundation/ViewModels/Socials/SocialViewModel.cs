using DohrniiFoundation.Helpers;
using DohrniiFoundation.IServices;
using DohrniiFoundation.Models.MockData;
using DohrniiFoundation.Services;
using DohrniiFoundation.Views.Socials;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DohrniiFoundation.ViewModels.Socials
{
    /// <summary>
    /// View model to bind and implement the functionality of the social screen
    /// </summary>
    public class SocialViewModel : ObservableObject
    {
        #region Private Properties
        private static IAPIService aPIService;
        private static IMockDataService mockDataService;
        private ObservableCollection<LeaderBoard> leaderBoards;
        #endregion

        #region Public Properties
        public ICommand TestingCommand { get; set; }
        public ObservableCollection<LeaderBoard> LeaderBoards
        {
            get { return leaderBoards; }
            set
            {
                if (leaderBoards != value)
                {
                    leaderBoards = value;
                    this.OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the command
        /// </summary>
        public SocialViewModel()
        {
            try
            {
                TestingCommand = new Command(TestingClick);
                aPIService = new APIServices();
                mockDataService = new MockDataService();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// This method is used handle the navigation when click on back
        /// </summary>
        private async void TestingClick()
        {
            try
            {
                IsLoading = true;
                await Application.Current.MainPage.Navigation.PushModalAsync(new TestingFontPage());
                IsLoading = false;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        public async Task GetLeaders()
        {
            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = true;
                });
                var leaders = await mockDataService.GetLeaderboard("Today");
                LeaderBoards = new ObservableCollection<LeaderBoard>(leaders);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            finally
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = false;
                });
            }
        }
        #endregion
    }
}
