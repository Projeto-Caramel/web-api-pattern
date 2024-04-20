using Caramel.Pattern.Services.Domain.Entities;
using Caramel.Pattern.Services.Domain.Enums;

namespace Caramel.Pattern.Services.Domain.Services
{
    public interface IPetService
    {
        Task<Pet> GetSingleAsync(int id);
        Task<PetStatus> GetPetStatusAsync(int id);
        Task<IEnumerable<Pet>> FetchAsync(int partnerId);
        Task<IEnumerable<Pet>> FetchByFilterAsync(int partnerId, PetFilter filter);
        Task<Pet> AddAsync(Pet entity);
        Pet Update(Pet entity);
        Task<PetStatus> UpdateStatusAsync(int id, PetStatus status);
        Task<bool> DeleteAsync(int id);
    }
}
