using DohrniiFoundation.Helpers;
using Newtonsoft.Json;

namespace DohrniiFoundation.Models.InvestorsProfileModel
{
    /// <summary>
    /// Class to bind the questionnaire list of the investors profile
    /// </summary>
    public class QuestionnaireModel : ObservableObject
    {
        #region Private Properties 
        private bool isAnswerVisible { get; set; } = false;
        private string answer { get; set; }
        private string dropIconImage = StringConstant.dropdown;
        #endregion

        #region Public Properties

        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        public bool IsQuestionSelected { get; set; } = false;
        public string Answer
        {
            get
            {
                return this.answer;
            }
            set
            {
                if (this.answer != value)
                {
                    this.answer = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool IsAnswerVisible
        {
            get
            {
                return isAnswerVisible;
            }
            set
            {
                if (isAnswerVisible != value)
                {
                    isAnswerVisible = value;
                    OnPropertyChanged();
                }
            }
        }
        public string DropIconImage
        {
            get
            {
                return dropIconImage;
            }
            set
            {
                if (dropIconImage != value)
                {
                    dropIconImage = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion
    }
}
