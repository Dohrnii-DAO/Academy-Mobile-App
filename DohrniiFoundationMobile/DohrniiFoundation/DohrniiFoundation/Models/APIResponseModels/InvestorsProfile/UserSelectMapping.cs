using Newtonsoft.Json;

namespace DohrniiFoundation.Models.APIResponseModels.InvestorsProfile
{
    /// <summary>
    /// This class is used to define the submit selected answers API response properties
    /// </summary>
    public class UserSelectMapping
    {
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
    }
}
