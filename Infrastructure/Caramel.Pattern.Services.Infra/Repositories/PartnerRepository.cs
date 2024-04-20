using Caramel.Pattern.Services.Domain.Entities;
using Caramel.Pattern.Services.Domain.Repositories;
using Caramel.Pattern.Services.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Caramel.Pattern.Services.Infra.Repositories
{
    public class PartnerRepository(DataContext context) : BaseRepository<Partner, int>(context), IPartnerRepository
    {
    }
}
