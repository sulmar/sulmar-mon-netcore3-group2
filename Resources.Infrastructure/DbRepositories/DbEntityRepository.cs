using Microsoft.EntityFrameworkCore;
using Resources.Domain.Models;
using Resources.Domain.Services;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Threading.Tasks;

namespace Resources.Infrastructure.DbRepositories
{
    public class DbEntityRepository<T> : IEntityRepository<T>
        where T : BaseEntity, new()
    {
        protected readonly ResourcesContext context;
        protected DbSet<T> entities => context.Set<T>();

        public DbEntityRepository(ResourcesContext context)
        {
            this.context = context;
        }
           

        public virtual async Task AddAsync(T entity)
        {
            entities.Add(entity);
            await context.SaveChangesAsync();
        }

        public virtual Task<bool> ExistsAsync(int id)
        {
            return entities.AnyAsync(e => e.Id == id);
        }

        public virtual async Task<IEnumerable<T>> GetAsync()
        {
            return await entities.AsNoTracking().ToListAsync();
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await entities.FindAsync(id);
        }

        public virtual async Task RemoveAsync(int id)
        {
            T entity = new T() { Id = id };
            entities.Remove(entity);
            await context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            entities.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
