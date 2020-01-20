using Resources.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Resources.Domain.Services
{
    public interface IEntityRepository<TKey, TEntity>
      where TEntity : BaseEntity
    {
        Task<bool> ExistsAsync(TKey id);
        Task<IEnumerable<TEntity>> GetAsync();
        Task<TEntity> GetAsync(TKey id);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task RemoveAsync(TKey id);
    }

    public interface IEntityRepository<TEntity> : IEntityRepository<int, TEntity>
        where TEntity : BaseEntity
    {
    }

}
