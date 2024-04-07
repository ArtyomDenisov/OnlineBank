using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OnlineBank.Models
{
    public class User
    {
        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("accountPhone")]
        [Display(Name = "Телефон")]
        [Required(ErrorMessage = "Необходимо заполнить поле")]
        [Phone(ErrorMessage = "Некорректный номер телефона")]
        public string UserPhone { get; set; }

        [JsonPropertyName("accountLogin")]
        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Необходимо заполнить поле")]
        [StringLength(32, MinimumLength = 6, ErrorMessage = "Поле должно содержать от 6 до 32 символов")]
        [RegularExpression(@"^[A-Za-z0-9_-]{6,}$", ErrorMessage = "Некорректный логин")]
        public string UserLogin { get; set; }

        [JsonPropertyName("accountPassword")]
        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Необходимо заполнить поле")]
        [StringLength(28, MinimumLength = 8, ErrorMessage = "Поле должно содержать от 8 до 28 символов")]
        public string UserPassword { get; set; }

        [JsonPropertyName("accountName")]
        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Необходимо заполнить поле")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Поле должно содержать от 3 до 20 символов")]
        public string UserName { get; set; }

        [JsonPropertyName("accountSurname")]
        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Необходимо заполнить поле")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Поле должно содержать от 3 до 20 символов")]
        public string UserSurname { get; set; }

        [JsonPropertyName("accountPatronymic")]
        [Display(Name = "Отчество")]
        [Required(ErrorMessage = "Необходимо заполнить поле")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Поле должно содержать от 3 до 20 символов")]
        public string UserPatronymic { get; set; }

        [JsonPropertyName("userLevelId")]
        public int UserUserLevelId { get; set; }

        [JsonPropertyName("enabled")]
        public bool UserEnabled { get; set; }

        [JsonPropertyName("cardList")]
        public List<Card> cardList;

    }
}
