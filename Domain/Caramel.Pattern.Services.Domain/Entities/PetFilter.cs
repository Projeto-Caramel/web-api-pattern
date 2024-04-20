using Caramel.Pattern.Services.Domain.Enums;
using System.ComponentModel;

namespace Caramel.Pattern.Services.Domain.Entities
{
    public class PetFilter
    {
        public string? Name { get; set; }
        public FilterCastrated Castrated { get; set; }
        public FilterVaccinated Vaccinated { get; set; }
        public int Age { get; set; }
        public FilterPetStatus Status { get; set; }
    }

    public enum FilterCastrated
    {
        [Description("Nenhum")]
        None = 0,
        [Description("Castrado")]
        Castrated = 1,
        [Description("Não Castrado")]
        NotCastrated = 2
    }

    public enum FilterVaccinated
    {
        [Description("Nenhum")]
        None = 0,
        [Description("Vacinado")]
        Vaccinated = 1,
        [Description("Não Vacinado")]
        NotVaccinated = 2
    }

    public enum FilterPetStatus
    {
        [Description("Nenhum")]
        None = 0,
        [Description("Disponível")]
        Available = 1,
        [Description("Indisponível")]
        Unavailable = 2,
        [Description("Adotado pelos processos das ONGs")]
        AdoptONG = 3,
        [Description("Adotado pelo APP")]
        AdoptAPP = 4
    }
}
