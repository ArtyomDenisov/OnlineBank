﻿using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OnlineBank.Models
{
    public class Card
    {
        [JsonPropertyName("cardId")]
        [Display(Name = "cardId")]
        public int SubstanceId { get; set; }

        [JsonPropertyName("cardVariantId")]
        [Display(Name = "cardVariantId")]
        public int CardVariantId { get; set; }

        [JsonPropertyName("userId")]
        [Display(Name = "userId")]
        public int AccountId { get; set; }

        
        [JsonPropertyName("rublesCount")]
        [Display(Name = "Баланс")]
        public int RublesCount { get; set; }

        [JsonPropertyName("imagePath")]
        [Display(Name = "imagePath")]
        public string ImagePath { get; set; }

        [JsonPropertyName("enabled")]
        [Display(Name = "enabled")]
        public bool Enabled { get; set; }

        [JsonPropertyName("cardNumber")]
        [Display(Name = "cardNumber")]
        public string CardNumber { get; set; }

        public IFormFile File { get; set; }

        public Card(int SubstanceId, int CardVariantId, int AccountId, int RublesCount, string ImagePath, bool Enabled, string CardNumber)
        {
            this.SubstanceId = SubstanceId;
            this.CardVariantId = CardVariantId;
            this.AccountId = AccountId;
            this.RublesCount = RublesCount;
            this.ImagePath = ImagePath;
            this.Enabled = Enabled;
            this.CardNumber = CardNumber;
        }

        public Card() { }
    }
}
