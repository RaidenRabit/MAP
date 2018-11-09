using Newtonsoft.Json;

namespace MAPwClient.Model
{
    public class Token
    {
        public string access_token { get; set; }
        [JsonProperty(".issued")]
        public string issued { get; set; }
        [JsonProperty(".expires")]
        public string expires { get; set; }
    }
}
