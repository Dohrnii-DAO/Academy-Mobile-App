using DohrniiFoundation.Helpers;
using DohrniiFoundation.Models.Lessons;
using DohrniiFoundation.Resources;
using DohrniiFoundation.Views.Lessons;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DohrniiFoundation.ViewModels.Lessons
{
    /// <summary>
    /// This view model is to bind the UI and functionality
    /// </summary>
    public class LessonOnboardingViewModel : ObservableObject
    {
        #region Private Properties
        private ObservableCollection<LessonOnboardingModel> lessonsOnboardingList;
        private int position = 0;
        private string nextButton = DFResources.NextText;
        #endregion

        #region Public Properties
        public string OnboardingJellyfish { get; set; } = StringConstant.OnboardingJellyfish;
        public string OnboardingXPJellyfish { get; set; } = StringConstant.OnboardingXPJellyfish;
        public string OnboardingCryptoJellyfish { get; set; } = StringConstant.OnboardingCryptoJellyfish;
        public string OnboardingDhnJellyfish { get; set; } = StringConstant.OnboardingDhnJellyfish;
        public string BottomXPJellyfish { get; set; } = StringConstant.BottomXPJellyfish;
        public ObservableCollection<LessonOnboardingModel> LessonsOnboardingList
        {
            get { return lessonsOnboardingList; }
            set
            {
                if (lessonsOnboardingList != value)
                {
                    lessonsOnboardingList = value;
                    this.OnPropertyChanged(nameof(LessonsOnboardingList));
                }
            }
        }
        public int Position
        {
            get { return position; }
            set
            {
                if (position != value)
                {
                    position = value;
                    this.OnPropertyChanged(nameof(Position));
                }
            }
        }
        public string NextButton
        {
            get { return nextButton; }
            set
            {
                if (nextButton != value)
                {
                    nextButton = value;
                    this.OnPropertyChanged(nameof(NextButton));
                }
            }
        }
        public ICommand NextCommand { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// This constructor is to load the onboarding data of the lesson
        /// </summary>
        public LessonOnboardingViewModel()
        {
            NextCommand = new Command(NextClick);
            GetOnboardingList();
        }
        #endregion

        #region Methods
        /// <summary>
        /// This methods is to get the list of the onboarding lessons
        /// </summary>
        public void GetOnboardingList()
        {
            try
            {
                LessonsOnboardingList = new ObservableCollection<LessonOnboardingModel>()
            {
                new LessonOnboardingModel()
                {
                    Title = DFResources.WhatisDohrniiaboutText,
                    Question = DFResources.LearnAboutCryptoText,
                    ShortTitle =string.Empty,
                    Description = DFResources.TheDohrniiAcademyText + Environment.NewLine + Environment.NewLine+ DFResources.OurcontentText + Environment.NewLine + Environment.NewLine,
                    SecondDescription = DFResources.GetReadyText,
                    ZeroIndexFrameVisible = true,
                    FirstIndexFrameVisible = false,
                    SecondIndexFrameVisible = false,
                    ThirdIndexFrameVisible = false,
                    SecondDescriptionVisible = true,
                    SecondIndexImageVisible = false
                },
                new LessonOnboardingModel()
                {
                    Title = DFResources.SomebasictermsText,
                    Question = DFResources.WhatisXPText,
                    ShortTitle = DFResources.XPText + string.Empty + StringConstant.DashSymbol + string.Empty,
                    Description=DFResources.YougetXPText + Environment.NewLine + Environment.NewLine + DFResources.EachclasshasText + Environment.NewLine + Environment.NewLine + DFResources.YoucanalsoexchangeText ,
                    SecondDescription = string.Empty,
                    ZeroIndexFrameVisible = false,
                    FirstIndexFrameVisible = true,
                    SecondIndexFrameVisible = false,
                    ThirdIndexFrameVisible = false,
                    SecondDescriptionVisible = false,
                    SecondIndexImageVisible = true
                },
                new LessonOnboardingModel()
                {
                    Title = DFResources.WannaunlockText,
                    Question = DFResources.WhatisCryptoJellyText,
                    ShortTitle = DFResources.CryptoJellyText + string.Empty + StringConstant.DashSymbol + string.Empty,
                    Description= DFResources.theyareusedText + Environment.NewLine + Environment.NewLine + DFResources.TheendText + Environment.NewLine + Environment.NewLine,SecondDescription = "\n\n" + DFResources.YoucanexchangeText,ZeroIndexFrameVisible = false,
                    FirstIndexFrameVisible = false,
                    SecondIndexFrameVisible = true,
                    ThirdIndexFrameVisible = false,
                    SecondDescriptionVisible = true,
                    SecondIndexImageVisible = false
                },
                new LessonOnboardingModel()
                {
                    Title = DFResources.OurtokensText,
                    Question = DFResources.WhatisDHNText,
                    ShortTitle = DFResources.DHNText + string.Empty + StringConstant.DashSymbol + string.Empty,
                    Description = DFResources.DHNisthenativetokenText + Environment.NewLine + Environment.NewLine + DFResources.WeareamultichainText + Environment.NewLine + Environment.NewLine + DFResources.DHNisanempowermentText + Environment.NewLine,
                    SecondDescription = "\n\n" + DFResources.WeareturningText,
                    ZeroIndexFrameVisible = false,
                    FirstIndexFrameVisible = false,
                    SecondIndexFrameVisible = false,
                    ThirdIndexFrameVisible = true,
                    SecondDescriptionVisible = true,
                    SecondIndexImageVisible = false
                },
            };
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// This method handle the onboarding screens on click of the next button
        /// </summary>
        public async void NextClick()
        {
            try
            {
                //REMARK: This condition handles the navigation when the onboarding is completed
                if (Position == LessonsOnboardingList.Count - 1)
                {
                    Position = 0;
                    Preferences.Set(StringConstant.TabIdKey, 2);
                    Application.Current.MainPage = new LessonsPage();
                    return;
                }
                //REAMRK: This increment the position of the carousel which shows on the onboarding screens
                if (Position == 2)
                {
                    NextButton = DFResources.tolessonsText;
                }
                int nextPosition = ++Position;
                Position = nextPosition;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        /// <summary>
        /// This method is to get the current item of the carousel view 
        /// </summary>
        public void OnBoardingCurrentItemChanged()
        {
            try
            {
                if (Position == LessonsOnboardingList.Count - 1)
                {
                    NextButton = DFResources.tolessonsText;
                }
                else
                {
                    NextButton = DFResources.NextText.ToLower();
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
