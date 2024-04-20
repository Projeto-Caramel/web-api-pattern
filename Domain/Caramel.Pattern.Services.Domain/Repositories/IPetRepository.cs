using Caramel.Pattern.Services.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.Pattern.Services.Domain.Repositories
{
    public interface IPetRepository : IBaseRespository<Pet, int>
    {
    }
}
