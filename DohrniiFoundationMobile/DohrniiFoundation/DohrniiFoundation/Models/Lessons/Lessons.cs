using DohrniiFoundation.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace DohrniiFoundation.Models.Lessons
{
    /// <summary>
    /// This molel class is to bind the chapters lessons
    /// </summary>
    public class Lessons : ObservableObject
    {
        #region Private Properties
        private bool isCompleted;
        private bool isPending;
        private bool isLocked;
        private bool isProgressBar;
        private Color frameBackgroundColor;
        private Color lessonTextColor;
        private Color lessonCountBackgroundColor;
        private string classPercentage;
        #endregion

        #region Properties
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("chapter_id")]
        public int ChapterId { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("earn_xp")]
        public string EarnXP { get; set; }
        [JsonProperty("total_link_class")]
        public int TotalLinkClass { get; set; }
        [JsonProperty("complete_class")]
        public string CompleteClass { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty("deleted_at")]
        public object DeletedAt { get; set; }
        public string LessonsTick { get; set; } = StringConstant.LessonsTick;
        public string LessonLock { get; set; } = StringConstant.LessonLock;
        public int LessonsNumber { get; set; }
        public ObservableCollection<ProgressBarModel> ProgressBarList { get; set; }
        public int ChapterProgress { get; set; }
        public string LessonsCompletedClass { get; set; }
        public bool IsCompleted
        {
            get { return isCompleted; }
            set
            {
                if (isCompleted != value)
                {
                    isCompleted = value;
                    this.OnPropertyChanged(nameof(IsCompleted));
                }
            }
        }
        public bool IsPending
        {
            get { return isPending; }
            set
            {
                if (isPending != value)
                {
                    isPending = value;
                    this.OnPropertyChanged(nameof(IsPending));
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
        public bool IsProgressBar
        {
            get { return isProgressBar; }
            set
            {
                if (isProgressBar != value)
                {
                    isProgressBar = value;
                    this.OnPropertyChanged(nameof(IsProgressBar));
                }
            }
        }
        public Color FrameBackgroundColor
        {
            get { return frameBackgroundColor; }
            set
            {
                if (frameBackgroundColor != value)
                {
                    frameBackgroundColor = value;
                    this.OnPropertyChanged(nameof(FrameBackgroundColor));
                }
            }
        }
        public Color LessonTextColor
        {
            get { return lessonTextColor; }
            set
            {
                if (lessonTextColor != value)
                {
                    lessonTextColor = value;
                    this.OnPropertyChanged(nameof(LessonTextColor));
                }
            }
        }
        public Color LessonCountBackgroundColor
        {
            get { return lessonCountBackgroundColor; }
            set
            {
                if (lessonCountBackgroundColor != value)
                {
                    lessonCountBackgroundColor = value;
                    this.OnPropertyChanged(nameof(LessonCountBackgroundColor));
                }
            }
        }
        public string ClassPercentage
        {
            get { return classPercentage; }
            set
            {
                if (classPercentage != value)
                {
                    classPercentage = value;
                    this.OnPropertyChanged(nameof(ClassPercentage));
                }
            }
        }
        #endregion
    }
}
