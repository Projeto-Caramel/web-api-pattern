using Caramel.Pattern.Services.Domain.Entities;

namespace Caramel.Services.Pattern.Tests.Mocks.Data
{
    public class PartnerData
    {
        public static Dictionary<string, Partner> Data = new Dictionary<string, Partner>
        {
            {
                "Basic", new Partner()
                {
                    Id = 1,
                    Name = "Test",
                    Description = "Test",
                    Phone = "Test",
                    Email = "Test",
                    Cnpj = "Test",
                    AdoptionRate = 100
                }
            },
            {
                "BasicUpdate", new Partner()
                {
                    Id = 1,
                    Name = "Test",
                    Description = "Test",
                    Phone = "Test",
                    Email = "Test",
                    Cnpj = "Test",
                    AdoptionRate = 100
                }
            },
            {
                "BasicUpdateException", new Partner()
                {
                    Id = 1,
                    Name = "Test",
                    Description = "Test",
                    Phone = "Test",
                    Email = "Test",
                    Cnpj = "Test",
                    AdoptionRate = 100
                }
            },
            { "Empty", new Partner() { Id = 1 } },
            { "WithoutId", new Partner() },
            { "Null", null },
        };
    }
}
