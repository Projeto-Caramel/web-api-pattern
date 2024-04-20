using Caramel.Pattern.Services.Domain.Entities;
using System.Linq.Expressions;

namespace Caramel.Pattern.Services.Domain.Repositories
{
    public interface IBaseRespository<TEntity, T>
        where TEntity : class, IEntity<T>, new()
        where T : IComparable, IEquatable<T>
    {
        Task<TEntity?> GetSingleAsync(T id);
        Task<IEnumerable<TEntity>> FetchAsync(Expression<Func<TEntity, bool>>? condition = null);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
