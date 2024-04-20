using Caramel.Pattern.Services.Domain.Entities;
using Caramel.Pattern.Services.Domain.Repositories;
using Caramel.Pattern.Services.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Caramel.Pattern.Services.Infra.Repositories
{
    public class PetRepository(DataContext context) : BaseRepository<Pet, int>(context), IPetRepository
    {
    }
}
