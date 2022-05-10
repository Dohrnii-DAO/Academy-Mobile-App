using Newtonsoft.Json;
using System.Collections.Generic;

namespace DohrniiFoundation.Models.APIResponseModels.User
{
    /// <summary>
    /// This model is used to get the strategy assigned to the user
    /// </summary>
    public class StrategyStatusResponseModel : APIResponseModel
    {
        [JsonProperty("data")]
        public List<UserStatusDetails> UserStatusDetails { get; set; }
    }        
}
