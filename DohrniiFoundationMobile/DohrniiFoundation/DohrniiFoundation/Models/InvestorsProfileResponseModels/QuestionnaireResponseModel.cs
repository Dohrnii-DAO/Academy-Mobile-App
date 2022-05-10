using DohrniiFoundation.Models.APIResponseModels;
using DohrniiFoundation.Models.InvestorsProfileModel;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace DohrniiFoundation.Models.InvestorsProfileResponseModels
{
   public class QuestionnaireResponseModel : APIResponseModel
    {
        #region Properties
        [JsonProperty("data")]
        public ObservableCollection<QuestionnaireModel> Questions { get; set; }
        #endregion
    }
}
