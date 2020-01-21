using Bogus;
using Microsoft.Extensions.Options;
using Resources.Domain.Models;
using Resources.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Resources.Infrastructure
{
    public class FakeVehicleRepositoryOptions
    {
        public int Count { get; set; }
    }


    // dotnet add package Bogus

    public class FakeVehicleRepository : IVehicleRepository
    {
        private readonly ICollection<Vehicle> vehicles;
        private readonly FakeVehicleRepositoryOptions options;

        // dotnet add package Microsoft.Extensions.Options
        public FakeVehicleRepository(Faker<Vehicle> faker, 
            IOptionsSnapshot<FakeVehicleRepositoryOptions> options)
        {
            this.options = options.Value;

            vehicles = faker.Generate(this.options.Count);
        }

        public Task AddAsync(Vehicle entity)
        {
            vehicles.Add(entity);

            return Task.CompletedTask;
        }

        public Task<bool> ExistsAsync(int id)
        {
           return Task.FromResult(vehicles.Any(c => c.Id == id));
        }

        public Task<IEnumerable<Vehicle>> GetAsync(VehicleSearchCriteria criteria)
        {
            return Task.FromResult(Get(criteria));
        }

        private IEnumerable<Vehicle> Get(VehicleSearchCriteria criteria)
        {
            var query = vehicles.AsQueryable();

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

            return query.ToList();
        }

        public Task<IEnumerable<Vehicle>> GetAsync()
        {
            return Task.FromResult(vehicles.AsEnumerable());
        }

        public Task<Vehicle> GetAsync(int id)
        {
            return Task.FromResult(vehicles.SingleOrDefault(v => v.Id == id));
        }

        public Task RemoveAsync(int id)
        {
            Vehicle vehicle = GetAsync(id).Result;
            vehicles.Remove(vehicle);

            return Task.CompletedTask;
        }

        public Task UpdateAsync(Vehicle entity)
        {
            throw new NotImplementedException();
        }

        public Task<Vehicle> GetAsync(string vin)
        {
            return Task.FromResult(Get(vin));
        }

        private Vehicle Get(string vin)
        {
            return vehicles.SingleOrDefault(v => v.VIN == vin);
        }
    }
}
