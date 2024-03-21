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

        [JsonPropertyName("userPhone")]
        public string UserPhone { get; set; }

        [JsonPropertyName("userLogin")]
        public string UserLogin { get; set; }

        [JsonPropertyName("userPassword")]
        public string UserPassword { get; set; }

        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        [JsonPropertyName("userSurname")]
        public string UserSurname { get; set; }

        [JsonPropertyName("userPatronymic")]
        public string UserPatronymic { get; set; }
    }
}
