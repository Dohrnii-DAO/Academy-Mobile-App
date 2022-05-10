using Newtonsoft.Json;

namespace DohrniiFoundation.Models.InvestorsProfileModel
{
    /// <summary>
    /// This class is used to define the submit answers API request properties
    /// </summary>
    public class SubmitQuestionnaireAnswer
    {
        #region Public Properties
        [JsonProperty("ques_id")]
        public int QuesId { get; set; }
        [JsonProperty("ques_description")]
        public string QuesDescription { get; set; }
        [JsonProperty("selected_ans_id")]
        public int SelectedAnsId { get; set; }
        [JsonProperty("ans_description")]
        public string AnsDescription { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        #endregion
    }
}
