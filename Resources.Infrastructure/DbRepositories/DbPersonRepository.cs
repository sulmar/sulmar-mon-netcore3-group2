using Microsoft.EntityFrameworkCore;
using Resources.Domain.Models;
using Resources.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace Resources.Infrastructure.DbRepositories
{

    public class DbPersonRepository : DbEntityRepository<Person>, IPersonRepository
    {
        public DbPersonRepository(ResourcesContext context) : base(context)
        {
        }

        public async Task<Person> GetByVehicleAsync(int vehicleId)
        {
            var vehicle = await context.Vehicles
                .Include(v=>v.Owner)
                .SingleAsync(v => v.Id == vehicleId);

            return vehicle.Owner;
        }

        public bool TryAuthenticate(string username, string password, out Person person)
        {
            person = entities
                .SingleOrDefault(e => e.UserName == username && e.HashPassword == password);

            return person != null;
        }

     
    }
}
