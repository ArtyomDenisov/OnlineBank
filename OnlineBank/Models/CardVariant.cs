using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OnlineBank.Models
{
    public class CardVariant
    {
        [JsonPropertyName("cardVariantId")]
        public long CardVariantId { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("text")]
        public string? Text { get; set; }

        [JsonPropertyName("percentage")]
        public int Percentage { get; set; }

        [JsonPropertyName("cardTypeId")]
        public long CardTypeId { get; set; }

        [JsonPropertyName("userLevelId")]
        public long UserLevelId { get; set; }

        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }
    }
}
