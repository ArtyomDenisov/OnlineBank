using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OnlineBank.Models
{
    public class Sending
    {
        [JsonPropertyName("sendingId")]
        public long SendingId { get; set; }

        [JsonPropertyName("operationDateTime")]
        public DateTime OperationDateTime { get; set; }

        [JsonPropertyName("substanceId")]
        public long SubstanceId { get; set; }

        [JsonPropertyName("substanceSenderId")]
        public long SubstanceSenderId { get; set; }

        [JsonPropertyName("substanceRecipientId")]
        public long SubstanceRecipientId { get; set; }

        [JsonPropertyName("operationTypeId")]
        public long OperationTypeId { get; set; }

        [JsonPropertyName("rublesCount")]
        public int RublesCount { get; set; }

        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }
    }
}
