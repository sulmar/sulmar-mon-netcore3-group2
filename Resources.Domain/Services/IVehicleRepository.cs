using Resources.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Resources.Domain.Services
{
    public interface IVehicleRepository : IEntityRepository<Vehicle>
    {
        Task<IEnumerable<Vehicle>> GetAsync(VehicleSearchCriteria criteria);
        Task<Vehicle> GetAsync(string vin);
    }

}
