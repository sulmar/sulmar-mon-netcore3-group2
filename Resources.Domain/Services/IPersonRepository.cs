using Resources.Domain.Models;
using System.Threading.Tasks;

namespace Resources.Domain.Services
{
    public interface IPersonRepository : IEntityRepository<Person>
    {
        Task<Person> GetByVehicleAsync(int vehicleId);
        bool TryAuthenticate(string username, string password, out Person person);
    }

}
