using DohrniiFoundation.Models.APIResponseModels;
using DohrniiFoundation.Models.InvestorsProfileModel;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace DohrniiFoundation.Models.InvestorsProfileResponseModels
{
    /// <summary>
    /// This class is used to define the answers API response properties
    /// </summary>
    public class AnswersResponseModel : APIResponseModel
    {
        #region Properties
        [JsonProperty("data")]
        public ObservableCollection<AnswersModel> Answers { get; set; }
        #endregion
    }
}
