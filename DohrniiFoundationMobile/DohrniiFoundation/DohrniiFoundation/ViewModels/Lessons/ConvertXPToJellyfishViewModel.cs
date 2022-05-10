using DohrniiFoundation.Helpers;
using DohrniiFoundation.IServices;
using DohrniiFoundation.Models.APIRequestModel.Lessons;
using DohrniiFoundation.Models.APIResponseModels;
using DohrniiFoundation.Resources;
using DohrniiFoundation.Services;
using DohrniiFoundation.Views;
using Microsoft.AppCenter.Crashes;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DohrniiFoundation.ViewModels.Lessons
{   
    /// <summary>
    /// This view model is to bind the UI with convert XP to jellyfish
    /// </summary>
    public class ConvertXPToJellyfishViewModel : ObservableObject
    {
        #region Private Properties
        private int totalCryptoJelly;
        private string enteredCryptoJelly;
        private int totalCryptoJellySpend;
        private bool isConvertButtonEnabled;
        private Color convertButtonBgColor = (Color)Application.Current.Resources["LessonSegmentColor"];
        private IAPIService aPIService;
        #endregion

        #region Public Properties
        public int TotalCryptoJelly
        {
            get { return totalCryptoJelly; }
            set
            {
                if (totalCryptoJelly != value)
                {
                    totalCryptoJelly = value;
                    OnPropertyChanged();
                }
            }
        }
        public int TotalCryptoJellySpend
        {
            get { return totalCryptoJellySpend; }
            set
            {
                if (totalCryptoJellySpend != value)
                {
                    totalCryptoJellySpend = value;
                    OnPropertyChanged();
                }
            }
        }
        public string EnteredCryptoJelly
        {
            get { return enteredCryptoJelly; }
            set
            {
                if (enteredCryptoJelly != value)
                {
                    enteredCryptoJelly = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool IsConvertButtonEnabled
        {
            get { return isConvertButtonEnabled; }
            set
            {
                if (isConvertButtonEnabled != value)
                {
                    isConvertButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }
        public Color ConvertButtonBgColor
        {
            get { return convertButtonBgColor; }
            set
            {
                if (convertButtonBgColor != value)
                {
                    convertButtonBgColor = value;
                    OnPropertyChanged();
                }
            }
        }
        public ICommand ConvertXPCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// This constructor is to initialize
        /// </summary>
        public ConvertXPToJellyfishViewModel()
        {
            try
            {
                aPIService = new APIServices();
                TotalCryptoJelly = Utilities.TotalXP;
                ConvertXPCommand = new Command(ConvertXPClick);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// This is to integrate the API to convert the XP into jellyfish
        /// </summary>
        private async void ConvertXPClick()
        {
            try
            {
                if (IsConvertButtonEnabled)
                {
                    IsLoading = true;
                    int userId = Preferences.Get(StringConstant.UserId, 0) == 0 ? 0 : Preferences.Get(StringConstant.UserId, 0);
                    ConvertXPToCryptojellyRequestModel convertXPToCryptojellyRequestModel = new ConvertXPToCryptojellyRequestModel()
                    {
                        UserId = userId,
                        Crptojelley = Convert.ToInt32(EnteredCryptoJelly),
                    };
                    ResponseModel convertXPToCryptojellyResponse = await aPIService.ConvertXPToCryptoJellyService(convertXPToCryptojellyRequestModel);
                    if (convertXPToCryptojellyResponse != null)
                    {
                        if (convertXPToCryptojellyResponse.StatusCode == 200 && convertXPToCryptojellyResponse.Status)
                        {
                            await Application.Current.MainPage.Navigation.PopPopupAsync();
                            await Application.Current.MainPage.Navigation.PopPopupAsync();
                            MessagingCenter.Send<ConvertXPToJellyfishViewModel, bool>(this, StringConstant.UpdateUserXPRefresh, true);
                        }
                        else if (convertXPToCryptojellyResponse.StatusCode == 202 && !convertXPToCryptojellyResponse.Status)
                        {
                            await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, convertXPToCryptojellyResponse.Message, DFResources.OKText);
                        }
                        else
                        {
                            if (convertXPToCryptojellyResponse.StatusCode == 501 || convertXPToCryptojellyResponse.StatusCode == 401 || convertXPToCryptojellyResponse.StatusCode == 400 || convertXPToCryptojellyResponse.StatusCode == 404)
                            {
                                await Application.Current.MainPage.Navigation.PushModalAsync(new ResponseErrorPage());
                            }
                            else
                            {
                                await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, convertXPToCryptojellyResponse.Message, DFResources.OKText);
                            }
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert(DFResources.OopsText, DFResources.SomethingWrongText, DFResources.OKText);
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                IsLoading = false;
            }
            finally
            {
                IsLoading = false;
            }
        }
        /// <summary>
        /// Implement the validation to convert the xp to jellyfish 
        /// </summary>
        /// <returns></returns>
        public async Task ConvertXPToJellyfish()
        {
            try
            {
                if (EnteredCryptoJelly == "0")
                {
                    TotalCryptoJellySpend = 0;
                    TotalCryptoJelly = Utilities.TotalXP;
                }
                else
                {
                    if (!string.IsNullOrEmpty(EnteredCryptoJelly))
                    {
                        if (EnteredCryptoJelly != "0")
                        {
                            IsConvertButtonEnabled = false;
                            ConvertButtonBgColor = (Color)Application.Current.Resources["LessonSegmentColor"];
                            int totalCryproJellySpend = Convert.ToInt32(EnteredCryptoJelly) * Utilities.XPPerCryptoJelly;
                            int totalCryptoJelly = Utilities.TotalXP;
                            if (totalCryproJellySpend > totalCryptoJelly)
                            {
                                await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, DFResources.CollectmoreXPtoConvertText + " " + totalCryproJellySpend, DFResources.OKText);
                                TotalCryptoJelly = TotalCryptoJelly;
                                TotalCryptoJellySpend = 0;
                            }
                            else
                            {
                                IsConvertButtonEnabled = true;
                                ConvertButtonBgColor = (Color)Application.Current.Resources["ConvertJellyBtnBgColor"];
                                TotalCryptoJellySpend = Convert.ToInt32(EnteredCryptoJelly) * Utilities.XPPerCryptoJelly;
                                TotalCryptoJelly = totalCryptoJelly - TotalCryptoJellySpend;
                            }
                        }
                        else
                        {
                            IsConvertButtonEnabled = false;
                            ConvertButtonBgColor = (Color)Application.Current.Resources["LessonSegmentColor"];
                        }
                    }
                    else
                    {
                        IsConvertButtonEnabled = false;
                        ConvertButtonBgColor = (Color)Application.Current.Resources["LessonSegmentColor"];
                        TotalCryptoJellySpend = 0;
                        TotalCryptoJelly = Utilities.TotalXP;
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        /// <summary>
        /// This command is to handle the pop up of the page
        /// </summary>
        public Command PopUpCommand
        {
            get
            {
                return new Command(async =>
                {
                    try
                    {
                        Application.Current.MainPage.Navigation.PopPopupAsync();
                    }
                    catch (Exception ex)
                    {
                        Crashes.TrackError(ex);
                    }
                });
            }
        }
        #endregion
    }
}
