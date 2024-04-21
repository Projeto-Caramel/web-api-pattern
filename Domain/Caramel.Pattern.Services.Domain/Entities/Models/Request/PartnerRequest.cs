namespace Caramel.Pattern.Services.Domain.Entities.Models.Request
{
    public class PartnerRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Cnpj { get; set; }
        public double AdoptionRate { get; set; }
    }
}
