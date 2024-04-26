using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OnlineBank.Models
{
    public class Sending
    {
        [JsonPropertyName("sendingId")]
        public long SendingId { get; set; }

        [JsonPropertyName("operationDateTime")]
        public DateTime OperationDateTime { get; set; }

        [JsonPropertyName("substanceId")]
        [Required(ErrorMessage = "Необходимо заполнить поле")]
        [Range(1, long.MaxValue, ErrorMessage = "Некорректный идентификатор карты")]
        public long? SubstanceId { get; set; } 

        [JsonPropertyName("substanceSenderId")]
        public long? SubstanceSenderId { get; set; }

        [JsonPropertyName("substanceRecipientId")]
        public long SubstanceRecipientId { get; set; }

        [JsonPropertyName("operationTypeId")]
        public long OperationTypeId { get; set; }

        [JsonPropertyName("rublesCount")]
        [Required(ErrorMessage = "Необходимо заполнить поле")]
        [Range(1, int.MaxValue, ErrorMessage = "Некорректная сумма")]
        public int? RublesCount { get; set; }

        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        public string PersonSurname; // Фамилия. И

        public string AvatarLetter; // Ф

        //[Required(ErrorMessage = "Необходимо заполнить поле")]
        //[Phone(ErrorMessage = "Некорректный номер телефона")]
        //public string Phone { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить поле")]
        [RegularExpression("[0-9]{4} [0-9]{4} [0-9]{4} [0-9]{4}", ErrorMessage = "Неверный формат номера номера карты")]
        public string CardNumber { get; set; }
    }
}
