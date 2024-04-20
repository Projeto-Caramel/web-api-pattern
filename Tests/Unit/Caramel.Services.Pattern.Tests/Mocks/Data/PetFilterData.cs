using Caramel.Pattern.Services.Domain.Entities;

namespace Caramel.Services.Pattern.Tests.Mocks.Data
{
    public class PetFilterData
    {
        public static Dictionary<string, PetFilter> Data = new Dictionary<string, PetFilter>
        {
            {
                "Basic",
                new PetFilter
                {
                    Name = "Test",
                    Castrated = FilterCastrated.Castrated,
                    Vaccinated = FilterVaccinated.Vaccinated,
                    Age = 2,
                    Status = FilterPetStatus.Available
                }
            },
            {
                "NotFound",
                new PetFilter
                {
                    Name = "Not Found",
                    Castrated = FilterCastrated.Castrated,
                    Vaccinated = FilterVaccinated.Vaccinated,
                    Age = 10,
                    Status = FilterPetStatus.AdoptONG
                }
            },
            {
                "Error",
                new PetFilter
                {
                    Name = string.Empty,
                    Castrated = FilterCastrated.None,
                    Vaccinated = FilterVaccinated.None,
                    Age = 0,
                    Status = FilterPetStatus.None
                }
            },
            { "Null", null}
        };
    }
}

