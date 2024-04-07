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

        [JsonPropertyName("accountLevelId")]
        public int UserUserLevelId { get; set; }

        [JsonPropertyName("accountEnabled")]
        public bool UserEnabled { get; set; }
    }
}
