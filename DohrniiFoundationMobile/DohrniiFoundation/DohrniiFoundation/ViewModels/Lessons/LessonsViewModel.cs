using DohrniiFoundation.Helpers;
using DohrniiFoundation.IServices;
using DohrniiFoundation.Models.APIResponseModels.Lessons;
using DohrniiFoundation.Models.APIResponseModels.User;
using DohrniiFoundation.Models.Lessons;
using DohrniiFoundation.Resources;
using DohrniiFoundation.Services;
using DohrniiFoundation.Views;
using DohrniiFoundation.Views.Lessons;
using Microsoft.AppCenter.Crashes;
using Rg.Plugins.Popup.Extensions;
using System.Threading.Tasks;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace DohrniiFoundation.ViewModels.Lessons
{
    /// <summary>
    /// View model to bind and implement the functionality of the lessons home screen
    /// </summary>
    public class LessonsViewModel : ObservableObject
    {
        #region Private Properties
        private ObservableCollection<ChaptersCategoryModel> chaptersCategoryList;
        private static IAPIService aPIService;
        private int totalXP;
        private int totalCryptoJelly;
        private decimal totalDHN;
        private decimal chapterProgress;
        private string chapterProgressPercentage;
        private string lessonContinueName;
        #endregion

        #region Public Properties
        public string LessonsBg { get; set; } = StringConstant.LessonsBg;
        public string Jellyfish { get; set; } = StringConstant.Jellyfish;
        public string LessonsDropup { get; set; } = StringConstant.LessonsDropup;
        public ObservableCollection<ChaptersCategoryModel> ChaptersCategoryList
        {
            get { return chaptersCategoryList; }
            set
            {
                if (chaptersCategoryList != value)
                {
                    chaptersCategoryList = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public int TotalXP
        {
            get { return totalXP; }
            set
            {
                if (totalXP != value)
                {
                    totalXP = value;
                    OnPropertyChanged();
                }
            }
        }
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
        public decimal TotalDHN
        {
            get { return totalDHN; }
            set
            {
                if (totalDHN != value)
                {
                    totalDHN = value;
                    OnPropertyChanged();
                }
            }
        }
        public decimal ChapterProgress
        {
            get { return chapterProgress; }
            set
            {
                if (chapterProgress != value)
                {
                    chapterProgress = value;
                    OnPropertyChanged();
                }
            }
        }
        public string ChapterProgressPercentage
        {
            get { return chapterProgressPercentage; }
            set
            {
                if (chapterProgressPercentage != value)
                {
                    chapterProgressPercentage = value;
                    OnPropertyChanged();
                }
            }
        }
        public string LessonContinueName
        {
            get { return lessonContinueName; }
            set
            {
                if (lessonContinueName != value)
                {
                    lessonContinueName = value;
                    OnPropertyChanged(nameof(LessonContinueName));
                }
            }
        }
        public ICommand ContinueChapterCommand { get; set; }
        public ICommand XPCommand { get; set; }
        public ICommand JellyfishCommand { get; set; }
        public ICommand DHNCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the command
        /// </summary>
        public LessonsViewModel()
        {
            try {
            ContinueChapterCommand = new Command(ContinueChapterClick);
            XPCommand = new Command(XPClick);
            JellyfishCommand = new Command(JellyfishClick);
            DHNCommand = new Command(DHNCommandClick);
            aPIService = new APIServices();
            ChaptersCategoryList = new ObservableCollection<ChaptersCategoryModel>();
            GetChaptersCategories();
            MessagingCenter.Subscribe<ConvertXPToJellyfishViewModel, bool>(this, StringConstant.UpdateUserXPRefresh, async (s, e) =>
            {
                await GetUserStatus();
            });
           }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// This method is used to integrate the API to get all the chapters categories
        /// </summary>
        private async void GetChaptersCategories()
        {
            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = true;
                });
                ChaptersCategoryResponseModel responseModel = await aPIService.GetAllChaptersCategoryService();
                if (responseModel != null)
                {
                    if (responseModel.StatusCode == 200 && responseModel.Status)
                    {
                        foreach (ChaptersCategoryModel categories in responseModel.Categories)
                        {
                            ChaptersCategoryList.Add(new ChaptersCategoryModel() { CategoryName = categories.CategoryName, Id = categories.Id });
                        }
                        //REMARK: Store the default selected category id
                        Utilities.ChaptersCategorySelected = ChaptersCategoryList[0];
                        ChaptersCategoryList[0].IsCategorySelected = true;
                        foreach (ChaptersCategoryModel chaptersCategories in ChaptersCategoryList)
                        {
                            if (chaptersCategories.IsCategorySelected)
                            {
                                chaptersCategories.IsGradientVisible = true;
                                chaptersCategories.IsNotGrdientVisible = false;
                            }
                            else
                            {
                                chaptersCategories.IsGradientVisible = false;
                                chaptersCategories.IsNotGrdientVisible = true;
                            }
                        }
                        ChaptersCategoryList[0].IsGradientVisible = true;
                        ChaptersCategoryList[1].IsNotGrdientVisible = true;
                    }
                    else if (!responseModel.Status && responseModel.StatusCode == 202)
                    {
                        await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, responseModel.Message, DFResources.OKText);
                    }
                    else
                    {
                        if (responseModel.StatusCode == 501 || responseModel.StatusCode == 401 || responseModel.StatusCode == 400 || responseModel.StatusCode == 404)
                        {
                            await Application.Current.MainPage.Navigation.PushModalAsync(new ResponseErrorPage());
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, responseModel.Message, DFResources.OKText);
                        }
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(DFResources.OopsText, DFResources.SomethingWrongText, DFResources.OKText);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                IsLoading = false;
            }
            finally
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = false;
                });
            }
        }
        /// <summary>
        /// This method is used to integrate the API to get the user status details
        /// </summary>
        /// <returns></returns>
        public async Task GetUserStatus()
        {
            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = true;
                });
                StrategyStatusResponseModel userStatusResponse = await aPIService.GetUserStatusService();
                if (userStatusResponse != null)
                {
                    if (userStatusResponse.StatusCode == 200 && userStatusResponse.Status)
                    {
                        if (userStatusResponse.UserStatusDetails != null)
                        {
                            foreach (UserStatusDetails userDetails in userStatusResponse.UserStatusDetails)
                            {
                                TotalXP = userDetails.TotalXP;
                                TotalCryptoJelly = userDetails.TotalCryptoJelly;
                                TotalDHN = Math.Round(userDetails.TotalDHN, 2);
                                Utilities.UserTotalCryptoJelly = userDetails.TotalCryptoJelly;
                                Utilities.XPPerCryptoJelly = Convert.ToInt32(userDetails.XPPerCryptoJelly);
                                Utilities.TotalXP = userDetails.TotalXP;
                                if (userDetails.LessonInProgress != null)
                                {
                                    foreach (var lessonsProgress in userDetails.LessonInProgress)
                                    {
                                        if (lessonsProgress.CategoryId == Utilities.ChaptersCategorySelected.Id)
                                        {
                                            if (string.IsNullOrEmpty(lessonsProgress.LessonName))
                                            {
                                                LessonContinueName = DFResources.AllLessonsCompletedText;
                                            }
                                            else
                                            {
                                                LessonContinueName = lessonsProgress.LessonName;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (!userStatusResponse.Status && userStatusResponse.StatusCode == 202)
                    {
                        await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, userStatusResponse.Message, DFResources.OKText);
                    }
                    else
                    {
                        if (userStatusResponse.StatusCode == 501 || userStatusResponse.StatusCode == 401 || userStatusResponse.StatusCode == 400 || userStatusResponse.StatusCode == 404)
                        {
                            await Application.Current.MainPage.Navigation.PushModalAsync(new ResponseErrorPage());
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, userStatusResponse.Message, DFResources.OKText);
                        }
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(DFResources.OopsText, DFResources.SomethingWrongText, DFResources.OKText);
                }
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
        /// <summary>
        /// This method is to get the chapters progress from API
        /// </summary>
        /// <returns></returns>
        public async Task GetChapterProgress()
        {
            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = true;
                });
                ChapterProgressRequestModel chapterProgressRequestModel = new ChapterProgressRequestModel()
                {
                    CategoryId = Utilities.ChaptersCategorySelected.Id,
                };
                ChapterProgressResponseModel chapterProgressResponseModel = await aPIService.GetChapterProgressService(chapterProgressRequestModel);
                if (chapterProgressResponseModel != null)
                {
                    if (chapterProgressResponseModel.StatusCode == 200 && chapterProgressResponseModel.Status)
                    {
                        if (chapterProgressResponseModel.ProgressData != null)
                        {
                            //REMARK: Bind the chapter progress depends on the classes
                            decimal classCompleted = Convert.ToDecimal(chapterProgressResponseModel.ProgressData.CompleteClass);
                            decimal classesTotal = Convert.ToDecimal(chapterProgressResponseModel.ProgressData.TotalClass);
                            ChapterProgress = classCompleted / classesTotal;
                            ChapterProgressPercentage = Math.Round(ChapterProgress * 100, MidpointRounding.ToEven).ToString() + "%";
                        }
                    }
                    else if (!chapterProgressResponseModel.Status && chapterProgressResponseModel.StatusCode == 202)
                    {
                        await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, chapterProgressResponseModel.Message, DFResources.OKText);
                    }
                    else
                    {
                        if (chapterProgressResponseModel.StatusCode == 501 || chapterProgressResponseModel.StatusCode == 401 || chapterProgressResponseModel.StatusCode == 400 || chapterProgressResponseModel.StatusCode == 404)
                        {
                            await Application.Current.MainPage.Navigation.PushModalAsync(new ResponseErrorPage());
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, chapterProgressResponseModel.Message, DFResources.OKText);
                        }
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(DFResources.OopsText, DFResources.SomethingWrongText, DFResources.OKText);
                }
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
        /// <summary>
        /// This method is used handle the navigation when click on back
        /// </summary>
        private async void ContinueChapterClick()
        {
            try
            {
                IsLoading = true;
                await Application.Current.MainPage.Navigation.PushModalAsync(new LessonChaptersPage());
                IsLoading = false;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        /// <summary>
        /// This method is used to open the XP frame 
        /// </summary>
        private async void XPClick()
        {
            try
            {
                Utilities.SelectedLessonTypePoints = DFResources.XPText;
                await Application.Current.MainPage.Navigation.PushPopupAsync(new LessonOnboardingPopUpPage());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        /// <summary>
        /// This method is used to open the jelly frame 
        /// </summary>
        private async void JellyfishClick()
        {
            try
            {
                Utilities.SelectedLessonTypePoints = DFResources.CryptoJellyText;
                await Application.Current.MainPage.Navigation.PushPopupAsync(new LessonOnboardingPopUpPage());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        /// <summary>
        /// This method is used to open the DHN frame 
        /// </summary>
        private async void DHNCommandClick()
        {
            try
            {
                Utilities.SelectedLessonTypePoints = DFResources.DHNText;
                await Application.Current.MainPage.Navigation.PushPopupAsync(new LessonOnboardingPopUpPage());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        /// <summary>
        /// This command is to handle the chapters category selected and show the progress and status
        /// </summary>
        public Command CategoriesSelectedCommand
        {
            get
            {
                return new Command(async (param) =>
                {
                    try
                    {
                        IsLoading = true;
                        var selectedCategories = param as ChaptersCategoryModel;
                        Utilities.ChaptersCategorySelected = selectedCategories;
                        foreach (var chaptersCategories in ChaptersCategoryList)
                        {
                            if (chaptersCategories.Id == selectedCategories.Id)
                            {
                                chaptersCategories.IsCategorySelected = true;
                                chaptersCategories.IsGradientVisible = true;
                                chaptersCategories.IsNotGrdientVisible = false;
                            }
                            else
                            {
                                chaptersCategories.IsCategorySelected = false;
                                chaptersCategories.IsGradientVisible = false;
                                chaptersCategories.IsNotGrdientVisible = true;
                            }
                        }
                        await GetChapterProgress();
                        await GetUserStatus();
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
