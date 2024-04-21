using Caramel.Pattern.Services.Domain.Enums;

namespace Caramel.Pattern.Services.Domain.Entities.Models.Request
{
    public class PetRequest
    {
        public int PartnerId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool Castrated { get; set; }
        public bool Vaccinated { get; set; }
        public int Age { get; set; }
        public PetStatus Status { get; set; }
    }
}
