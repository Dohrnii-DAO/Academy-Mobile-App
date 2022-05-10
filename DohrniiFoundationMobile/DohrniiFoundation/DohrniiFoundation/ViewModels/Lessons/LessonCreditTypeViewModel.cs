using DohrniiFoundation.Helpers;
using DohrniiFoundation.Resources;
using DohrniiFoundation.Views.Lessons;
using Microsoft.AppCenter.Crashes;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace DohrniiFoundation.ViewModels.Lessons
{
    /// <summary>
    /// This view model is to bind the UI and functionality
    /// </summary>
    public class LessonCreditTypeViewModel : ObservableObject
    {
        #region Private Properties
        private string onBoardingTitle;
        private string onBoardingDescription;
        private string onBoardingType;
        private double xPOpacity;
        private double jellyfishOpacity;
        private double dHNOpacity;
        private bool isXPConvertVisible;
        private bool isRedeemDHNVisible;
        private bool convertXPFrameEnabled;
        private Color convertXPFrameBgColor;
        #endregion

        #region Public Properties
        public string OnBoardingTitle
        {
            get
            {
                return onBoardingTitle;
            }
            set
            {
                if (onBoardingTitle != value)
                {
                    onBoardingTitle = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public string OnBoardingDescription
        {
            get
            {
                return onBoardingDescription;
            }
            set
            {
                if (onBoardingDescription != value)
                {
                    onBoardingDescription = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public string OnBoardingType
        {
            get
            {
                return onBoardingType;
            }
            set
            {
                if (onBoardingType != value)
                {
                    onBoardingType = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public double XPOpacity
        {
            get
            {
                return xPOpacity;
            }
            set
            {
                if (xPOpacity != value)
                {
                    xPOpacity = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public double JellyfishOpacity
        {
            get
            {
                return jellyfishOpacity;
            }
            set
            {
                if (jellyfishOpacity != value)
                {
                    jellyfishOpacity = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public double DHNOpacity
        {
            get
            {
                return dHNOpacity;
            }
            set
            {
                if (dHNOpacity != value)
                {
                    dHNOpacity = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public bool IsXPConvertVisible
        {
            get
            {
                return isXPConvertVisible;
            }
            set
            {
                if (isXPConvertVisible != value)
                {
                    isXPConvertVisible = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public bool IsRedeemDHNVisible
        {
            get
            {
                return isRedeemDHNVisible;
            }
            set
            {
                if (isRedeemDHNVisible != value)
                {
                    isRedeemDHNVisible = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public bool ConvertXPFrameEnabled
        {
            get
            {
                return convertXPFrameEnabled;
            }
            set
            {
                if (convertXPFrameEnabled != value)
                {
                    convertXPFrameEnabled = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public Color ConvertXPFrameBgColor
        {
            get
            {
                return convertXPFrameBgColor;
            }
            set
            {
                if (convertXPFrameBgColor != value)
                {
                    convertXPFrameBgColor = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public ICommand NextCommand { get; set; }
        public ICommand ConvertCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// This constructor is to initialize the command and get type clicked
        /// </summary>
        public LessonCreditTypeViewModel()
        {
            try
            {
                NextCommand = new Command(NextClick);
                ConvertCommand = new Command(ConvertClick);
                if (Utilities.TotalXP >= Utilities.XPPerCryptoJelly)
                {
                    ConvertXPFrameEnabled = true;
                    ConvertXPFrameBgColor = (Color)Application.Current.Resources["ConvertJellyBtnBgColor"];
                }
                else
                {
                    ConvertXPFrameEnabled = false;
                    ConvertXPFrameBgColor = (Color)Application.Current.Resources["LessonSegmentColor"];
                }
                GetClickedTypeDetail();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// This method is used to get the credit type details if login from fresh signup
        /// </summary>
        private void GetClickedTypeDetail()
        {
            try
            {
                switch (Utilities.SelectedLessonTypePoints)
                {
                    case "XP":
                        OnBoardingTitle = DFResources.WhatisXPText;
                        OnBoardingType = DFResources.XPText + string.Empty + StringConstant.DashSymbol + string.Empty;
                        OnBoardingDescription = DFResources.XPContentText + string.Empty + DFResources.XPContentTextNext;
                        XPOpacity = 1.0;
                        JellyfishOpacity = 0.5;
                        DHNOpacity = 0.5;
                        IsRedeemDHNVisible = false;
                        IsXPConvertVisible = true;
                        break;
                    case "Crypto Jelly":
                        OnBoardingTitle = DFResources.WhatisCryptoJellyText;
                        OnBoardingType = DFResources.CryptoJellyText + string.Empty + StringConstant.DashSymbol + string.Empty;
                        OnBoardingDescription = DFResources.CryptoJellyContentText;
                        XPOpacity = 0.5;
                        JellyfishOpacity = 1.0;
                        DHNOpacity = 0.5;
                        IsRedeemDHNVisible = false;
                        IsXPConvertVisible = true;
                        break;
                    case "DHN":
                        OnBoardingTitle = DFResources.WhatisDHNText;
                        OnBoardingType = DFResources.DHNText + string.Empty + StringConstant.DashSymbol + string.Empty;
                        OnBoardingDescription = DFResources.DHNContentText + " 😉";
                        XPOpacity = 0.5;
                        JellyfishOpacity = 0.5;
                        DHNOpacity = 1.0;
                        IsRedeemDHNVisible = true;
                        IsXPConvertVisible = false;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        /// <summary>
        /// This method is used to handle the navigation on the next button click
        /// </summary>
        private void NextClick()
        {
            try
            {
                Application.Current.MainPage.Navigation.PopPopupAsync();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        /// <summary>
        /// This method is to navigate to the convert XP to jellyfish screen
        /// </summary>
        private void ConvertClick()
        {
            try
            {
                Application.Current.MainPage.Navigation.PushPopupAsync(new ConvertXPToJellyfishPage());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion
    }
}
