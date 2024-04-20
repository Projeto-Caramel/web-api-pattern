using Caramel.Pattern.Services.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.Pattern.Services.Domain.Repositories.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IPetRepository Pets { get; }
        IPartnerRepository Partners { get; }
        int Save();
    }
}
