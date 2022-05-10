using DohrniiFoundation.Helpers;
using DohrniiFoundation.IServices;
using DohrniiFoundation.Models.APIRequestModel.Lessons;
using DohrniiFoundation.Models.APIResponseModels.Lessons;
using DohrniiFoundation.Models.Lessons;
using DohrniiFoundation.Resources;
using DohrniiFoundation.Services;
using DohrniiFoundation.Views;
using DohrniiFoundation.Views.Lessons;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DohrniiFoundation.ViewModels.Lessons
{
    /// <summary>
    /// View model to bind the class quiz with answers and functionality 
    /// </summary>
    public class ClassQuizViewModel : ObservableObject
    {
        #region Private Properties
        private static IAPIService aPIService;
        private ObservableCollection<ChapterQuizModel> chapterQuizDataList;
        private ObservableCollection<ChapterQuizModel> chapterQuizList;
        private string questionTitle;
        private int questionNumber = 0;
        private Color questionNumberTextColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
        private ObservableCollection<QuestionsProgressList> questionsProgressBarList;
        private List<QuizAttempted> selectedQuizAnswerList;
        private List<string> collectedXPToken;
        private int quizCounterSeconds = 0;
        Timer timer = new Timer();
        private string quizTimer;
        private bool isClassQuizCompleted;
        private List<int> correctAnswerList;
        #endregion

        #region Public Properties
        public int Index { get; set; } = 0;
        public bool IsAnswerSelected { get; set; } = false;
        public QuizAnswerModel QuizAnswerSelected = new QuizAnswerModel();
        public ObservableCollection<ChapterQuizModel> ChapterQuizDataList
        {
            get { return chapterQuizDataList; }
            set
            {
                if (chapterQuizDataList != value)
                {
                    chapterQuizDataList = value;
                    this.OnPropertyChanged(nameof(ChapterQuizDataList));
                }
            }
        }
        public ObservableCollection<ChapterQuizModel> ChapterQuizList
        {
            get { return chapterQuizList; }
            set
            {
                if (chapterQuizList != value)
                {
                    chapterQuizList = value;
                    this.OnPropertyChanged(nameof(ChapterQuizList));
                }
            }
        }
        public List<QuizAttempted> SelectedQuizAnswerList
        {
            get { return selectedQuizAnswerList; }
            set
            {
                if (selectedQuizAnswerList != value)
                {
                    selectedQuizAnswerList = value;
                    this.OnPropertyChanged(nameof(SelectedQuizAnswerList));
                }
            }
        }
        public int QuestionNumber
        {
            get { return questionNumber; }
            set
            {
                if (questionNumber != value)
                {
                    questionNumber = value;
                    this.OnPropertyChanged(nameof(QuestionNumber));
                }
            }
        }
        public string QuestionTitle
        {
            get { return questionTitle; }
            set
            {
                if (questionTitle != value)
                {
                    questionTitle = value;
                    this.OnPropertyChanged(nameof(QuestionTitle));
                }
            }
        }
        public Color QuestionNumberTextColor
        {
            get
            {
                return questionNumberTextColor;
            }
            set
            {
                if (questionNumberTextColor != value)
                {
                    questionNumberTextColor = value;
                    OnPropertyChanged(nameof(QuestionNumberTextColor));
                }
            }
        }
        public ObservableCollection<QuestionsProgressList> QuestionsProgressBarList
        {
            get { return questionsProgressBarList; }
            set
            {
                if (questionsProgressBarList != value)
                {
                    questionsProgressBarList = value;
                    this.OnPropertyChanged(nameof(questionsProgressBarList));
                }
            }
        }
        public List<string> CollectedXPToken
        {
            get { return collectedXPToken; }
            set
            {
                if (collectedXPToken != value)
                {
                    collectedXPToken = value;
                    this.OnPropertyChanged(nameof(CollectedXPToken));
                }
            }
        }
        public string QuizTimer
        {
            get { return quizTimer; }
            set
            {
                if (quizTimer != value)
                {
                    quizTimer = value;
                    this.OnPropertyChanged(nameof(QuizTimer));
                }
            }
        }
        public List<int> CorrectAnswerList
        {
            get { return correctAnswerList; }
            set
            {
                if (correctAnswerList != value)
                {
                    correctAnswerList = value;
                    this.OnPropertyChanged(nameof(CorrectAnswerList));
                }
            }
        }
        public bool IsClassQuizCompleted
        {
            get { return isClassQuizCompleted; }
            set
            {
                if (isClassQuizCompleted != value)
                {
                    isClassQuizCompleted = value;
                    this.OnPropertyChanged(nameof(IsClassQuizCompleted));
                }
            }
        }
        #endregion

        #region Constructor
        public ClassQuizViewModel()
        {
            try
            {
                aPIService = new APIServices();
                SelectedQuizAnswerList = new List<QuizAttempted>();
                CollectedXPToken = new List<string>();
                ChapterQuizList = new ObservableCollection<ChapterQuizModel>();
                ChapterQuizDataList = new ObservableCollection<ChapterQuizModel>();
                CorrectAnswerList = new List<int>();
                GetQuizQuestion();
                StartQuizTimer();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// This methods starts the timer and bind with UI
        /// </summary>
        private void StartQuizTimer()
        {
            try
            {
                TimeSpan time = TimeSpan.FromSeconds(Utilities.QuizSelectedChapter.QuizTimer);
                quizCounterSeconds = Utilities.QuizSelectedChapter.QuizTimer;
                QuizTimer = time.ToString(@"mm\:ss");
                timer.Interval = 1000;
                timer.Elapsed += Timer_Elapsed; ;
                timer.Start();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        /// <summary>
        /// This methods handles the counter of the timer and integtrates submit API after counter gets 0
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    quizCounterSeconds--;
                    TimeSpan time = TimeSpan.FromSeconds(quizCounterSeconds);
                    QuizTimer = time.ToString(@"mm\:ss");
                    if (quizCounterSeconds == 0)
                    {
                        timer.Stop();
                        await SubmitChaptersQuiz();
                    }

                });
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
        /// This method is to submit chapter quiz when chapter is completed
        /// </summary>
        /// <returns></returns>
        private async Task SubmitChaptersQuiz()
        {
            try
            {
                if (!Utilities.QuizSelectedChapter.IsQuizCompleted)
                {
                    //REMARK: submit API integration to submit the class questions with answers
                    int userId = Preferences.Get(StringConstant.UserId, 0) == 0 ? 0 : Preferences.Get(StringConstant.UserId, 0);
                    Utilities.QuestionType = StringConstant.QuestionQuizType;
                    decimal collectedXP = Convert.ToDecimal(CorrectAnswerList.Count);
                    decimal totalQuestions = Convert.ToDecimal(ChapterQuizDataList.Count);
                    decimal percentage = collectedXP / totalQuestions;
                    string correctQuestionPercentage = Math.Round(percentage * 100, MidpointRounding.ToEven).ToString();
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        IsLoading = true;
                    });
                    SubmitChapterQuizRequestModel submitChapterQuizRequestModel = new SubmitChapterQuizRequestModel()
                    {
                        UserId = userId,
                        ChapterId = Utilities.QuizSelectedChapter.Id,
                        Type = StringConstant.QuestionQuizType,
                        CorrectQuestionPercentage = Convert.ToInt32(correctQuestionPercentage),
                        QuestionsAttempted = SelectedQuizAnswerList,
                    };
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        IsLoading = false;
                    });
                    SubmitChapterQuizResponseModel submitChapterQuizResponseModel = await aPIService.SubmitSelectedChapterQuizService(submitChapterQuizRequestModel);
                    if (submitChapterQuizResponseModel != null)
                    {
                        if (submitChapterQuizResponseModel.StatusCode == 200 && submitChapterQuizResponseModel.Status)
                        {
                            //REMARK: Get the random number to change the chapters quiz UI
                            Random randomNumber = new Random();
                            int number = randomNumber.Next(0, 2);
                            Utilities.ChapterQuizRandom = number;

                            decimal collectedDHN = Convert.ToDecimal(submitChapterQuizResponseModel.SubmitChapterQuizData.CollectDHN);
                            Utilities.CollectDHN = Math.Round(collectedDHN, MidpointRounding.ToEven).ToString();
                            await Application.Current.MainPage.Navigation.PushModalAsync(new QuizCompletePage());
                        }
                        else if (submitChapterQuizResponseModel.StatusCode == 202 && !submitChapterQuizResponseModel.Status)
                        {
                            await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, submitChapterQuizResponseModel.Message, DFResources.OKText);
                        }
                        else
                        {
                            if (submitChapterQuizResponseModel.StatusCode == 501 || submitChapterQuizResponseModel.StatusCode == 401 || submitChapterQuizResponseModel.StatusCode == 400 || submitChapterQuizResponseModel.StatusCode == 404)
                            {
                                await Application.Current.MainPage.Navigation.PushModalAsync(new ResponseErrorPage());
                            }
                            else
                            {
                                await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, submitChapterQuizResponseModel.Message, DFResources.OKText);
                            }
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert(DFResources.OopsText, DFResources.SomethingWrongText, DFResources.OKText);
                    }
                }
                else
                {
                    IsClassQuizCompleted = true;
                    //REMARK: Get the random number to change the chapters quiz UI
                    Random randomNumber = new Random();
                    int number = randomNumber.Next(0, 2);
                    Utilities.ChapterQuizRandom = number;
                    Utilities.CollectDHN = "0";
                    if (IsClassQuizCompleted)
                    {
                        IsClassQuizCompleted = false;
                        await Application.Current.MainPage.Navigation.PushModalAsync(new QuizCompletePage());
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        /// <summary>
        /// This method is used to get the quiz questions of the completed chapter from API
        /// </summary>
        private async void GetQuizQuestion()
        {
            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = true;
                });
                ChapterQuizModel chapterQuizModel = new ChapterQuizModel();
                UnlockChapterQuizRequestModel chapterQuizRequestModel = new UnlockChapterQuizRequestModel()
                {
                    ChapterId = Utilities.QuizSelectedChapter.Id,
                };
                ChapterQuizResponseModel chapterQuizResponseModel = await aPIService.GetChapterQuizService(chapterQuizRequestModel);
                if (chapterQuizResponseModel != null)
                {
                    if (chapterQuizResponseModel.StatusCode == 200 && chapterQuizResponseModel.Status)
                    {
                        if (chapterQuizResponseModel.QuizQuestions != null)
                        {
                            QuestionsProgressBarList = new ObservableCollection<QuestionsProgressList>();
                            QuestionNumber++;

                            foreach (ChapterQuizModel question in chapterQuizResponseModel.QuizQuestions)
                            {
                                char answerAlpha = StringConstant.AnswerAlphabet;
                                //REMARK: Handle the progress bar of all the answers of the question                                   
                                QuestionsProgressBarList.Add(new QuestionsProgressList() { QuestionsName = question.QuestionTitle, StackWidth = Application.Current.MainPage.Width, ProgressBarFrameBgColor = (Color)Application.Current.Resources["WhiteText"], ProgressBarFrameBorderColor = (Color)Application.Current.Resources["LessonSegmentColor"] });
                                //REAMRK: Add extra progress frame to handle the UI functionality
                                QuestionsProgressList questionsProgressList = new QuestionsProgressList()
                                {
                                    QuestionsName = StringConstant.LastItemText,
                                    StackWidth = Application.Current.MainPage.Width,
                                    ProgressBarFrameBgColor = (Color)Application.Current.Resources["WhiteText"],
                                    ProgressBarFrameBorderColor = (Color)Application.Current.Resources["WhiteText"],
                                };
                                if (QuestionsProgressBarList.Count == chapterQuizResponseModel.QuizQuestions.Count)
                                {
                                    QuestionsProgressBarList.Add(questionsProgressList);
                                }
                                foreach (QuizAnswerModel answer in question.QuizAnswers)
                                {
                                    answer.AnswerAlphabet = answerAlpha++;
                                }
                                ChapterQuizDataList.Add(new ChapterQuizModel() { Id = question.Id, QuestionTitle = question.QuestionTitle, QuizAnswers = question.QuizAnswers });
                            }
                            //REMARK: To show only 1st index list on first time
                            chapterQuizModel = ChapterQuizDataList[Index];
                            ChapterQuizList.Add(chapterQuizModel);
                        }
                    }
                    else if (chapterQuizResponseModel.StatusCode == 202 && !chapterQuizResponseModel.Status)
                    {
                        await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, chapterQuizResponseModel.Message, DFResources.OKText);
                    }
                    else
                    {
                        if (chapterQuizResponseModel.StatusCode == 501 || chapterQuizResponseModel.StatusCode == 401 || chapterQuizResponseModel.StatusCode == 400 || chapterQuizResponseModel.StatusCode == 404)
                        {
                            await Application.Current.MainPage.Navigation.PushModalAsync(new ResponseErrorPage());
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, chapterQuizResponseModel.Message, DFResources.OKText);
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
        /// Method to pop to back 
        /// </summary>
        /// <param name></param>
        /// <returns></returns>
        private async void BackClick()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        /// <summary>
        /// Gets command is to handle the functionality when click on the answers of the class question
        /// </summary>
        public Command AnswerSelectedCommand
        {
            get
            {
                return new Command(async (param) =>
                {
                    try
                    {
                        QuizAnswerModel selectedAns = param as QuizAnswerModel;
                        var selectedChapterId = 0;
                        int isCorrect = 0;
                        if (!IsAnswerSelected)
                        {
                            //REMARK: Handle the funtionality according to the changed design
                            if (selectedAns != null)
                            {
                                QuizAnswerSelected = selectedAns;
                                foreach (var ques in ChapterQuizList)
                                {
                                    foreach (var ans in ques.QuizAnswers)
                                    {
                                        if (ans.AnswerId == selectedAns.AnswerId)
                                        {
                                            selectedChapterId = ques.Id;
                                        }
                                    }
                                }
                                if (QuizAnswerSelected.CorrectAnswer == StringConstant.CorrectAnswerValueOne)
                                {
                                    isCorrect = 1;
                                    selectedAns.AnswerFrameTextColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
                                    selectedAns.AnswerAlphabetTextColor = (Color)Application.Current.Resources["WhiteText"];
                                    selectedAns.AnswerAlphabetBorderColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
                                    selectedAns.AlphabetFrameBgColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
                                    QuestionNumberTextColor = (Color)Application.Current.Resources["QuestionCorrectGreenColor"];
                                    QuestionsProgressBarList[Index].ProgressBarFrameBorderColor = (Color)Application.Current.Resources["QuestionCorrectGreenColor"];
                                    QuestionsProgressBarList[Index].ProgressBarFrameBgColor = (Color)Application.Current.Resources["QuestionCorrectGreenColor"];
                                    CorrectAnswerList.Add(isCorrect);
                                }
                                else
                                {
                                    isCorrect = 0;
                                    selectedAns.AnswerFrameTextColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
                                    selectedAns.AnswerAlphabetTextColor = (Color)Application.Current.Resources["WhiteText"];
                                    selectedAns.AnswerAlphabetBorderColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
                                    selectedAns.AlphabetFrameBgColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
                                    QuestionNumberTextColor = (Color)Application.Current.Resources["QuestionWrongRedColor"];
                                    QuestionsProgressBarList[Index].ProgressBarFrameBorderColor = (Color)Application.Current.Resources["QuestionWrongRedColor"];
                                    QuestionsProgressBarList[Index].ProgressBarFrameBgColor = (Color)Application.Current.Resources["QuestionWrongRedColor"];
                                }
                                SelectedQuizAnswerList.Add(new QuizAttempted() { QuestionId = selectedChapterId, SelectedAnswerId = selectedAns.AnswerId, IsCorrect = isCorrect });
                            }
                            Index++;
                            if (Index > ChapterQuizDataList.Count)
                            {
                                Index = 0;
                            }
                            else
                            {
                                if (Index <= ChapterQuizDataList.Count - 1)
                                {
                                    ChapterQuizModel quizquestion = new ChapterQuizModel();
                                    ChapterQuizList = new ObservableCollection<ChapterQuizModel>();
                                    QuestionNumber++;
                                    quizquestion = ChapterQuizDataList[Index];
                                    ChapterQuizList.Add(quizquestion);
                                }
                                else
                                {
                                    if (quizCounterSeconds != 0)
                                    {
                                        timer.Stop();
                                        await SubmitChaptersQuiz();
                                    }
                                }
                            }
                            IsAnswerSelected = false;
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
