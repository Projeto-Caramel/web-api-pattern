using Caramel.Pattern.Services.Domain.Entities;
using Caramel.Pattern.Services.Domain.Repositories;
using Caramel.Pattern.Services.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Caramel.Pattern.Services.Infra.Repositories
{
    public abstract class BaseRepository<TEntity, T> : IBaseRespository<TEntity, T>
    where TEntity : class, IEntity<T>, new()
    where T : IComparable, IEquatable<T>
    {
        private readonly DataContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected DataContext Context { get { return _context; } }

        public BaseRepository(DataContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<TEntity?> GetSingleAsync(T id)
        {
            return await _dbSet.SingleOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<IEnumerable<TEntity>> FetchAsync(Expression<Func<TEntity, bool>>? condition = null)
        {
            return condition != null
                ? await _dbSet.Where(condition).ToListAsync()
                : await _dbSet.ToListAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
    }

}
