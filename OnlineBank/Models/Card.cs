using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OnlineBank.Models
{
    public class Card
    {
        [JsonPropertyName("substanceId")]
        [Display(Name = "SubstanceId")]
        public int SubstanceId { get; set; }

        [JsonPropertyName("cardVariantId")]
        [Display(Name = "cardVariantId")]
        public int CardVariantId { get; set; }

        [JsonPropertyName("accountId")]
        [Display(Name = "accountId")]
        public int AccountId { get; set; }

        [Display(Name = "Баланс")]
        [JsonPropertyName("rublesCount")]
        public int RublesCount { get; set; }

        [JsonPropertyName("imagePath")]
        [Display(Name = "imagePath")]
        public string ImagePath { get; set; }

        [JsonPropertyName("enabled")]
        [Display(Name = "enabled")]
        public bool Enabled { get; set; }
    }
}
