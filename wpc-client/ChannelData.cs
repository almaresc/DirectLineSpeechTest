using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpc_client
{
    public class RequestDetails
    {
        [JsonProperty("interactionId")]
        public string InteractionId { get; set; }
    }

    public class ConvAiData
    {
        [JsonProperty("requestInfo")]
        public RequestDetails RequestInfo { get; set; }
    }

    public class SpeechChannelData
    {
        [JsonProperty("conversationalAiData")]
        public ConvAiData ConversationalAiData { get; set; }
    }
}
