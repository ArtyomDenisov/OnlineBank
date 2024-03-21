using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnlineBank.Models
{
    public class UserLogIn
    {
        [JsonPropertyName("userLogin")]
        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Необходимо заполнить поле")]
        public string UserLogin { get; set; }

        [JsonPropertyName("userPassword")]
        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Необходимо заполнить поле")]
        public string UserPassword { get; set; }
    }
}

