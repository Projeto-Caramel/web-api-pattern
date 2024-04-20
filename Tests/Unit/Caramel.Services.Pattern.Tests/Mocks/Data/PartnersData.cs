using Caramel.Pattern.Services.Domain.Entities;

namespace Caramel.Services.Pattern.Tests.Mocks.Data
{
    public class PartnersData
    {
        public static Dictionary<string, IEnumerable<Partner>> Data = new Dictionary<string, IEnumerable<Partner>>
        {
            {
                "Basic",
                new List<Partner>
                {
                    PartnerData.Data["Basic"],
                    PartnerData.Data["Basic"],
                    PartnerData.Data["Basic"],
                    PartnerData.Data["Basic"]
                }
            },
            { "Empty", new List<Partner>() }
        };
    }
}
