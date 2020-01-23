using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Resources.Domain.Models;
using Resources.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resources.Infrastructure.DbRepositories
{
    public class DbVehicleRepository : IVehicleRepository
    {
        private readonly ResourcesContext context;
        private readonly ILogger<DbVehicleRepository> logger;

        public DbVehicleRepository(
            ResourcesContext context, 
            ILogger<DbVehicleRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task AddAsync(Vehicle entity)
        {
            context.ChangeTracker.TrackGraph(entity, e =>
            {
                if (e.Entry.IsKeySet)
                {
                    e.Entry.State = EntityState.Unchanged;
                }
                else
                {
                    e.Entry.State = EntityState.Added;
                }
            });

            //  context.Entry(entity.Owner).State = EntityState.Unchanged;

            LogState(entity);
            await context.Vehicles.AddAsync(entity);

            var entries = context.ChangeTracker.Entries().ToList();

            LogState(entity);
            await context.SaveChangesAsync();
            LogState(entity);
        }

        private void LogState(Vehicle entity)
        {
            logger.LogInformation(context.Entry(entity).State.ToString());
        }

        public Task<bool> ExistsAsync(int id)
        {
            return Task.FromResult(context.Vehicles.Any(v=>v.Id == id));
        }

        public async Task<IEnumerable<Vehicle>> GetAsync(VehicleSearchCriteria criteria)
        {
            IQueryable<Vehicle> query = context.Vehicles.AsQueryable();

            if (criteria.From.HasValue)
            {
                query = query.Where(v => v.ProductionYear >= criteria.From);
            }

            if (criteria.To.HasValue)
            {
                query = query.Where(v => v.ProductionYear <= criteria.To);
            }

            if (!string.IsNullOrEmpty(criteria.Name))
            {
                query = query.Where(v => v.Name.Contains(criteria.Name));
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public Task<Vehicle> GetAsync(string vin)
        {
            return context.Vehicles
                .AsNoTracking()
                .IgnoreQueryFilters()
                .SingleOrDefaultAsync(v => v.VIN == vin);
        }

        public async Task<IEnumerable<Vehicle>> GetAsync()
        {
            return await context.Vehicles.AsNoTracking().ToListAsync();
        }

        public async Task<Vehicle> GetAsync(int id)
        {
            return await context.Vehicles.FindAsync(id);
        }

        public async Task RemoveAsync(int id)
        {
            // Vehicle entity = await GetAsync(id);

            Vehicle entity = new Vehicle { Id = id };

            LogState(entity);
            context.Vehicles.Remove(entity);


            LogState(entity);
            await context.SaveChangesAsync();
            LogState(entity);
        }

        public async Task UpdateAsync(Vehicle entity)
        {
            LogState(entity);

            context.Vehicles.Update(entity);
            LogState(entity);
            await context.SaveChangesAsync();
            LogState(entity);
        }

        public async Task UpdateModelAsync(int id, string model)
        {
            Vehicle vehicle = new Vehicle { Id = id, Model = model };
            context.Entry(vehicle).Property(p => p.Model).IsModified = true;
            await context.SaveChangesAsync();
        }

        //public async Task UpdateModelAsync(int id, string propertyName, string value)
        //{
        //    Vehicle vehicle = new Vehicle { Id = id };
        //    vehicle["Model"] = value;
        //    context.Entry(vehicle).Property(propertyName).IsModified = true;
        //    await context.SaveChangesAsync();
        //}

    }
}
