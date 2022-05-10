using DohrniiFoundation.Models.APIResponseModels;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace DohrniiFoundation.Models.InvestorsProfileResponseModels
{
    public class AnswersResponseModel : APIResponseModel
    {
        #region Properties
        [JsonProperty("data")]
        public ObservableCollection<AnswersModel> Answers { get; set; }
        #endregion
    }
}
