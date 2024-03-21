using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OnlineBank.Models
{
    public class UserStatus
    {
        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("userUserLevelId")]
        public int UserUserLevelId { get; set; }

        [JsonPropertyName("userEnabled")]
        public bool UserEnabled { get; set; }
    }
}
