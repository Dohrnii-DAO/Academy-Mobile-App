using DohrniiFoundation.Helpers;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace DohrniiFoundation.Models.Lessons
{
    /// <summary>
    /// This model class is to bind the quiz answers
    /// </summary>
    public class QuizAnswerModel : ObservableObject
    {
        #region Private Properties
        private Color answerFrameTextColor = (Color)Application.Current.Resources["BlackTextColor"];
        private Color answerAlphabetTextColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
        private Color answerAlphabetBorderColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
        private Color alphabetFrameBgColor = (Color)Application.Current.Resources["WhiteText"];
        #endregion

        #region Public Properties

        [JsonProperty("answer_id")]
        public int AnswerId { get; set; }
        [JsonProperty("answer_title")]
        public string AnswerTitle { get; set; }
        [JsonProperty("correct_answer")]
        public string CorrectAnswer { get; set; }
        public char AnswerAlphabet { get; set; } = StringConstant.AnswerAlphabet;
        public Color AnswerFrameTextColor
        {
            get
            {
                return this.answerFrameTextColor;
            }
            set
            {
                if (this.answerFrameTextColor != value)
                {
                    this.answerFrameTextColor = value;
                    this.OnPropertyChanged(nameof(this.AnswerFrameTextColor));
                }
            }
        }
        public Color AnswerAlphabetTextColor
        {
            get
            {
                return this.answerAlphabetTextColor;
            }
            set
            {
                if (this.answerAlphabetTextColor != value)
                {
                    this.answerAlphabetTextColor = value;
                    this.OnPropertyChanged(nameof(this.AnswerAlphabetTextColor));
                }
            }
        }
        public Color AnswerAlphabetBorderColor
        {
            get
            {
                return this.answerAlphabetBorderColor;
            }
            set
            {
                if (this.answerAlphabetBorderColor != value)
                {
                    this.answerAlphabetBorderColor = value;
                    this.OnPropertyChanged(nameof(this.AnswerAlphabetBorderColor));
                }
            }
        }
        public Color AlphabetFrameBgColor
        {
            get
            {
                return this.alphabetFrameBgColor;
            }
            set
            {
                if (this.alphabetFrameBgColor != value)
                {
                    this.alphabetFrameBgColor = value;
                    this.OnPropertyChanged(nameof(this.AlphabetFrameBgColor));
                }
            }
        }
        #endregion
    }
}
