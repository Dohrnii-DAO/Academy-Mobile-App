using DohrniiFoundation.Helpers;
using Newtonsoft.Json;
using System;
using Xamarin.Forms;

namespace DohrniiFoundation.Models.InvestorsProfileModel
{
    /// <summary>
    /// Model class to bind the answers of the questionnaire screens and functionality
    /// </summary>
    public class AnswersModel : ObservableObject
    {
        #region Private Properties
        private bool isAnswerSelected = false;
        private double scaleAnswerSelected;
        private string answerSelectedImage = StringConstant.Unselected;
        private Color selectedAnswerBackgroundColor = (Color)Application.Current.Resources["WhiteText"];
        private FontAttributes fontAttributes { get; set; } = FontAttributes.None;
        #endregion

        #region Public Properties
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("question_id")]
        public int QuestionId { get; set; }

        [JsonProperty("strategies_id")]
        public object strategiesId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("sequence")]
        public int Sequence { get; set; }

        [JsonProperty("created_at")]
        public DateTime? CreatedDate { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedDate { get; set; }

        [JsonProperty("deleted_at")]
        public object DeletedDate { get; set; }

        public string QuestionType { get; set; }

        public bool IsAnswerSelected
        {
            get
            {
                return isAnswerSelected;
            }
            set
            {
                if (isAnswerSelected != value)
                {
                    isAnswerSelected = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string AnswerSelectedImage
        {
            get
            {
                return answerSelectedImage;
            }
            set
            {
                if (answerSelectedImage != value)
                {
                    answerSelectedImage = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public double ScaleAnswerSelected
        {
            get
            {
                return scaleAnswerSelected;
            }
            set
            {
                if (scaleAnswerSelected != value)
                {
                    scaleAnswerSelected = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Color SelectedAnswerBackgroundColor
        {
            get
            {
                return selectedAnswerBackgroundColor;
            }
            set
            {
                if (selectedAnswerBackgroundColor != value)
                {
                    selectedAnswerBackgroundColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public FontAttributes FontAttributes
        {
            get
            {
                return fontAttributes;
            }
            set
            {
                if (fontAttributes != value)
                {
                    fontAttributes = value;
                    this.OnPropertyChanged();
                }
            }
        }
        #endregion      
    }
}
