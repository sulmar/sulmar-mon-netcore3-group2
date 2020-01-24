using Microsoft.AspNetCore.Mvc;
using Resources.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resources.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonRepository personRepository;

        public PersonsController(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        // api/vehicles/{id}/owner        
        [HttpGet("~api/vehicles/{vehicleId}/owner")]
        public async Task<IActionResult> GetOwner(int vehicleId)
        {
            var person = await personRepository.GetByVehicleAsync(vehicleId);

            return Ok(person);
        }

    }
}
