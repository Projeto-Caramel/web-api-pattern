using System.Text.Json.Serialization;

namespace Caramel.Pattern.Services.Domain.Entities.Models.Request
{
    public class PartnerRequest
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("email")]
        public string? Email { get; set; }
        [JsonPropertyName("phone")]
        public string? Phone { get; set; }
        [JsonPropertyName("cnpj")]
        public string? Cnpj { get; set; }
        [JsonPropertyName("adoptionRate")]
        public double AdoptionRate { get; set; }
    }
}
