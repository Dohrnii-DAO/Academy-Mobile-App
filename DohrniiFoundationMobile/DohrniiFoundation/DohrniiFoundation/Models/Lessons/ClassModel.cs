using DohrniiFoundation.Helpers;
using DohrniiFoundation.Views.Lessons;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using Xamarin.Forms;

namespace DohrniiFoundation.Models.Lessons
{
    /// <summary>
    /// This model class is to bind the list of the class
    /// </summary>
    public class ClassModel : ObservableObject
    {
        #region Private Properties
        private int classNumber;
        private HtmlWebViewSource htmlWebViewSource;
        private bool isPasses;
        private bool isUnLocked;
        private bool isLocked;
        private Color classTextColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
        private Color classNumberTextColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
        private Color frameBgColor = (Color)Application.Current.Resources["LessonsDetailBgColor"];
        private double frameOpacity = 0.4;
        private HtmlWebViewSource classTitleHtml;
        private HtmlWebViewSource classNumberHtml;
        private bool isClassNameHtmlVisible;
        private bool isClassNameVisible;
        private HtmlWebViewSource startQuestionButtonHtml;
        #endregion

        #region Public Properties
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("question_limit")]
        public int QuestionLimit { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("class_total_xp")]
        public int ClassTotalXP { get; set; }
        [JsonProperty("illustration_image")]
        public string IllustrationImage { get; set; }
        [JsonProperty("content_url")]
        public string ContentUrl { get; set; }
        [JsonProperty("content_text")]
        public string ContentText { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        public HtmlWebViewSource ClassTitleHtml
        {
            get { return classTitleHtml; }
            set
            {
                if (classTitleHtml != value)
                {
                    classTitleHtml = value;
                    this.OnPropertyChanged(nameof(ClassTitleHtml));
                }
            }
        }
        public HtmlWebViewSource StartQuestionButtonHtml
        {
            get { return startQuestionButtonHtml; }
            set
            {
                if (startQuestionButtonHtml != value)
                {
                    startQuestionButtonHtml = value;
                    this.OnPropertyChanged(nameof(StartQuestionButtonHtml));
                }
            }
        }
        public HtmlWebViewSource ClassNumberHtml
        {
            get { return classNumberHtml; }
            set
            {
                if (classNumberHtml != value)
                {
                    classNumberHtml = value;
                    this.OnPropertyChanged(nameof(ClassNumberHtml));
                }
            }
        }
        public bool IsClassNameHtmlVisible
        {
            get { return isClassNameHtmlVisible; }
            set
            {
                if (isClassNameHtmlVisible != value)
                {
                    isClassNameHtmlVisible = value;
                    this.OnPropertyChanged(nameof(IsClassNameHtmlVisible));
                }
            }
        }
        public bool IsClassNameVisible
        {
            get { return isClassNameVisible; }
            set
            {
                if (isClassNameVisible != value)
                {
                    isClassNameVisible = value;
                    this.OnPropertyChanged(nameof(IsClassNameVisible));
                }
            }
        }
        public int ClassNumber
        {
            get { return classNumber; }
            set
            {
                if (classNumber != value)
                {
                    classNumber = value;
                    this.OnPropertyChanged(nameof(ClassNumber));
                }
            }
        }
        public HtmlWebViewSource HtmlWebViewSource
        {
            get { return htmlWebViewSource; }
            set
            {
                if (htmlWebViewSource != value)
                {
                    htmlWebViewSource = value;
                    this.OnPropertyChanged(nameof(HtmlWebViewSource));
                }
            }
        }
        private HtmlWebViewSource blurWebViewSource;
        public HtmlWebViewSource BlurWebViewSource
        {
            get { return blurWebViewSource; }
            set
            {
                if (blurWebViewSource != value)
                {
                    blurWebViewSource = value;
                    this.OnPropertyChanged(nameof(BlurWebViewSource));
                }
            }
        }
        public bool IsPasses
        {
            get { return isPasses; }
            set
            {
                if (isPasses != value)
                {
                    isPasses = value;
                    this.OnPropertyChanged(nameof(IsPasses));
                }
            }
        }
        public bool IsUnLocked
        {
            get { return isUnLocked; }
            set
            {
                if (isUnLocked != value)
                {
                    isUnLocked = value;
                    OnPropertyChanged(nameof(IsUnLocked));
                }
            }
        }
        public bool IsLocked
        {
            get { return isLocked; }
            set
            {
                if (isLocked != value)
                {
                    isLocked = value;
                    this.OnPropertyChanged(nameof(IsLocked));
                }
            }
        }
        public Color ClassTextColor
        {
            get { return classTextColor; }
            set
            {
                if (classTextColor != value)
                {
                    classTextColor = value;
                    this.OnPropertyChanged(nameof(ClassTextColor));
                }
            }
        }
        public Color ClassNumberTextColor
        {
            get { return classNumberTextColor; }
            set
            {
                if (classNumberTextColor != value)
                {
                    classNumberTextColor = value;
                    this.OnPropertyChanged(nameof(ClassNumberTextColor));
                }
            }
        }
        public Color FrameBgColor
        {
            get { return frameBgColor; }
            set
            {
                if (frameBgColor != value)
                {
                    frameBgColor = value;
                    this.OnPropertyChanged(nameof(FrameBgColor));
                }
            }
        }
        public double FrameOpacity
        {
            get { return frameOpacity; }
            set
            {
                if (frameOpacity != value)
                {
                    frameOpacity = value;
                    OnPropertyChanged(nameof(FrameOpacity));
                }
            }
        }
        #endregion

        #region Command
        /// <summary>
        /// Gets command is used to get the selected answer for which the start question is selected
        /// </summary>
        public Command StartQuestionCommand
        {
            get
            {
                return new Command(async (param) =>
                {
                    try
                    {
                        ClassModel selectedClass = param as ClassModel;
                        Utilities.SelectedClass = selectedClass;
                        Utilities.QuestionType = StringConstant.QuestionClassType;
                        if (selectedClass.Status != StringConstant.ClassLocked)
                        {
                            await Application.Current.MainPage.Navigation.PushModalAsync(new ClassesQuestionPage());
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
