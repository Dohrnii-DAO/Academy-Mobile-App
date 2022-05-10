using DohrniiFoundation.Helpers;
using DohrniiFoundation.IServices;
using DohrniiFoundation.Models.APIRequestModel.Lessons;
using DohrniiFoundation.Models.APIResponseModels;
using DohrniiFoundation.Models.APIResponseModels.Lessons;
using DohrniiFoundation.Models.Lessons;
using DohrniiFoundation.Resources;
using DohrniiFoundation.Services;
using DohrniiFoundation.Views;
using DohrniiFoundation.Views.Lessons;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DohrniiFoundation.ViewModels.Lessons
{
    /// <summary>
    /// View model to bind the lessons chapter and functionality 
    /// </summary>
    public class LessonChaptersViewModel : ObservableObject
    {
        #region Private Properties
        private int totalClasses;
        private string xPTokensCollected;
        private string totalXPCollected;
        private decimal xPTotalProgress;
        private ObservableCollection<ChaptersModel> chaptersList;
        private ObservableCollection<ProgressBarModel> progressBarList;
        private static IAPIService aPIService;
        private ObservableCollection<ClassModel> classeslList;
        private bool chaptersListVisible = true;
        private bool classesPopUpVisible = false;
        private string selectedLessonName;
        private string lessonTitle;
        private int selectedClassNumber;
        private Color lastProgressFrameColor = (Color)Application.Current.Resources["LessonSegmentColor"];
        private int position;
        private ObservableCollection<ClassesProgressBarModel> classesProgressBarList;
        private ClassModel classCurrentItem;
        #endregion

        #region Public Properties
        public string LessonsDropdown { get; set; } = StringConstant.LessonsDropdown;
        public ObservableCollection<ChaptersModel> ChaptersList
        {
            get { return chaptersList; }
            set
            {
                if (chaptersList != value)
                {
                    chaptersList = value;
                    this.OnPropertyChanged(nameof(ChaptersList));
                }
            }
        }
        public ObservableCollection<ProgressBarModel> ProgressBarList
        {
            get { return progressBarList; }
            set
            {
                if (progressBarList != value)
                {
                    progressBarList = value;
                    this.OnPropertyChanged(nameof(ProgressBarList));
                }
            }
        }
        public ObservableCollection<ClassesProgressBarModel> ClassesProgressBarList
        {
            get { return classesProgressBarList; }
            set
            {
                if (classesProgressBarList != value)
                {
                    classesProgressBarList = value;
                    this.OnPropertyChanged(nameof(ClassesProgressBarList));
                }
            }
        }
        public ObservableCollection<ClassModel> ClasseslList
        {
            get { return classeslList; }
            set
            {
                if (classeslList != value)
                {
                    classeslList = value;
                    this.OnPropertyChanged(nameof(ClasseslList));
                }
            }
        }
        public bool ChaptersListVisible
        {
            get { return chaptersListVisible; }
            set
            {
                if (chaptersListVisible != value)
                {
                    chaptersListVisible = value;
                    OnPropertyChanged(nameof(ChaptersListVisible));
                }
            }
        }
        public bool ClassesPopUpVisible
        {
            get { return classesPopUpVisible; }
            set
            {
                if (classesPopUpVisible != value)
                {
                    classesPopUpVisible = value;
                    this.OnPropertyChanged(nameof(ClassesPopUpVisible));
                }
            }
        }
        public string SelectedLessonName
        {
            get { return selectedLessonName; }
            set
            {
                if (selectedLessonName != value)
                {
                    selectedLessonName = value;
                    this.OnPropertyChanged(nameof(SelectedLessonName));
                }
            }
        }
        public string LessonTitle
        {
            get { return lessonTitle; }
            set
            {
                if (lessonTitle != value)
                {
                    lessonTitle = value;
                    this.OnPropertyChanged(nameof(LessonTitle));
                }
            }
        }
        public int TotalClasses
        {
            get { return totalClasses; }
            set
            {
                if (totalClasses != value)
                {
                    totalClasses = value;
                    OnPropertyChanged(nameof(TotalClasses));
                }
            }
        }
        public int SelectedClassNumber
        {
            get { return selectedClassNumber; }
            set
            {
                if (selectedClassNumber != value)
                {
                    selectedClassNumber = value;
                    this.OnPropertyChanged(nameof(SelectedClassNumber));
                }
            }
        }
        public string XPTokensCollected
        {
            get { return xPTokensCollected; }
            set
            {
                if (xPTokensCollected != value)
                {
                    xPTokensCollected = value;
                    this.OnPropertyChanged(nameof(XPTokensCollected));
                }
            }
        }
        public string TotalXPCollected
        {
            get { return totalXPCollected; }
            set
            {
                if (totalXPCollected != value)
                {
                    totalXPCollected = value;
                    this.OnPropertyChanged(nameof(TotalXPCollected));
                }
            }
        }
        public decimal XPTotalProgress
        {
            get { return xPTotalProgress; }
            set
            {
                if (xPTotalProgress != value)
                {
                    xPTotalProgress = value;
                    this.OnPropertyChanged(nameof(XPTotalProgress));
                }
            }
        }
        public Color LastProgressFrameColor
        {
            get { return lastProgressFrameColor; }
            set
            {
                if (lastProgressFrameColor != value)
                {
                    lastProgressFrameColor = value;
                    this.OnPropertyChanged(nameof(LastProgressFrameColor));
                }
            }
        }
        public int Position
        {
            get { return position; }
            set
            {
                position = value;
                OnPropertyChanged(nameof(Position));
            }
        }
        public ClassModel ClassCurrentItem
        {
            get { return classCurrentItem; }
            set
            {
                classCurrentItem = value;
                OnPropertyChanged(nameof(ClassCurrentItem));
            }
        }
        public ICommand BackCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand ContinueLessonCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the list and command
        /// </summary>
        public LessonChaptersViewModel()
        {
            try
            {
                BackCommand = new Command(BackClick);
                CancelCommand = new Command(CancelClick);
                ContinueLessonCommand = new Command(ContinueClick);
                aPIService = new APIServices();
                ClasseslList = new ObservableCollection<ClassModel>();
                ClassesProgressBarList = new ObservableCollection<ClassesProgressBarModel>();
                //REMARK: Method to update all status of chapters, lessons and classes
                RefreshStatusAll();
                if (Utilities.SelectedLesson != null)
                {
                    LessonTitle = Utilities.SelectedLesson.Title;
                }
                if (Utilities.IsFromLessonsPopUp)
                {
                    Utilities.IsFromLessonsPopUp = false;
                    foreach (ClassModel classes in Utilities.ClassesSelectedList)
                    {
                        if (classes.Status == StringConstant.ClassUnlocked || classes.Status == StringConstant.ClassPassed)
                        {
                            classes.FrameBgColor = Color.Transparent;
                            classes.FrameOpacity = 1.0;
                            if (classes.Status == StringConstant.ClassUnlocked)
                            {
                                ClassCurrentItem = classes;
                            }
                        }
                        else if (classes.Status == StringConstant.ClassLocked)
                        {
                            classes.FrameBgColor = (Color)Application.Current.Resources["LessonsDetailBgColor"];
                            classes.FrameOpacity = 0.4;
                        }
                    }
                    TotalClasses = Utilities.ClassesSelectedList.Count;
                    TotalXPCollected = Utilities.TotalXPCollected;
                    ClasseslList = Utilities.ClassesSelectedList;
                    ClassesProgressBarList = Utilities.SelectedClassesProgressBarList;
                }
                else
                {
                    if (Utilities.SelectedLesson != null)
                    {
                        TotalClasses = Utilities.ClassesSelectedList.Count;
                        foreach (ClassModel classes in Utilities.ClassesSelectedList)
                        {
                            if (classes.Status == StringConstant.ClassUnlocked)
                            {
                                ClassCurrentItem = classes;
                            }
                        }
                        ClasseslList = Utilities.ClassesSelectedList;
                        TotalXPCollected = Utilities.TotalXPCollected;
                        ClassesProgressBarList = Utilities.SelectedClassesProgressBarList;
                    }
                    GetAllChaptersAndLessons();
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion

        #region Methods       
        /// <summary>
        /// This method is to integrate the all chapters and lessons API and handle the functionality
        /// </summary>
        public async void GetAllChaptersAndLessons()
        {
            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = true;
                });
                ChaptersList = new ObservableCollection<ChaptersModel>();
                int chapterNumber = 0;
                int lessonNumber = 0;
                ChaptersCategoryRequestModel chaptersCategoryRequestModel = new ChaptersCategoryRequestModel()
                {
                    SelectedCategory = Utilities.ChaptersCategorySelected.Id,
                };
                LessonsResponseModel lessonsResponseModel = await aPIService.GetChaptersLessonsCategoryService(chaptersCategoryRequestModel);
                if (lessonsResponseModel != null)
                {
                    if (lessonsResponseModel.StatusCode == 200 && lessonsResponseModel.Status)
                    {
                        if (lessonsResponseModel.AllChapters != null)
                        {
                            foreach (ChaptersModel lessonsData in lessonsResponseModel.AllChapters)
                            {
                                chapterNumber++;
                                lessonNumber = 0;
                                if (lessonsData.LessonDataList != null)
                                {
                                    foreach (Models.Lessons.Lessons lessonsItem in lessonsData.LessonDataList)
                                    {
                                        lessonNumber++;
                                        lessonsItem.LessonsNumber = lessonNumber;
                                        //REMARK: condition to bind lessons progress bar 
                                        if (lessonsItem.Status == StringConstant.ClassUnlocked)
                                        {
                                            lessonsItem.ProgressBarList = new ObservableCollection<ProgressBarModel>();
                                            ProgressBarList = new ObservableCollection<ProgressBarModel>();
                                            ProgressBarModel lessonsProgressBarList = new ProgressBarModel();
                                            lessonsItem.LessonsCompletedClass = lessonsItem.CompleteClass;

                                            //REMARK: handle the background color of progress if class is completed
                                            for (int i = 1; i <= lessonsItem.TotalLinkClass; i++)
                                            {
                                                ProgressBarModel lessonsProgressBarList1 = new ProgressBarModel();
                                                if (Convert.ToInt32(lessonsItem.LessonsCompletedClass) != 0)
                                                {
                                                    lessonsProgressBarList1.LessonsName = lessonsData.Name;
                                                    lessonsProgressBarList1.StackWidth = Application.Current.MainPage.Width;
                                                    lessonsProgressBarList1.BorderCircleFrameColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
                                                    lessonsProgressBarList1.BoxviewFrameColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
                                                    lessonsProgressBarList1.FrameCircleBgColor = (Color)Application.Current.Resources["WhiteText"];
                                                    lessonsItem.LessonsCompletedClass = (Convert.ToInt32(lessonsItem.LessonsCompletedClass) - 1).ToString();
                                                }
                                                else
                                                {
                                                    lessonsProgressBarList1.LessonsName = lessonsData.Name;
                                                    lessonsProgressBarList1.StackWidth = Application.Current.MainPage.Width;
                                                    lessonsProgressBarList1.BorderCircleFrameColor = (Color)Application.Current.Resources["LessonSegmentColor"];
                                                    lessonsProgressBarList1.BoxviewFrameColor = (Color)Application.Current.Resources["LessonSegmentColor"];
                                                    lessonsProgressBarList1.FrameCircleBgColor = (Color)Application.Current.Resources["WhiteText"];
                                                }
                                                lessonsItem.ProgressBarList.Add(lessonsProgressBarList1);
                                            }
                                            //REMARK: classes progress in percentage in lessons
                                            if (lessonsItem.CompleteClass != "0")
                                            {
                                                decimal classCompleted = Convert.ToDecimal(lessonsItem.CompleteClass);
                                                decimal classesTotal = Convert.ToDecimal(lessonsItem.TotalLinkClass);
                                                decimal classProgress = classCompleted / classesTotal;
                                                lessonsItem.ClassPercentage = Math.Round(classProgress * 100, MidpointRounding.ToEven).ToString();
                                            }
                                            else
                                            {
                                                lessonsItem.ClassPercentage = "0";
                                            }
                                        }

                                        if (Utilities.SelectedLesson != null)
                                        {
                                            if (Utilities.SelectedLesson.Id == lessonsItem.Id)
                                            {
                                                if (lessonsData.Id == Utilities.SelectedLesson.ChapterId)
                                                {
                                                    Utilities.ClassXPTokensCollected = lessonsItem.EarnXP;
                                                    XPTokensCollected = Utilities.ClassXPTokensCollected;
                                                }
                                            }
                                        }
                                    }
                                    //REMARK: Get the last status of the lesson of the completed chapter
                                    string lastLessonStatus = lessonsData.LessonDataList[lessonsData.LessonDataList.Count - 1].Status;
                                    //REMARK: Bind the chapters quiz status depends on the value from API
                                    if (lessonsData.ChapterQuizStatus == StringConstant.ClassUnlocked)
                                    {
                                        lessonsData.QuizStatusName = StringConstant.Unlocked;
                                        lessonsData.IsQuizCompleted = false;
                                        lessonsData.IsQuizUnlock = true;
                                        lessonsData.IsQuizLocked = false;
                                        lessonsData.CryptoJelly = lessonsData.CryptoJelly;
                                    }
                                    else if (lessonsData.ChapterQuizStatus == StringConstant.ClassPassed)
                                    {
                                        lessonsData.QuizStatusName = StringConstant.Complete;
                                        lessonsData.IsQuizCompleted = true;
                                        lessonsData.IsQuizUnlock = false;
                                        lessonsData.IsQuizLocked = false;
                                        lessonsData.CryptoJelly = 0;
                                    }
                                    else if (lessonsData.ChapterQuizStatus == StringConstant.ClassLocked)
                                    {
                                        if (lastLessonStatus != "1")
                                        {
                                            lessonsData.QuizStatusName = StringConstant.Locked;
                                            lessonsData.IsQuizCompleted = false;
                                            lessonsData.IsQuizUnlock = false;
                                            lessonsData.IsQuizLocked = true;
                                            lessonsData.CryptoJelly = lessonsData.CryptoJelly;
                                        }
                                        else
                                        {
                                            if (lessonsData.CryptoJelly <= Utilities.UserTotalCryptoJelly)
                                            {
                                                ResponseModel unlockQuizResponse = await UnlockChapterQuiz(lessonsData);
                                                if (unlockQuizResponse.Status)
                                                {
                                                    lessonsData.ChapterQuizStatus = StringConstant.ClassUnlocked;
                                                    lessonsData.IsQuizCompleted = false;
                                                    lessonsData.IsQuizUnlock = true;
                                                    lessonsData.IsQuizLocked = false;
                                                    lessonsData.CryptoJelly = lessonsData.CryptoJelly;
                                                }
                                            }
                                            else
                                            {
                                                lessonsData.IsQuizCompleted = false;
                                                lessonsData.IsQuizUnlock = false;
                                                lessonsData.IsQuizLocked = true;
                                            }
                                        }
                                    }
                                    decimal totalClasses = Convert.ToDecimal(lessonsData.TotalClasses);
                                    decimal completedClasses = Convert.ToDecimal(lessonsData.CompleteClasses);
                                    lessonsData.ChaptersProgressPercentage = completedClasses / totalClasses;
                                    ChaptersList.Add(new ChaptersModel() { Id = lessonsData.Id, Name = lessonsData.Name, Description = lessonsData.Description, CategoryId = lessonsData.CategoryId, CategoryName = lessonsData.CategoryName, ChapterQuizStatus = lessonsData.ChapterQuizStatus, QuizStatusName = lessonsData.QuizStatusName, CryptoJelly = lessonsData.CryptoJelly, IllustrationImage = lessonsData.IllustrationImage, CompleteClasses = lessonsData.CompleteClasses, TotalClasses = lessonsData.TotalClasses, ChaptersProgressPercentage = lessonsData.ChaptersProgressPercentage, ChapterNumber = chapterNumber, IsQuizCompleted = lessonsData.IsQuizCompleted, IsQuizUnlock = lessonsData.IsQuizUnlock, IsQuizLocked = lessonsData.IsQuizLocked, QuizTimer = lessonsData.QuizTimer, LessonDataList = lessonsData.LessonDataList });
                                    Utilities.ChaptersListData = chaptersList;
                                }
                            }
                            foreach (ChaptersModel chapters in ChaptersList)
                            {
                                foreach (Models.Lessons.Lessons lessons in chapters.LessonDataList)
                                {
                                    if (lessons.Status == StringConstant.LessonInprogress)
                                    {
                                        lessons.IsCompleted = false;
                                        lessons.IsLocked = false;
                                        lessons.IsPending = true;
                                        lessons.IsProgressBar = true;
                                        lessons.LessonTextColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
                                        lessons.LessonCountBackgroundColor = (Color)Application.Current.Resources["BlackText"];
                                        //REMARK: Handle the xp collected for each class depends on the correct answer of the class
                                        int classesEarnXP = chapters.LessonDataList.Where(p => p.Id == lessons.Id).Select(x => Convert.ToInt32(x.EarnXP)).FirstOrDefault();
                                        Utilities.ClassesXPCollected = classesEarnXP;
                                    }
                                    else if (lessons.Status == StringConstant.LessonComplete)
                                    {
                                        lessons.IsCompleted = true;
                                        lessons.IsPending = false;
                                        lessons.IsProgressBar = false;
                                        lessons.IsLocked = false;
                                        lessons.LessonTextColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
                                        lessons.LessonCountBackgroundColor = (Color)Application.Current.Resources["BlackText"];
                                    }
                                    else if (lessons.Status == StringConstant.LessonsLocked)
                                    {
                                        lessons.IsCompleted = false;
                                        lessons.IsPending = false;
                                        lessons.IsProgressBar = false;
                                        lessons.IsLocked = true;
                                        lessons.LessonTextColor = (Color)Application.Current.Resources["LessonSegmentColor"];
                                        lessons.LessonCountBackgroundColor = (Color)Application.Current.Resources["LessonLockedGrayColor"];
                                    }
                                }
                            }
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, DFResources.SomethingWrongText, DFResources.OKText);
                        }

                    }
                    else if (lessonsResponseModel.StatusCode == 202 && !lessonsResponseModel.Status)
                    {
                        await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, lessonsResponseModel.Message, DFResources.OKText);
                    }
                    else
                    {
                        if (lessonsResponseModel.StatusCode == 501 || lessonsResponseModel.StatusCode == 401 || lessonsResponseModel.StatusCode == 400 || lessonsResponseModel.StatusCode == 404)
                        {
                            await Application.Current.MainPage.Navigation.PushModalAsync(new ResponseErrorPage());
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, lessonsResponseModel.Message, DFResources.OKText);
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
        /// This method is used to unlock the chapter if completed using the unlock quiz API
        /// </summary>
        /// <param name="selectedQuiz"></param>
        /// <returns></returns>
        private async Task<ResponseModel> UnlockChapterQuiz(ChaptersModel selectedQuiz)
        {
            ResponseModel unlockChapterQuizResponseModel = new ResponseModel();
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
                unlockChapterQuizResponseModel = await aPIService.UnlockChapterQuizService(unlockChapterQuizRequestModel);
                if (unlockChapterQuizResponseModel != null)
                {
                    if (unlockChapterQuizResponseModel.StatusCode == 200 && unlockChapterQuizResponseModel.Status)
                    {
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
                IsLoading = false;
            }
            finally
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = false;
                });
            }
            return unlockChapterQuizResponseModel;
        }
        /// <summary>
        /// API integration to get all classes of the selected lesson
        /// </summary>
        /// <param name="selectedLesson"></param>
        public async Task GetAllClasses(Models.Lessons.Lessons selectedLesson)
        {
            try
            {
                ClasseslList = new ObservableCollection<ClassModel>();
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = true;
                });
                int classNumber = 0;
                SelectedLessonRequestModel selectedLessonRequestModel = new SelectedLessonRequestModel()
                {
                    SelectedLessonId = selectedLesson.Id,
                    SelectedChapterId = Utilities.SelectedLesson.ChapterId,
                };
                ClassesResponseModel classesResponseModel = await aPIService.GetClassesLessonIdService(selectedLessonRequestModel);
                if (classesResponseModel != null)
                {
                    if (classesResponseModel.StatusCode == 200 && classesResponseModel.Status)
                    {
                        ClasseslList = new ObservableCollection<ClassModel>();
                        SelectedLessonName = selectedLesson.Title;
                        TotalClasses = classesResponseModel.AllClasses.Count;
                        foreach (ClassModel classes in classesResponseModel.AllClasses)
                        {
                            classNumber++;
                            //REMARK: Html content not blur for passes and unlocked classes
                            classes.HtmlWebViewSource = new HtmlWebViewSource()
                            {
                                Html = StringConstant.NotBlurHtmlClassContent,
                            };
                            //REMARK: Html content blur for locked classes
                            classes.BlurWebViewSource = new HtmlWebViewSource()
                            {
                                Html = StringConstant.BlurHtmlClassContent1 + classes.HtmlWebViewSource.Html + StringConstant.BlurHtmlClassContent2,
                            };
                            //REMARK: Html class title blur for locked classes
                            classes.ClassTitleHtml = new HtmlWebViewSource()
                            {
                                Html = StringConstant.BlurClassTitleContent1 + classes.Title + StringConstant.BlurClassTitleContent2,
                            };
                            //REMARK: Html class number blur for locked classes
                            var classNumberText = DFResources.ClassText + " " + classNumber;
                            classes.ClassNumberHtml = new HtmlWebViewSource()
                            {
                                Html = StringConstant.BlurClassNumberContent1 + classNumberText + StringConstant.BlurClassNumberContent2,
                            };
                            //REMARK: Html start question button blur for locked classes
                            classes.StartQuestionButtonHtml = new HtmlWebViewSource()
                            {
                                Html = StringConstant.BlurStartButtonContent1 + DFResources.StartQuestionsText + StringConstant.BlurStartButtonContent2,
                            };
                            if (classes.Status == StringConstant.ClassUnlocked)
                            {
                                classes.IsPasses = false;
                                classes.IsUnLocked = true;
                                classes.IsLocked = false;
                                classes.IsClassNameHtmlVisible = false;
                                classes.IsClassNameVisible = true;
                            }
                            else if (classes.Status == StringConstant.ClassPassed)
                            {
                                classes.IsPasses = true;
                                classes.IsUnLocked = false;
                                classes.IsLocked = false;
                                classes.IsClassNameHtmlVisible = false;
                                classes.IsClassNameVisible = true;
                            }
                            else if (classes.Status == StringConstant.ClassLocked)
                            {
                                classes.IsPasses = false;
                                classes.IsUnLocked = false;
                                classes.IsLocked = true;
                                classes.IsClassNameHtmlVisible = true;
                                classes.IsClassNameVisible = false;
                                classes.ClassTextColor = (Color)Application.Current.Resources["LessonSegmentColor"];
                                classes.ClassNumberTextColor = (Color)Application.Current.Resources["LessonSegmentColor"];
                            }
                            ClasseslList.Add(new ClassModel() { Id = classes.Id, Title = classes.Title, IsClassNameVisible = classes.IsClassNameVisible, IsClassNameHtmlVisible = classes.IsClassNameHtmlVisible, ClassTitleHtml = classes.ClassTitleHtml, ClassNumberHtml = classes.ClassNumberHtml, StartQuestionButtonHtml = classes.StartQuestionButtonHtml, BlurWebViewSource = classes.BlurWebViewSource, Description = classes.Description, Status = classes.Status, ClassTotalXP = classes.ClassTotalXP, IllustrationImage = classes.IllustrationImage, ContentText = classes.ContentText, ContentUrl = classes.ContentUrl, ClassNumber = classNumber, HtmlWebViewSource = classes.HtmlWebViewSource, IsPasses = classes.IsPasses, IsUnLocked = classes.IsUnLocked, IsLocked = classes.IsLocked, ClassNumberTextColor = classes.ClassNumberTextColor, ClassTextColor = classes.ClassTextColor });
                        }
                        ClassesProgressBarModel classesProgressBarList1 = new ClassesProgressBarModel();
                        ClassesProgressBarList = new ObservableCollection<ClassesProgressBarModel>();
                        //REMARK: Add classes in progress bar list and bind the frame color
                        foreach (ClassModel classItem in ClasseslList)
                        {
                            ClassesProgressBarModel classesProgressBarList = new ClassesProgressBarModel
                            {
                                ClassName = classItem.Title,
                                StackWidth = Application.Current.MainPage.Width
                            };
                            if (classItem.IsPasses)
                            {
                                classesProgressBarList.PassedProgressBgFrameColor = (Color)Application.Current.Resources["WhiteText"];
                                classesProgressBarList.PassedBorderFrameColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
                                classesProgressBarList.BorderFrameColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
                            }
                            else if (classItem.IsUnLocked)
                            {
                                classesProgressBarList.PassedProgressBgFrameColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
                                classesProgressBarList.PassedBorderFrameColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
                                classesProgressBarList.BorderFrameColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
                            }
                            else if (classItem.IsLocked)
                            {
                                classesProgressBarList.PassedProgressBgFrameColor = (Color)Application.Current.Resources["WhiteText"];
                                classesProgressBarList.PassedBorderFrameColor = (Color)Application.Current.Resources["LessonSegmentColor"];
                                classesProgressBarList.BorderFrameColor = (Color)Application.Current.Resources["LessonSegmentColor"];
                            }
                            ClassesProgressBarList.Add(classesProgressBarList);
                        }
                        if (Convert.ToInt32(selectedLesson.CompleteClass) == 0)
                        {
                            Utilities.ClassesXPCollected = 0;
                        }
                        //REMARK: Handle the funtionality to bind the class total XP
                        int totalClassXP = 0;
                        foreach (ClassModel classes in ClasseslList)
                        {
                            totalClassXP += classes.ClassTotalXP;
                        }
                        //REMARK: Bind the xp collected progress using the XP collected and total XP of class
                        decimal tempTotalXPCollected = Convert.ToDecimal(totalClassXP);
                        if (Utilities.ClassesXPCollected != 0)
                        {
                            decimal xpCollected = Convert.ToDecimal(Utilities.ClassesXPCollected);
                            XPTokensCollected = Utilities.ClassesXPCollected.ToString();
                            XPTotalProgress = (xpCollected / tempTotalXPCollected);
                            TotalXPCollected = tempTotalXPCollected.ToString();
                            Utilities.TotalXPCollected = TotalXPCollected;
                        }
                        else
                        {
                            XPTotalProgress = 0;
                            XPTokensCollected = Utilities.SelectedLesson.EarnXP;
                            decimal xpCollected = Convert.ToDecimal(Utilities.SelectedLesson.EarnXP);
                            XPTotalProgress = (xpCollected / tempTotalXPCollected);
                            TotalXPCollected = tempTotalXPCollected.ToString();
                            Utilities.TotalXPCollected = TotalXPCollected;
                        }
                        Utilities.ClassesSelectedList = ClasseslList;
                        Utilities.SelectedClassesProgressBarList = ClassesProgressBarList;
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
        /// This method is to refresh all the API's for the lesson module
        /// </summary>
        /// <returns></returns>
        private async Task RefreshStatusAll()
        {
            try
            {
                MessagingCenter.Subscribe<ClassCompleteViewModel, bool>(this, StringConstant.UpdateClassesApi, async (s, e) =>
                {
                    IsLoading = true;
                    await Task.Delay(2000);
                    //REMARK: Handle condition and all status when class is completed
                    int totalClassXP = 0;
                    GetAllChaptersAndLessons();
                    await GetAllClasses(Utilities.SelectedLesson);
                    foreach (ClassModel classes in Utilities.ClassesSelectedList)
                    {
                        try
                        {
                            totalClassXP += classes.ClassTotalXP;
                            decimal xpCollected = Convert.ToDecimal(XPTokensCollected);
                            decimal tempTotalXPCollected = Convert.ToDecimal(totalClassXP);
                            XPTokensCollected = Utilities.ClassXPTokensCollected;
                            XPTotalProgress = (xpCollected / tempTotalXPCollected);
                            TotalXPCollected = tempTotalXPCollected.ToString();

                            if (classes.Status == StringConstant.ClassUnlocked || classes.Status == StringConstant.ClassPassed)
                            {
                                classes.FrameBgColor = Color.Transparent;
                                classes.FrameOpacity = 1.0;
                            }
                            else if (classes.Status == StringConstant.ClassLocked)
                            {
                                classes.FrameBgColor = (Color)Application.Current.Resources["LessonsDetailBgColor"];
                                classes.FrameOpacity = 0.4;
                            }
                            if (classes.Status == StringConstant.ClassUnlocked)
                            {
                                ClassCurrentItem = classes;
                            }
                        }
                        catch (Exception ex)
                        {
                            Crashes.TrackError(ex);
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        /// <summary>
        /// Method to navigate to back 
        /// </summary>
        /// <param name></param>
        /// <returns></returns>
        private async void BackClick()
        {
            try
            {              
                int isLessonOpened = Preferences.Get(StringConstant.IsLessonOnboarding, 0);
                if (isLessonOpened == 0)
                {
                    //REMARK : Handle the navigation when comes from class completed page
                    var modalStack = Application.Current.MainPage.Navigation.ModalStack;
                    string firstPage = modalStack[0].ToString();
                    if (Application.Current.MainPage.Navigation.ModalStack.Count > 3)
                    {
                        if (!Utilities.IsFromClassCompletePage)
                        {
                            try
                            {
                                if (Device.RuntimePlatform == Device.Android)
                                {
                                    //REMARK: Handle the navigation when chapter quiz is completed and back from lessons chapters page
                                    foreach (Page pages in modalStack)
                                    {
                                        await Application.Current.MainPage.Navigation.PopModalAsync();
                                        if (firstPage.Contains(StringConstant.LessonsPage))
                                        {
                                            if (Application.Current.MainPage.Navigation.ModalStack.Count == 1)
                                            {
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            if (firstPage.Contains(StringConstant.LessonChaptersPage))
                                            {
                                                if (Application.Current.MainPage.Navigation.ModalStack.Count == 1)
                                                {
                                                    Application.Current.MainPage = new LessonsPage();
                                                    return;
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (Device.RuntimePlatform == Device.iOS)
                                {
                                    IsLoading = true;
                                    Application.Current.MainPage = new LessonsPage();
                                }
                            }
                            catch (Exception ex)
                            {
                                Crashes.TrackError(ex);
                            }
                        }
                        else
                        {
                            try
                            {
                                Utilities.IsFromClassCompletePage = false;

                                if (Device.RuntimePlatform == Device.Android)
                                {
                                    //REMARK: Handle the navigation when class questions is completed and back from lessons details page
                                    foreach (var modalPages in modalStack)
                                    {
                                        await Application.Current.MainPage.Navigation.PopModalAsync();
                                        if (firstPage.Contains(StringConstant.LessonsPage))
                                        {
                                            if (modalStack[1].ToString().Contains(StringConstant.LessonsPage) && modalStack[2].ToString().Contains(StringConstant.LessonChaptersPage))
                                            {
                                                if (Application.Current.MainPage.Navigation.ModalStack.Count == 3)
                                                {
                                                    return;
                                                }
                                            }
                                            else if (modalStack[1].ToString().Contains(StringConstant.LessonChaptersPage))
                                            {
                                                if (Application.Current.MainPage.Navigation.ModalStack.Count == 2)
                                                {
                                                    return;
                                                }
                                            }
                                        }
                                        else if (firstPage.Contains(StringConstant.LessonChaptersPage))
                                        {
                                            if (Application.Current.MainPage.Navigation.ModalStack.Count == 1)
                                            {
                                                return;
                                            }
                                        }
                                    }
                                }
                                else if (Device.RuntimePlatform == Device.iOS)
                                {
                                    IsLoading = true;
                                    Application.Current.MainPage = new LessonsPage();
                                }
                            }
                            catch (Exception ex)
                            {
                                Crashes.TrackError(ex);
                            }
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.Navigation.PopModalAsync();
                        ChaptersListVisible = true;
                        ClassesPopUpVisible = false;
                    }
                }
                else
                {
                    Application.Current.MainPage = new LessonsPage();
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                IsLoading = false;
            }
        }
        /// <summary>
        /// This method is to handle the cancel click
        /// </summary>
        private void CancelClick()
        {
            try
            {
                ChaptersListVisible = true;
                ClassesPopUpVisible = false;
                ClasseslList.Clear();
                ClassesProgressBarList.Clear();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        /// <summary>
        /// This method is to handle the navigation when click on the continue
        /// </summary>
        private async void ContinueClick()
        {
            try
            {
                if (!Utilities.IsFromLessonsPopUp)
                {
                    Utilities.IsFromLessonsPopUp = true;
                    await Application.Current.MainPage.Navigation.PushModalAsync(new LessonsDetailPage());
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        /// <summary>
        /// This command is used to handle the clicked action of the lesson selected
        /// </summary>
        public Command LessonSelectedCommand
        {
            get
            {
                return new Command(async (param) =>
                {
                    try
                    {
                        var selectedLesson = param as Models.Lessons.Lessons;
                        Utilities.SelectedLesson = selectedLesson;
                        foreach (var chapter in ChaptersList)
                        {
                            if (selectedLesson.ChapterId == chapter.Id)
                            {
                                Utilities.ChaptersProgress = chapter.ChaptersProgressPercentage;
                            }
                        }
                        if (selectedLesson.IsLocked)
                        {
                           await Application.Current.MainPage.DisplayAlert(DFResources.LessonLockedAlertText, DFResources.LessonLockedMsgText, DFResources.OKText);
                        }
                        else
                        {
                            if (selectedLesson.IsPending)
                            {
                                Utilities.IsAllClassCompleted = false;
                                await GetAllClasses(selectedLesson);
                                ChaptersListVisible = false;
                                ClassesPopUpVisible = true;
                            }
                            else if (selectedLesson.IsCompleted)
                            {
                                Utilities.IsAllClassCompleted = true;
                                await GetAllClasses(selectedLesson);
                                int totalClassXP = 0;
                                decimal xpCollected = 0;
                                foreach (ClassModel classes in ClasseslList)
                                {
                                    totalClassXP += classes.ClassTotalXP;
                                }
                                foreach (var data in Utilities.ChaptersListData)
                                {
                                    foreach (var lesson in data.LessonDataList)
                                    {
                                        if (Utilities.SelectedLesson != null)
                                        {
                                            if (Utilities.SelectedLesson.Id == lesson.Id)
                                            {
                                                if (data.Id == Utilities.SelectedLesson.ChapterId)
                                                {
                                                    XPTokensCollected = lesson.EarnXP;
                                                    xpCollected = Convert.ToDecimal(XPTokensCollected);
                                                }
                                            }
                                        }
                                    }
                                }
                                decimal tempTotalXPCollected = Convert.ToDecimal(totalClassXP);
                                XPTotalProgress = xpCollected / tempTotalXPCollected;
                                TotalXPCollected = tempTotalXPCollected.ToString();
                                ChaptersListVisible = false;
                                ClassesPopUpVisible = true;
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
        /// <summary>
        /// This command is used to handle the previous functionality of the class details
        /// </summary>
        public Command PreviousCommand
        {
            get
            {
                return new Command(() =>
                {
                    try
                    {
                        if (ClasseslList.Count > 1)
                        {
                            if (position > 0)
                            {
                                var nextPosition = --Position;
                                Position = nextPosition;
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
        /// <summary>
        /// This command is used to handle the next functionality of the class details
        /// </summary>
        public Command NextCommand
        {
            get
            {
                return new Command(() =>
                {
                    try
                    {
                        if (ClasseslList.Count > 1)
                        {
                            if (Position != ClasseslList.Count - 1)
                            {
                                var nextPosition = ++Position;
                                Position = nextPosition;
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
        #endregion
    }
}
