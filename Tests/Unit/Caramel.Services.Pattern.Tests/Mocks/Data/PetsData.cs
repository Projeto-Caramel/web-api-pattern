using Caramel.Pattern.Services.Domain.Entities;

namespace Caramel.Services.Pattern.Tests.Mocks.Data
{
    public class PetsData
    {
        public static Dictionary<string, IEnumerable<Pet>> Data = new Dictionary<string, IEnumerable<Pet>>
        {
            {
                "Basic",
                new List<Pet>
                {
                    PetData.Data["Basic"],
                    PetData.Data["Basic"],
                    PetData.Data["Basic"],
                    PetData.Data["Basic"],
                    PetData.Data["Vaccinated"],
                    PetData.Data["Castrated"],
                    PetData.Data["Available"],
                    PetData.Data["AdoptApp"],
                    PetData.Data["AdoptOng"],
                    PetData.Data["OtherPartner"],
                    PetData.Data["OtherPartner"],
                    PetData.Data["OtherPartner"],
                }
            },
            {
                "Filtered",
                new List<Pet>
                {
                    PetData.Data["OtherPartner"],
                    PetData.Data["OtherPartner"],
                    PetData.Data["OtherPartner2"],
                }
            },
            {
                "Empty",
                new List<Pet>()
            }
        };
    }
}
