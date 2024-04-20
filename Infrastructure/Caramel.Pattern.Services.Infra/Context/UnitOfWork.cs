using Caramel.Pattern.Services.Domain.Repositories;
using Caramel.Pattern.Services.Domain.Repositories.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Caramel.Pattern.Services.Infra.Context
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        public IPetRepository Pets { get; }
        public IPartnerRepository Partners { get; }

        public UnitOfWork(
            DataContext context,
            IPetRepository pets,
            IPartnerRepository partners)
        {
            _context = context;
            Pets = pets;
            Partners = partners;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
        }
    }
}
