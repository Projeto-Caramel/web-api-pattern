namespace Caramel.Pattern.Services.Domain.Entities
{
    public class Partner : IEntity<int> 
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Cnpj { get; set; }
        public Double AdoptionRate { get; set; }
    }
}
