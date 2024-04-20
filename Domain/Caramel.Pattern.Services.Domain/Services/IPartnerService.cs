using Caramel.Pattern.Services.Domain.Entities;

namespace Caramel.Pattern.Services.Domain.Services
{
    public interface IPartnerService
    {
        Task<Partner> GetSingleAsync(int id);
        Task<IEnumerable<Partner>> FetchAsync();
        Task<Partner> AddAsync(Partner entity);
        Partner Update(Partner entity);
        Task<bool> DeleteAsync(int id);
    }
}
