using Caramel.Pattern.Services.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Caramel.Pattern.Services.Domain.Entities
{
    public class Pet : IEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("partnerId")]
        public int PartnerId { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("castrated")]
        public bool Castrated { get; set; }
        [JsonPropertyName("vaccinated")]
        public bool Vaccinated { get; set; }
        [JsonPropertyName("age")]
        public int Age { get; set; }
        [JsonPropertyName("status")]
        public PetStatus Status { get; set; }
    }
}
