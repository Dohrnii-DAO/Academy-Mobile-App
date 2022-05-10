using DohrniiFoundation.Models.APIResponseModels;
using DohrniiFoundation.Models.APIResponseModels.InvestorsProfile;
using Newtonsoft.Json;

namespace DohrniiFoundation.Models.InvestorsProfileResponseModels
{
    /// <summary>
    /// This class is used to define the submit selected answers API response properties
    /// </summary>
    public class SubmitQuestionnaireResponseModel : ResponseModel
    {
        #region Properties
        [JsonProperty("data")]
        public SubmittedAnswersResponse SubmittedAnswersList { get; set; }
        #endregion
    }   
}
