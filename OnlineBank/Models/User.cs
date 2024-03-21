using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OnlineBank.Models
{
    public class User
    {
        [JsonPropertyName("accountId")]
        public int UserId { get; set; }

        [JsonPropertyName("accountPhone")]
        [Display(Name = "Телефон")]
        [Required(ErrorMessage = "Необходимо заполнить поле")]
        public string UserPhone { get; set; }

        [JsonPropertyName("accountLogin")]
        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Необходимо заполнить поле")]
        public string UserLogin { get; set; }

        [JsonPropertyName("accountPassword")]
        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Необходимо заполнить поле")]
        public string UserPassword { get; set; }

        [JsonPropertyName("accountName")]
        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Необходимо заполнить поле")]   
        public string UserName { get; set; }

        [JsonPropertyName("accountSurname")]
        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Необходимо заполнить поле")]
        public string UserSurname { get; set; }

        [JsonPropertyName("accountPatronymic")]
        [Display(Name = "Отчество")]
        [Required(ErrorMessage = "Необходимо заполнить поле")]
        public string UserPatronymic { get; set; }

        [JsonPropertyName("accountLevelId")]
        public int UserUserLevelId { get; set; }

        [JsonPropertyName("accountEnabled")]
        public bool UserEnabled { get; set; }

            //[Display(Name = "Возраст")]
            //[Required(ErrorMessage = "Необходимо заполнить поле")]
            //public int Age { get; set; }

            //[Display(Name = "Электронная почта")]
            //[Required(ErrorMessage = "Необходимо заполнить поле")]
            //public string Email { get; set; }

        }
}
