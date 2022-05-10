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
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DohrniiFoundation.ViewModels.Lessons
{
    /// <summary>
    /// View model to bind the class questions with answers and functionality 
    /// </summary>
    public class ClassesQuestionViewModel : ObservableObject
    {
        #region Private Properties
        private static IAPIService aPIService;
        private ObservableCollection<QuestionModel> questionList;
        private string questionTitle;
        private int questionNumber = 0;
        private bool excellentTitleVisible;
        private bool correctAnswerVisible;
        private string answerCorrect;
        private string submitText = DFResources.CheckText;
        private string correctAnswerText;
        private Color checkFrameBgColor = (Color)Application.Current.Resources["LessonSegmentColor"];
        private Color questionNumberTextColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
        private ObservableCollection<QuestionModel> questionListData;
        private ObservableCollection<QuestionsProgressList> questionsProgressBarList;
        private List<QuestionsAttempted> selectedAnswerList;
        private List<int> collectedXPToken;
        private string questionXP;
        #endregion

        #region Public Properties
        public int Index { get; set; } = 0;
        public bool IsAnswerSelected { get; set; } = false;
        public ClassAnswerModel ClassAnswerSelected = new ClassAnswerModel();
        public ObservableCollection<QuestionModel> QuestionListData
        {
            get { return questionListData; }
            set
            {
                if (questionListData != value)
                {
                    questionListData = value;
                    this.OnPropertyChanged(nameof(QuestionListData));
                }
            }
        }
        public ObservableCollection<QuestionModel> QuestionList
        {
            get { return questionList; }
            set
            {
                if (questionList != value)
                {
                    questionList = value;
                    this.OnPropertyChanged(nameof(QuestionList));
                }
            }
        }
        public List<QuestionsAttempted> SelectedAnswerList
        {
            get { return selectedAnswerList; }
            set
            {
                if (selectedAnswerList != value)
                {
                    selectedAnswerList = value;
                    this.OnPropertyChanged(nameof(SelectedAnswerList));
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
        public bool ExcellentTitleVisible
        {
            get
            {
                return excellentTitleVisible;
            }
            set
            {
                if (excellentTitleVisible != value)
                {
                    excellentTitleVisible = value;
                    this.OnPropertyChanged(nameof(ExcellentTitleVisible));
                }
            }
        }
        public bool CorrectAnswerVisible
        {
            get
            {
                return correctAnswerVisible;
            }
            set
            {
                if (correctAnswerVisible != value)
                {
                    correctAnswerVisible = value;
                    this.OnPropertyChanged(nameof(CorrectAnswerVisible));
                }
            }
        }
        public string CorrectAnswerText
        {
            get
            {
                return correctAnswerText;
            }
            set
            {
                if (correctAnswerText != value)
                {
                    correctAnswerText = value;
                    this.OnPropertyChanged(nameof(CorrectAnswerText));
                }
            }
        }
        public string AnswerCorrect
        {
            get
            {
                return this.answerCorrect;
            }
            set
            {
                if (this.answerCorrect != value)
                {
                    this.answerCorrect = value;
                    this.OnPropertyChanged(nameof(this.AnswerCorrect));
                }
            }
        }
        public string SubmitText
        {
            get { return submitText; }
            set
            {
                if (submitText != value)
                {
                    submitText = value;
                    OnPropertyChanged(nameof(SubmitText));
                }
            }
        }
        public Color CheckFrameBgColor
        {
            get
            {
                return checkFrameBgColor;
            }
            set
            {
                if (checkFrameBgColor != value)
                {
                    checkFrameBgColor = value;
                    this.OnPropertyChanged(nameof(CheckFrameBgColor));
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
        public List<int> CollectedXPToken
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
        public string QuestionXP
        {
            get { return questionXP; }
            set
            {
                if (questionXP != value)
                {
                    questionXP = value;
                    this.OnPropertyChanged(nameof(QuestionXP));
                }
            }
        }
        public ICommand ContinueCommand { get; set; }
        #endregion

        #region Constructor
        public ClassesQuestionViewModel()
        {
            try
            {
                SelectedAnswerList = new List<QuestionsAttempted>();
                CollectedXPToken = new List<int>();
                ContinueCommand = new Command(ContinueClick);
                aPIService = new APIServices();
                QuestionList = new ObservableCollection<QuestionModel>();
                QuestionListData = new ObservableCollection<QuestionModel>();
                GetClassQuestion();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// This method is to get the selected class questions with answers and progress bar
        /// </summary>
        private async void GetClassQuestion()
        {
            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = true;
                });
                QuestionModel questionModel = new QuestionModel();
                SelectedClassRequestModel selectedClassRequestModel = new SelectedClassRequestModel()
                {
                    ClassId = Utilities.SelectedClass.Id,
                };
                ClassesQuestionsResponseModel classesQuestionsResponseModel = await aPIService.GetQuestionsClassIdService(selectedClassRequestModel);
                if (classesQuestionsResponseModel != null)
                {
                    if (classesQuestionsResponseModel.StatusCode == 200 && classesQuestionsResponseModel.Status)
                    {
                        if (classesQuestionsResponseModel.ClassQuestions != null)
                        {
                            QuestionsProgressBarList = new ObservableCollection<QuestionsProgressList>();
                            QuestionNumber++;
                            foreach (QuestionModel question in classesQuestionsResponseModel.ClassQuestions)
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
                                if (QuestionsProgressBarList.Count == classesQuestionsResponseModel.ClassQuestions.Count)
                                {
                                    QuestionsProgressBarList.Add(questionsProgressList);
                                }
                                foreach (ClassAnswerModel answer in question.ClassAnswersList)
                                {
                                    answer.AnswerAlphabet = answerAlpha++;
                                }
                                QuestionXP = question.XPToken;
                                QuestionListData.Add(new QuestionModel() { QuestionTitle = question.QuestionTitle, QuesId = question.QuesId, XPToken = question.XPToken, ClassAnswersList = question.ClassAnswersList });
                            }
                            //REMARK: To show only 1st index list on first time
                            questionModel = QuestionListData[Index];
                            QuestionList.Add(questionModel);
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, DFResources.SomethingWrongText, DFResources.OKText);
                        }
                    }
                    else if (classesQuestionsResponseModel.StatusCode == 202 && !classesQuestionsResponseModel.Status)
                    {
                        await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, classesQuestionsResponseModel.Message, DFResources.OKText);
                    }
                    else
                    {
                        if (classesQuestionsResponseModel.StatusCode == 501 || classesQuestionsResponseModel.StatusCode == 401 || classesQuestionsResponseModel.StatusCode == 400 || classesQuestionsResponseModel.StatusCode == 404)
                        {
                            await Application.Current.MainPage.Navigation.PushModalAsync(new ResponseErrorPage());
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, classesQuestionsResponseModel.Message, DFResources.OKText);
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
        /// This method is used to handle the click functionality to change the text of button check, continue and submit the questions
        /// </summary>
        private async void ContinueClick()
        {
            try
            {
                if (SubmitText == DFResources.CheckText)
                {
                    if (IsAnswerSelected)
                    {
                        if (ClassAnswerSelected.CorrectAnswer == StringConstant.CorrectAnswerValueOne)
                        {
                            ExcellentTitleVisible = true;
                            CorrectAnswerVisible = false;
                            CheckFrameBgColor = (Color)Application.Current.Resources["QuestionCorrectGreenColor"];
                            ClassAnswerSelected.AnswerFrameTextColor = (Color)Application.Current.Resources["QuestionCorrectGreenColor"];
                            ClassAnswerSelected.AnswerAlphabetTextColor = (Color)Application.Current.Resources["WhiteText"];
                            ClassAnswerSelected.AnswerAlphabetBorderColor = (Color)Application.Current.Resources["QuestionCorrectGreenColor"];
                            ClassAnswerSelected.AlphabetFrameBgColor = (Color)Application.Current.Resources["QuestionCorrectGreenColor"];
                            QuestionNumberTextColor = (Color)Application.Current.Resources["QuestionCorrectGreenColor"];
                            QuestionsProgressBarList[Index].ProgressBarFrameBorderColor = (Color)Application.Current.Resources["QuestionCorrectGreenColor"];
                            QuestionsProgressBarList[Index].ProgressBarFrameBgColor = (Color)Application.Current.Resources["QuestionCorrectGreenColor"];
                        }
                        else
                        {
                            if (ClassAnswerSelected.CorrectAnswer == StringConstant.WrongAnswerValueZero)
                            {
                                ExcellentTitleVisible = false;
                                CorrectAnswerVisible = true;
                                foreach (QuestionModel questions in QuestionList)
                                {
                                    foreach (ClassAnswerModel correctAnswer in questions.ClassAnswersList)
                                    {
                                        if (correctAnswer.CorrectAnswer == StringConstant.CorrectAnswerValueOne)
                                        {
                                            CorrectAnswerText = correctAnswer.AnswerAlphabet.ToString();
                                        }
                                    }
                                }
                                CheckFrameBgColor = (Color)Application.Current.Resources["QuestionWrongRedColor"];
                                ClassAnswerSelected.AnswerFrameTextColor = (Color)Application.Current.Resources["QuestionWrongRedColor"];
                                ClassAnswerSelected.AnswerAlphabetTextColor = (Color)Application.Current.Resources["WhiteText"];
                                ClassAnswerSelected.AnswerAlphabetBorderColor = (Color)Application.Current.Resources["QuestionWrongRedColor"];
                                ClassAnswerSelected.AlphabetFrameBgColor = (Color)Application.Current.Resources["QuestionWrongRedColor"];
                                QuestionNumberTextColor = (Color)Application.Current.Resources["QuestionWrongRedColor"];
                                QuestionsProgressBarList[Index].ProgressBarFrameBorderColor = (Color)Application.Current.Resources["QuestionWrongRedColor"];
                                QuestionsProgressBarList[Index].ProgressBarFrameBgColor = (Color)Application.Current.Resources["QuestionWrongRedColor"];
                            }
                        }
                        IsAnswerSelected = false;
                        SubmitText = QuestionNumber == QuestionListData.Count ? DFResources.SubmitText : DFResources.ContinueText;
                    }
                }
                else if (SubmitText == DFResources.ContinueText)
                {
                    QuestionModel questionModel1 = new QuestionModel();
                    QuestionList = new ObservableCollection<QuestionModel>();
                    QuestionNumber++;
                    Index++;
                    questionModel1 = QuestionListData[Index];
                    QuestionList.Add(questionModel1);
                    SubmitText = DFResources.CheckText;
                    IsAnswerSelected = false;
                    ExcellentTitleVisible = false;
                    CorrectAnswerVisible = false;
                    CheckFrameBgColor = (Color)Application.Current.Resources["LessonSegmentColor"];
                    QuestionNumberTextColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
                    foreach (QuestionModel questions in QuestionListData)
                    {
                        foreach (ClassAnswerModel answers in questions.ClassAnswersList)
                        {
                            answers.AnswerFrameTextColor = (Color)Application.Current.Resources["BlackTextColor"];
                            answers.AnswerAlphabetTextColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
                            answers.AnswerAlphabetBorderColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
                            answers.AlphabetFrameBgColor = (Color)Application.Current.Resources["WhiteText"];
                        }
                    }
                }
                else
                {
                    if (QuestionNumber == QuestionListData.Count)
                    {

                        Device.BeginInvokeOnMainThread(() =>
                        {
                            IsLoading = true;
                        });
                        //REMARK: Handle the functionality to implement to not to submit completed lessons questions  
                        if (!Utilities.IsAllClassCompleted)
                        {
                            if (Utilities.SelectedClass.Status != StringConstant.ClassPassed)
                            {
                                //REMARK: submit API integration to submit the class questions with answers
                                int userId = Preferences.Get(StringConstant.UserId, 0) == 0 ? 0 : Preferences.Get(StringConstant.UserId, 0);
                                int collectedClassXP = 0;
                                foreach (int collectedXP in CollectedXPToken)
                                {
                                    collectedClassXP += collectedXP;
                                }
                                Utilities.XPClassRewarded = collectedClassXP;
                                SubmitClassQuestionsRequestModel submitClassQuestionsRequestModel = new SubmitClassQuestionsRequestModel()
                                {
                                    UserId = userId.ToString(),
                                    ChapterId = Utilities.SelectedLesson.ChapterId.ToString(),
                                    Lessonsid = Utilities.SelectedLesson.Id.ToString(),
                                    ClassId = Utilities.SelectedClass.Id.ToString(),
                                    CollectXP = collectedClassXP.ToString(),
                                    Type = Utilities.QuestionType,
                                    QuestionsAttempted = SelectedAnswerList,
                                };
                                SubmitClassQuestionsResponseModel submitClassQuestionsResponseModel = await aPIService.SubmitSelectedClassAnswersService(submitClassQuestionsRequestModel);
                                if (submitClassQuestionsResponseModel != null)
                                {
                                    if (submitClassQuestionsResponseModel.StatusCode == 200 && submitClassQuestionsResponseModel.Status)
                                    {
                                        //REMARK: Get the random number to change the chapters quiz UI
                                        Random randomNumber = new Random();
                                        int number = randomNumber.Next(0, 5);
                                        Utilities.ClassQuestionsRandom = number;
                                        await Application.Current.MainPage.Navigation.PushModalAsync(new ClassCompletePage());
                                    }
                                    else if (submitClassQuestionsResponseModel.StatusCode == 202 && !submitClassQuestionsResponseModel.Status)
                                    {
                                        await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, submitClassQuestionsResponseModel.Message, DFResources.OKText);
                                    }
                                    else
                                    {
                                        if (submitClassQuestionsResponseModel.StatusCode == 501 || submitClassQuestionsResponseModel.StatusCode == 401 || submitClassQuestionsResponseModel.StatusCode == 400 || submitClassQuestionsResponseModel.StatusCode == 404)
                                        {
                                            await Application.Current.MainPage.Navigation.PushModalAsync(new ResponseErrorPage());
                                        }
                                        else
                                        {
                                            await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, submitClassQuestionsResponseModel.Message, DFResources.OKText);
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
                                //REMARK: Get the random number to change the chapters quiz UI
                                Random randomNumber = new Random();
                                int number = randomNumber.Next(0, 5);
                                Utilities.ClassQuestionsRandom = number;
                                Utilities.XPClassRewarded = 0;
                                await Application.Current.MainPage.Navigation.PushModalAsync(new ClassCompletePage());
                                Utilities.IsAllClassCompleted = false;
                            }
                        }
                        else
                        {
                            //REMARK: Get the random number to change the chapters quiz UI
                            Random randomNumber = new Random();
                            int number = randomNumber.Next(0, 5);
                            Utilities.ClassQuestionsRandom = number;
                            Utilities.XPClassRewarded = 0;
                            await Application.Current.MainPage.Navigation.PushModalAsync(new ClassCompletePage());
                            Utilities.IsAllClassCompleted = false;
                        }
                    }
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
        /// Gets command is to handle the functionality when click on the answers of the class question
        /// </summary>
        public Command AnswerSelectedCommand
        {
            get
            {
                return new Command((param) =>
                {
                    try
                    {
                        ClassAnswerModel selectedAns = param as ClassAnswerModel;
                        int selectedQuestionId = 0;
                        if (SubmitText == DFResources.CheckText)
                        {
                            int isCorrect = 0;
                            if (!IsAnswerSelected)
                            {
                                ClassAnswerSelected = selectedAns;
                                if (ClassAnswerSelected.CorrectAnswer == StringConstant.CorrectAnswerValueOne)
                                {
                                    isCorrect = 1;
                                    foreach (QuestionModel questions in QuestionList)
                                    {
                                        foreach (var lesson in questions.ClassAnswersList)
                                        {
                                            if (lesson.Id == selectedAns.Id)
                                            {
                                                CollectedXPToken.Add(Convert.ToInt32(questions.XPToken));
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    isCorrect = 0;
                                }
                                foreach (QuestionModel ques in QuestionList)
                                {
                                    foreach (ClassAnswerModel ans in ques.ClassAnswersList)
                                    {
                                        if (ans.Id == selectedAns.Id)
                                        {
                                            selectedQuestionId = ques.QuesId;
                                        }
                                    }
                                }
                                SelectedAnswerList.Add(new QuestionsAttempted() { QuestionId = selectedQuestionId.ToString(), SelectedAnswerId = selectedAns.Id.ToString(), IsCorrect = isCorrect.ToString() });
                                ExcellentTitleVisible = false;
                                CorrectAnswerVisible = false;
                                CheckFrameBgColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
                                selectedAns.AnswerFrameTextColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
                                selectedAns.AnswerAlphabetTextColor = (Color)Application.Current.Resources["WhiteText"];
                                selectedAns.AnswerAlphabetBorderColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
                                selectedAns.AlphabetFrameBgColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
                                QuestionNumberTextColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
                                IsAnswerSelected = true;
                                QuestionsProgressBarList[Index].ProgressBarFrameBorderColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
                                QuestionsProgressBarList[Index].ProgressBarFrameBgColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
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


