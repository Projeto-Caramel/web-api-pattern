namespace Caramel.Pattern.Services.Domain.Entities.Models.Request
{
    public class GetPetsFilteredRequest
    {
        public Pagination Pagination { get; set; }
        public PetFilter PetFilter { get; set; }
    }
}
