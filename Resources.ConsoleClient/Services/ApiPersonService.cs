using Resources.Domain.Models;
using Resources.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Resources.ConsoleClient.Services
{
    public class ApiPersonService : IPersonRepository
    {
        public Task AddAsync(Person entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Person>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Person> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Person> GetByVehicleAsync(int vehicleId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public bool TryAuthenticate(string username, string password, out Person person)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Person entity)
        {
            throw new NotImplementedException();
        }
    }
}
