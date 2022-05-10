using DohrniiFoundation.Models.APIResponseModels;
using DohrniiFoundation.Models.InvestorsProfileModel;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace DohrniiFoundation.Models.InvestorsProfileRequestModels
{
    /// <summary>
    /// This class is used to define the submit selected answers API request properties
    /// </summary>
    public class SubmitQuestionnaireRequestModel : APIResponseModel
    {
        #region Properties
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        [JsonProperty("data")]
        public ObservableCollection<SubmitQuestionnaireAnswer> Data { get; set; }
        #endregion
    }    
}
