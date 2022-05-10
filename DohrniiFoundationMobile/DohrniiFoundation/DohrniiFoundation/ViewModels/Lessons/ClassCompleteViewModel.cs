using DohrniiFoundation.Helpers;
using DohrniiFoundation.IServices;
using DohrniiFoundation.Models.APIResponseModels.Lessons;
using DohrniiFoundation.Models.Lessons;
using DohrniiFoundation.Services;
using DohrniiFoundation.Views.Lessons;
using Microsoft.AppCenter.Crashes;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace DohrniiFoundation.ViewModels.Lessons
{
    /// <summary>
    /// This view model is used to handle the functionality when the class is completed
    /// </summary>
    public class ClassCompleteViewModel : ObservableObject
    {
        #region Private Properties
        private int xpClassRewarded;
        private string chapterProgress;
        private bool firstRandomGridVisible;
        private bool secondRandomGridVisible;
        private bool thirdRandomGridVisible;
        private bool fourthRandomGridVisible;
        private bool fifthRandomGridVisible;
        private static IAPIService aPIService;
        private bool loaderVisible;
        #endregion

        #region Public Properties
        public string LessonsTick { get; set; } = StringConstant.LessonsTick;
        public string ClassCompletedFish { get; set; } = StringConstant.ClassCompletedFish;
        public int XPClassRewarded
        {
            get { return xpClassRewarded; }
            set
            {
                if (xpClassRewarded != value)
                {
                    xpClassRewarded = value;
                    this.OnPropertyChanged(nameof(XPClassRewarded));
                }
            }
        }
        public string ChapterProgress
        {
            get { return chapterProgress; }
            set
            {
                if (chapterProgress != value)
                {
                    chapterProgress = value;
                    this.OnPropertyChanged(nameof(ChapterProgress));
                }
            }
        }
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
        public bool ThirdRandomGridVisible
        {
            get { return thirdRandomGridVisible; }
            set
            {
                thirdRandomGridVisible = value;
                OnPropertyChanged(nameof(ThirdRandomGridVisible));
            }
        }
        public bool FourthRandomGridVisible
        {
            get { return fourthRandomGridVisible; }
            set
            {
                fourthRandomGridVisible = value;
                OnPropertyChanged(nameof(FourthRandomGridVisible));
            }
        }
        public bool FifthRandomGridVisible
        {
            get { return fifthRandomGridVisible; }
            set
            {
                fifthRandomGridVisible = value;
                OnPropertyChanged(nameof(FifthRandomGridVisible));
            }
        }
        public bool LoaderVisible
        {
            get { return loaderVisible; }
            set
            {
                loaderVisible = value;
                OnPropertyChanged(nameof(LoaderVisible));
            }
        }
        public ICommand ContinueCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// This constructor is to intialize and get the class complete random screen
        /// </summary>
        public ClassCompleteViewModel()
        {
            try
            {
                ContinueCommand = new Command(ContinueClick);
                aPIService = new APIServices();
                GetChaptersProgress();
                GetRandomClassComplete();
                XPClassRewarded = Utilities.XPClassRewarded;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// This method is get update the chapters progress after completing the class using API
        /// </summary>
        private async void GetChaptersProgress()
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
                            decimal progress = classCompleted / classesTotal;
                            ChapterProgress = Math.Round(progress * 100, MidpointRounding.ToEven).ToString();
                        }
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
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = false;
                });
            }
        }
        /// <summary>
        /// This method is to get the random class complete page depends on the random value
        /// </summary>
        private void GetRandomClassComplete()
        {
            try
            {
                switch (Utilities.ClassQuestionsRandom)
                {
                    case 0:
                        FirstRandomGridVisible = true;
                        SecondRandomGridVisible = false;
                        ThirdRandomGridVisible = false;
                        FourthRandomGridVisible = false;
                        FifthRandomGridVisible = false;
                        break;
                    case 1:
                        FirstRandomGridVisible = false;
                        SecondRandomGridVisible = true;
                        ThirdRandomGridVisible = false;
                        FourthRandomGridVisible = false;
                        FifthRandomGridVisible = false;
                        break;
                    case 2:
                        FirstRandomGridVisible = false;
                        SecondRandomGridVisible = false;
                        ThirdRandomGridVisible = true;
                        FourthRandomGridVisible = false;
                        FifthRandomGridVisible = false;
                        break;
                    case 3:
                        FirstRandomGridVisible = false;
                        SecondRandomGridVisible = false;
                        ThirdRandomGridVisible = false;
                        FourthRandomGridVisible = true;
                        FifthRandomGridVisible = false;
                        break;
                    case 4:
                        FirstRandomGridVisible = false;
                        SecondRandomGridVisible = false;
                        ThirdRandomGridVisible = false;
                        FourthRandomGridVisible = false;
                        FifthRandomGridVisible = true;
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
        /// This method is to handle the navigation when click on continue
        /// </summary>
        private async void ContinueClick()
        {
            try
            {
                Utilities.IsFromClassCompletePage = true;
                await Application.Current.MainPage.Navigation.PushModalAsync(new LessonsDetailPage());
                MessagingCenter.Send<ClassCompleteViewModel, bool>(this, StringConstant.UpdateClassesApi, true);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion
    }
}
