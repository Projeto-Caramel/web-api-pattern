using System.ComponentModel;

namespace Caramel.Pattern.Services.Domain.Enums
{
    public enum PetStatus
    {
        [Description("Disponível")]
        Available = 1,
        [Description("Indisponível")]
        Unavailable = 2,
        [Description("Adotado pelos processos das ONGs")]
        AdoptOng = 3,
        [Description("Adotado pelo APP")]
        AdoptApp = 4
    }
}
