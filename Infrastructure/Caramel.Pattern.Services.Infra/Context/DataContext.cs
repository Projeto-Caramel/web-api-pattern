using Caramel.Pattern.Services.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Caramel.Pattern.Services.Infra.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> contextOptions) : base(contextOptions)
        { }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<Partner> Partners { get; set; }
    }
}
