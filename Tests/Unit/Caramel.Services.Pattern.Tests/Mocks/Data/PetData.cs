using Caramel.Pattern.Services.Domain.Entities;
using Caramel.Pattern.Services.Domain.Enums;

namespace Caramel.Services.Pattern.Tests.Mocks.Data
{
    public class PetData
    {
        public static Dictionary<string, Pet> Data = new Dictionary<string, Pet>
        {
            {
                "Basic", new Pet()
                {
                    Id = 1,
                    PartnerId = 1,
                    Name = "Test",
                    Age = 2,
                    Description = "Test",
                    Vaccinated = false,
                    Castrated = false,
                    Status = PetStatus.Unavailable
                }
            },
            {
                "BasicUpdateSuccess", new Pet()
                {
                    Id = 1,
                    PartnerId = 1,
                    Name = "Test",
                    Age = 2,
                    Description = "Test",
                    Vaccinated = false,
                    Castrated = false,
                    Status = PetStatus.Unavailable
                }
            },
            {
                "BasicUpdateException", new Pet()
                {
                    Id = 1,
                    PartnerId = 1,
                    Name = "Test",
                    Age = 2,
                    Description = "Test",
                    Vaccinated = false,
                    Castrated = false,
                    Status = PetStatus.Unavailable
                }
            },
            {
                "BasicUpdateStatus", new Pet()
                {
                    Id = 1,
                    PartnerId = 1,
                    Name = "Test",
                    Age = 2,
                    Description = "Test",
                    Vaccinated = false,
                    Castrated = false,
                    Status = PetStatus.Unavailable
                }
            },
            {
                "BasicUpdateStatusException", new Pet()
                {
                    Id = 1,
                    PartnerId = 1,
                    Name = "Test",
                    Age = 2,
                    Description = "Test",
                    Vaccinated = false,
                    Castrated = false,
                    Status = PetStatus.Unavailable
                }
            },
            {
                "Vaccinated", new Pet()
                {
                    Id = 2,
                    PartnerId = 1,
                    Name = "Test",
                    Age = 2,
                    Description = "Test",
                    Vaccinated = true,
                    Castrated = false,
                    Status = PetStatus.Unavailable
                }
            },
            {
                "Castrated", new Pet()
                {
                    Id = 2,
                    PartnerId = 1,
                    Name = "Test",
                    Age = 2,
                    Description = "Test",
                    Vaccinated = false,
                    Castrated = true,
                    Status = PetStatus.Unavailable
                }
            },
            {
                "Available", new Pet()
                {
                    Id = 1,
                    PartnerId = 1,
                    Name = "Test",
                    Age = 2,
                    Description = "Test",
                    Vaccinated = false,
                    Castrated = false,
                    Status = PetStatus.Available
                }
            },
            {
                "AdoptApp", new Pet()
                {
                    Id = 1,
                    PartnerId = 1,
                    Name = "Test",
                    Age = 2,
                    Description = "Test",
                    Vaccinated = false,
                    Castrated = false,
                    Status = PetStatus.AdoptApp
                }
            },
            {
                "AdoptOng", new Pet()
                {
                    Id = 1,
                    PartnerId = 1,
                    Name = "Test",
                    Age = 2,
                    Description = "Test",
                    Vaccinated = false,
                    Castrated = false,
                    Status = PetStatus.AdoptOng
                }
            },
            {
                "OtherPartner", new Pet()
                {
                    Id = 1,
                    PartnerId = 2,
                    Name = "Test",
                    Age = 2,
                    Description = "Test",
                    Vaccinated = true,
                    Castrated = true,
                    Status = PetStatus.Available
                }
            },
            {
                "OtherPartner2", new Pet()
                {
                    Id = 1,
                    PartnerId = 2,
                    Name = "Test",
                    Age = 2,
                    Description = "Test",
                    Vaccinated = false,
                    Castrated = false,
                    Status = PetStatus.Available
                }
            },
            { "Empty", new Pet() { Id = 1 } },
            { "WithoutId", new Pet() },
            { "Null", null },
        };
    }
}
