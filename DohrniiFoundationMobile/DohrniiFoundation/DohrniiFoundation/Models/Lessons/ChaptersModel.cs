using DohrniiFoundation.Helpers;
using DohrniiFoundation.IServices;
using DohrniiFoundation.Models.APIRequestModel.Lessons;
using DohrniiFoundation.Models.APIResponseModels;
using DohrniiFoundation.Resources;
using DohrniiFoundation.Services;
using DohrniiFoundation.ViewModels.Lessons;
using DohrniiFoundation.Views;
using DohrniiFoundation.Views.Lessons;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DohrniiFoundation.Models.Lessons
{
    /// <summary>
    /// This model class is to bind the properties of the chapter
    /// </summary>
    public class ChaptersModel : ObservableObject
    {
        #region Private Properties
        private decimal chaptersProgressPercentage;
        private string quizStatusName;
        private static IAPIService aPIService;
        private bool isQuizCompleted;
        private bool isQuizUnlock;
        private bool isQuizLocked;
        #endregion

        #region Public Properties
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("category_id")]
        public int CategoryId { get; set; }
        [JsonProperty("category_name")]
        public string CategoryName { get; set; }
        [JsonProperty("chapter_quiz_status")]
        public string ChapterQuizStatus { get; set; }
        [JsonProperty("crypto_jelly")]
        public int CryptoJelly { get; set; }
        [JsonProperty("question_limit")]
        public int QuestionLimit { get; set; }
        [JsonProperty("quiz_timer")]
        public int QuizTimer { get; set; }        
        [JsonProperty("illustration_image")]
        public string IllustrationImage { get; set; }
        [JsonProperty("total_classes")]
        public int TotalClasses { get; set; }
        [JsonProperty("complete_classes")]
        public int CompleteClasses { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty("deleted_at")]
        public object DeletedAt { get; set; }
        [JsonProperty("lesson_data")]
        public ObservableCollection<Lessons> LessonDataList { get; set; }
        public string ChaptersBg { get; set; } = StringConstant.ChaptersBg;
        public int ChapterNumber { get; set; }
        public decimal ChaptersProgressPercentage
        {
            get { return chaptersProgressPercentage; }
            set
            {
                if (chaptersProgressPercentage != value)
                {
                    chaptersProgressPercentage = value;
                    this.OnPropertyChanged(nameof(ChaptersProgressPercentage));
                }
            }
        }
        public string QuizStatusName
        {
            get { return quizStatusName; }
            set
            {
                if (quizStatusName != value)
                {
                    quizStatusName = value;
                    this.OnPropertyChanged(nameof(QuizStatusName));
                }
            }
        }
        public bool IsQuizCompleted
        {
            get { return isQuizCompleted; }
            set
            {
                if (isQuizCompleted != value)
                {
                    isQuizCompleted = value;
                    this.OnPropertyChanged(nameof(IsQuizCompleted));
                }
            }
        }
        public bool IsQuizUnlock
        {
            get { return isQuizUnlock; }
            set
            {
                if (isQuizUnlock != value)
                {
                    isQuizUnlock = value;
                    this.OnPropertyChanged(nameof(IsQuizUnlock));
                }
            }
        }
        public bool IsQuizLocked
        {
            get { return isQuizLocked; }
            set
            {
                if (isQuizLocked != value)
                {
                    isQuizLocked = value;
                    this.OnPropertyChanged(nameof(IsQuizLocked));
                }
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// This constructor is to used to initialize
        /// </summary>
        public ChaptersModel()
        {
            aPIService = new APIServices();
        }
        /// <summary>
        /// This command is used to handle the unlock quiz funtionality by integrating the API
        /// </summary>
        public Command QuizCommand
        {
            get
            {
                return new Command(async (param) =>
                {
                    try
                    {                      
                        var selectedChapterQuiz = param as ChaptersModel;
                        Utilities.QuizSelectedChapter = selectedChapterQuiz;
                        string lastLessonStatus = string.Empty;
                        //REMARK: Get the last status of the lesson of the completed chapter
                        foreach (var chapter in Utilities.ChaptersListData)
                        {
                            if (chapter.Id == Utilities.QuizSelectedChapter.Id)
                            {
                                foreach (var lessons in chapter.LessonDataList)
                                {
                                    lastLessonStatus = chapter.LessonDataList[chapter.LessonDataList.Count - 1].Status;
                                }
                            }
                        }
                        //REMARK: Handle the condition on the basis of chapter quiz status
                        if (selectedChapterQuiz.ChapterQuizStatus == StringConstant.ClassUnlocked)
                        {
                            await Application.Current.MainPage.Navigation.PushModalAsync(new ChaptersQuizPage());
                        }
                        else if (selectedChapterQuiz.ChapterQuizStatus == StringConstant.ClassPassed)
                        {
                            await Application.Current.MainPage.Navigation.PushModalAsync(new ChaptersQuizPage());
                        }
                        else if(selectedChapterQuiz.ChapterQuizStatus == StringConstant.ClassLocked)
                        {
                            if (lastLessonStatus == StringConstant.LessonComplete)
                            {
                                if (Utilities.UserTotalCryptoJelly < selectedChapterQuiz.CryptoJelly)
                                {
                                    await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, "Please earn " + selectedChapterQuiz.CryptoJelly + " crypto jelly to unlock this quiz.", DFResources.OKText);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Crashes.TrackError(ex);
                    }
                });
            }
        }
        private async Task UnlockChapterQuiz(ChaptersModel selectedQuiz)
        {
            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = true;
                });
                int userId = Preferences.Get(StringConstant.UserId, 0) == 0 ? 0 : Preferences.Get(StringConstant.UserId, 0);
                UnlockQuizRequestModel unlockChapterQuizRequestModel = new UnlockQuizRequestModel()
                {
                    UserId = userId,
                    ChapterId = selectedQuiz.Id,
                };
                ResponseModel unlockChapterQuizResponseModel = await aPIService.UnlockChapterQuizService(unlockChapterQuizRequestModel);
                if (unlockChapterQuizResponseModel != null)
                {
                    if (unlockChapterQuizResponseModel.StatusCode == 200 && unlockChapterQuizResponseModel.Status)
                    {
                        //REMARK: Bind the chapters quiz status depends on the value from API
                        if (selectedQuiz.ChapterQuizStatus == StringConstant.ClassLocked)
                        {
                            selectedQuiz.QuizStatusName = StringConstant.Unlocked;
                            selectedQuiz.ChapterQuizStatus = StringConstant.ClassUnlocked;
                            selectedQuiz.CryptoJelly = 0;
                        }
                        LessonChaptersViewModel lessonChaptersViewModel = new LessonChaptersViewModel();
                        lessonChaptersViewModel.GetAllChaptersAndLessons();
                    }
                    else if (unlockChapterQuizResponseModel.StatusCode == 202 && !unlockChapterQuizResponseModel.Status)
                    {
                        await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, unlockChapterQuizResponseModel.Message, DFResources.OKText);
                    }
                    else
                    {
                        if (unlockChapterQuizResponseModel.StatusCode == 501 || unlockChapterQuizResponseModel.StatusCode == 401 || unlockChapterQuizResponseModel.StatusCode == 400 || unlockChapterQuizResponseModel.StatusCode == 404)
                        {
                            await Application.Current.MainPage.Navigation.PushModalAsync(new ResponseErrorPage());
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, unlockChapterQuizResponseModel.Message, DFResources.OKText);
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
        #endregion
    } 
}
