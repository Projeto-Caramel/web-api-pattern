using Caramel.Pattern.Services.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.Pattern.Services.Domain.Services
{
    public interface IUserService
    {
        Task<User> GetSingleAsync(int id);
        Task<IEnumerable<User>> FetchAsync();
        Task<User> AddAsync(User entity);
        User Update(User entity);
        Task<bool> DeleteAsync(int id);
    }
}
