using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OnlineBank.Models
{
    public class UserLevel
    {
        [JsonPropertyName("userLevelId")]
        public long AccountLevelId { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }
    }
}
