using System;
using DohrniiFoundation.Helpers;
using Microsoft.AppCenter.Crashes;
using Xamarin.Essentials;
using Xamarin.Forms;
using DohrniiFoundation.Views.Lessons;
using DohrniiFoundation.Views.More;
using DohrniiFoundation.Views.Socials;

namespace DohrniiFoundation.ViewModels
{
    /// <summary>
    /// View model to handle the functionality of the custom tabbed view
    /// </summary>
    public class TabbedViewViewModel : ObservableObject
    {
        #region Variables
        //private string homeTabSelected = StringConstant.HomeTabSelected;
        //private string lessonsTabSelected = StringConstant.LessonTabUnselected;
        //private string investorsTabSelected = StringConstant.InvestorTabUnselected;

        private Command homeTabCommand;
        private Command lessonsTabCommand;
        private Command investorsTabCommand;
        private Command moreTabCommand;
        private Color homeTextColor = (Color)Application.Current.Resources["GrayTextColor"];
        private Color lessonsTextColor = (Color)Application.Current.Resources["GrayTextColor"];
        private Color strategiesTextColor = (Color)Application.Current.Resources["GrayTextColor"];
        private Color moreTextColor = (Color)Application.Current.Resources["GrayTextColor"];
        private string homeIcon;
        private string lessonsIcon;
        private string strategiesIcon;
        private string moreIcon;
        #endregion

        #region Properties
        //public string HomeTabSelected
        //{
        //    get
        //    {
        //        return homeTabSelected;
        //    }
        //    set
        //    {
        //        if (homeTabSelected != value)
        //        {
        //            homeTabSelected = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}
        //public string LessonsTabSelected
        //{
        //    get
        //    {
        //        return lessonsTabSelected;
        //    }
        //    set
        //    {
        //        if (lessonsTabSelected != value)
        //        {
        //            lessonsTabSelected = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}
        //public string InvestorsTabSelected
        //{
        //    get
        //    {
        //        return investorsTabSelected;
        //    }
        //    set
        //    {
        //        if (investorsTabSelected != value)
        //        {
        //            investorsTabSelected = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}
        public Color HomeTextColor
        {
            get
            {
                return homeTextColor;
            }
            set
            {
                if (homeTextColor != value)
                {
                    homeTextColor = value;
                    OnPropertyChanged();
                }
            }
        }
        public Color LessonsTextColor
        {
            get
            {
                return lessonsTextColor;
            }
            set
            {
                if (lessonsTextColor != value)
                {
                    lessonsTextColor = value;
                    OnPropertyChanged();
                }
            }
        }
        public Color StrategiesTextColor
        {
            get
            {
                return strategiesTextColor;
            }
            set
            {
                if (strategiesTextColor != value)
                {
                    strategiesTextColor = value;
                    OnPropertyChanged();
                }
            }
        }
        public Color MoreTextColor
        {
            get
            {
                return moreTextColor;
            }
            set
            {
                if (moreTextColor != value)
                {
                    moreTextColor = value;
                    OnPropertyChanged();
                }
            }
        }
        public string HomeIcon
        {
            get
            {
                return homeIcon;
            }
            set
            {
                if (homeIcon != value)
                {
                    homeIcon = value;
                    OnPropertyChanged();
                }
            }
        }
        public string LessonsIcon
        {
            get
            {
                return lessonsIcon;
            }
            set
            {
                if (lessonsIcon != value)
                {
                    lessonsIcon = value;
                    OnPropertyChanged();
                }
            }
        }
        public string StrategiesIcon
        {
            get
            {
                return strategiesIcon;
            }
            set
            {
                if (strategiesIcon != value)
                {
                    strategiesIcon = value;
                    OnPropertyChanged();
                }
            }
        }
        public string MoreIcon
        {
            get
            {
                return moreIcon;
            }
            set
            {
                if (moreIcon != value)
                {
                    moreIcon = value;
                    OnPropertyChanged();
                }
            }
        }
        public Command HomeTabCommand
        {
            get
            {
                try
                {
                    if (homeTabCommand == null)
                        homeTabCommand = new Command(() =>
                        {
                            HomeIcon = StringConstant.HomeTabSelected;
                            HomeTextColor = (Color)Application.Current.Resources["BlackTextColor"];
                            Preferences.Set(StringConstant.TabIdKey, 1);
                            Application.Current.MainPage = new SocialPage();
                        });
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                }
                return homeTabCommand;
            }
        }
        public Command LessonsTabCommand
        {
            get
            {
                try
                {
                    if (lessonsTabCommand == null)
                        lessonsTabCommand = new Command(() =>
                        {
                            LessonsIcon = StringConstant.LessonTabSelected;
                            LessonsTextColor = (Color)Application.Current.Resources["BlackTextColor"];
                            Preferences.Set(StringConstant.TabIdKey, 2);
                            Application.Current.MainPage = new LessonsPage();

                            //REMARK: Handle the Lessons onboarding functionality should be open once for the user
                            //var isLessonOpened = Preferences.Get(StringConstant.IsLessonOnboarding, 0);
                            //if (isLessonOpened == 0)
                            //{
                            //    Preferences.Set(StringConstant.IsLessonOnboarding, 1);
                            //    Application.Current.MainPage.Navigation.PushPopupAsync(new LessonOnboardingPopUpPage());
                            //}
                        });
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                }
                return lessonsTabCommand;
            }
        }
        /// <summary>
        /// Gets command is used to handle the investors profile tabbed click and navigation
        /// </summary>
        public Command InvestorsTabCommand
        {
            get
            {
                try
                {
                    if (investorsTabCommand == null)
                    {
                        investorsTabCommand = new Command(() =>
                        {
                            //StrategiesIcon = StringConstant.StrategiesTabSelected;
                            //StrategiesTextColor = (Color)Application.Current.Resources["BlackTextColor"];
                            //Preferences.Set(StringConstant.TabIdKey, 3);
                            //Application.Current.MainPage = new InvestorsProfilePage();
                            //Preferences.Set(StringConstant.TabIdKey, 3);
                            //Application.Current.MainPage = new StrategiesPage();
                        });
                    }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                }
                return investorsTabCommand;
            }
        }
        /// <summary>
        /// Gets command is used to handle the strategies tabbed click and navigation
        /// </summary>
        public Command MoreTabCommand
        {
            get
            {
                try
                {
                    if (moreTabCommand == null)
                        moreTabCommand = new Command(() =>
                        {
                            MoreIcon = StringConstant.MoreSelected;
                            MoreTextColor = (Color)Application.Current.Resources["BlackTextColor"];
                            Preferences.Set(StringConstant.TabIdKey, 4);
                            Application.Current.MainPage = new ProfileMorePage();
                            //Preferences.Set(StringConstant.TabIdKey, 4);
                            //Utilities.SelectedStrategies = null;
                            ////REMARK: Condition to handle the navigation if the strategy is assigned
                            //int strategyAssigned = Preferences.Get(StringConstant.AssignedStrategyId, 0);
                            //Application.Current.MainPage = strategyAssigned != 0 ? new StrategiesPage() : (Page)new AllStrategiesListPage();
                        });
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                }
                return moreTabCommand;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Handle the tabbed view images change
        /// </summary>
        public TabbedViewViewModel()
        {
            try
            {
                int tabId = Preferences.Get(StringConstant.TabIdKey, 0);
                switch (tabId)
                {
                    case 0:
                    case 1:
                        HomeIcon = StringConstant.HomeTabSelected;
                        HomeTextColor = (Color)Application.Current.Resources["BlackTextColor"];
                        LessonsIcon = StringConstant.LessonTabUnselected;
                        LessonsTextColor = (Color)Application.Current.Resources["GrayTextColor"];
                        StrategiesIcon = StringConstant.StrategiesTabUnselected;
                        StrategiesTextColor = (Color)Application.Current.Resources["GrayTextColor"];
                        MoreIcon = StringConstant.MoreUnselected;
                        MoreTextColor = (Color)Application.Current.Resources["GrayTextColor"];
                        break;
                    case 2:
                        HomeIcon = StringConstant.HomeTabUnselected;
                        HomeTextColor = (Color)Application.Current.Resources["GrayTextColor"];
                        LessonsIcon = StringConstant.LessonTabSelected;
                        LessonsTextColor = (Color)Application.Current.Resources["BlackTextColor"];
                        StrategiesIcon = StringConstant.StrategiesTabUnselected;
                        StrategiesTextColor = (Color)Application.Current.Resources["GrayTextColor"];
                        MoreIcon = StringConstant.MoreUnselected;
                        MoreTextColor = (Color)Application.Current.Resources["GrayTextColor"];
                        break;
                    case 3:
                        HomeIcon = StringConstant.HomeTabUnselected;
                        HomeTextColor = (Color)Application.Current.Resources["GrayTextColor"];
                        LessonsIcon = StringConstant.LessonTabUnselected;
                        LessonsTextColor = (Color)Application.Current.Resources["GrayTextColor"];
                        StrategiesIcon = StringConstant.StrategiesTabSelected;
                        StrategiesTextColor = (Color)Application.Current.Resources["BlackTextColor"];
                        MoreIcon = StringConstant.MoreUnselected;
                        MoreTextColor = (Color)Application.Current.Resources["GrayTextColor"];
                        break;
                    case 4:
                        HomeIcon = StringConstant.HomeTabUnselected;
                        HomeTextColor = (Color)Application.Current.Resources["GrayTextColor"];
                        LessonsIcon = StringConstant.LessonTabUnselected;
                        LessonsTextColor = (Color)Application.Current.Resources["GrayTextColor"];
                        StrategiesIcon = StringConstant.StrategiesTabUnselected;
                        StrategiesTextColor = (Color)Application.Current.Resources["GrayTextColor"];
                        MoreIcon = StringConstant.MoreSelected;
                        MoreTextColor = (Color)Application.Current.Resources["BlackTextColor"];
                        break;
                    default:
                        HomeIcon = StringConstant.HomeTabSelected;
                        HomeTextColor = (Color)Application.Current.Resources["BlackTextColor"];
                        LessonsIcon = StringConstant.LessonTabUnselected;
                        LessonsTextColor = (Color)Application.Current.Resources["GrayTextColor"];
                        StrategiesIcon = StringConstant.StrategiesTabUnselected;
                        StrategiesTextColor = (Color)Application.Current.Resources["GrayTextColor"];
                        MoreIcon = StringConstant.MoreUnselected;
                        MoreTextColor = (Color)Application.Current.Resources["GrayTextColor"];
                        break;
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion
    }
}
