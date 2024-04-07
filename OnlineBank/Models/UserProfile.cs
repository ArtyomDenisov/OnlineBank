using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OnlineBank.Models
{
    public class UserProfile
    {
        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("accountPhone")]
        public string UserPhone { get; set; }

        [JsonPropertyName("accountLogin")]
        public string UserLogin { get; set; }

        [JsonPropertyName("accountPassword")]
        public string UserPassword { get; set; }

        [JsonPropertyName("accountName")]
        public string UserName { get; set; }

        [JsonPropertyName("accountSurname")]
        public string UserSurname { get; set; }

        [JsonPropertyName("accountPatronymic")]
        public string UserPatronymic { get; set; }
    }
}
