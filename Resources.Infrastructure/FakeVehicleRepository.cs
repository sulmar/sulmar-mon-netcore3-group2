using Bogus;
using Resources.Domain.Models;
using Resources.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resources.Infrastructure
{
    // dotnet add package Bogus

    public class FakeVehicleRepository : IVehicleRepository
    {
        private readonly ICollection<Vehicle> vehicles;

        public FakeVehicleRepository(Faker<Vehicle> faker)
        {
            vehicles = faker.Generate(50);
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
    }
}
