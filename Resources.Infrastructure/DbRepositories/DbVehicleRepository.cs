using Microsoft.EntityFrameworkCore;
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

        public DbVehicleRepository(ResourcesContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Vehicle entity)
        {
            await context.Vehicles.AddAsync(entity);
            await context.SaveChangesAsync();
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

            return await query.ToListAsync();
        }

        public Task<Vehicle> GetAsync(string vin)
        {
            return context.Vehicles.SingleOrDefaultAsync(v => v.VIN == vin);
        }

        public async Task<IEnumerable<Vehicle>> GetAsync()
        {
            return await context.Vehicles.ToListAsync();
        }

        public async Task<Vehicle> GetAsync(int id)
        {
            return await context.Vehicles.FindAsync(id);
        }

        public async Task RemoveAsync(int id)
        {
            Vehicle vehicle = await GetAsync(id);
            context.Vehicles.Remove(vehicle);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Vehicle entity)
        {
            context.Vehicles.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
