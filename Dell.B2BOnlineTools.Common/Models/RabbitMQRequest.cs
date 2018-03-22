
using Newtonsoft.Json;

namespace Dell.B2BOnlineTools.Common.Models
{
    public sealed class RabbitMQRequest
    {
        public RabbitMQRequest()
        {
            Payload_Encoding = "string";
            Properties = new Properties()
            {
                Delivery_Mode = 2,
                ContentType = "application/json"
            };
        }
        [JsonProperty(PropertyName = "properties")]
        public Properties Properties { get; private set; }
        [JsonProperty(PropertyName = "routing_key")]
        public string Routing_Key { get; private set; }
        [JsonProperty(PropertyName = "payload_encoding")]
        public string Payload_Encoding { get; private set; }
        [JsonProperty(PropertyName = "payload")]
        public string Payload { get; private set; }

        public void AddQMsg(string routingKey, QMsg.QueueMessage message)
        {
            Routing_Key = routingKey;
            Payload = JsonConvert.SerializeObject(message);
        }
    }

    public class Properties
    {
        [JsonProperty(PropertyName = "delivery_mode")]
        public int Delivery_Mode { get; set; }
        [JsonProperty(PropertyName = "content-type")]
        public string ContentType { get; set; }
    }

}

