using Microsoft.AspNetCore.Mvc;
using Resources.Domain.Models;
using Resources.Domain.Services;
using Resources.Infrastructure;
using Resources.Infrastructure.Fakers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resources.API.Controllers
{
    // api/vehicles

    [Route("api/[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleRepository vehicleRepository;

        public VehiclesController(IVehicleRepository vehicleRepository)
        {
            this.vehicleRepository = vehicleRepository;
        }

        // GET api/vehicles
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var vehicles = await vehicleRepository.GetAsync();

            return Ok(vehicles);
        }

        // GET api/vehicles/10
        [HttpGet("{id}")]
        // MVC 
        // [HttpGet]
        // [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var vehicle = await vehicleRepository.GetAsync(id);

            if (vehicle == null)
                return NotFound();

            return Ok(vehicle);
        }

        // POST api/vehicles
        [HttpPost]
        public async Task<IActionResult> Post(Vehicle vehicle)
        {
            await vehicleRepository.AddAsync(vehicle);

            return CreatedAtAction(nameof(GetById), new { Id = vehicle.Id }, vehicle);
        }

        // PUT api/vehicles/10
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Vehicle vehicle)
        {
            if (id != vehicle.Id)
                return BadRequest();

            await vehicleRepository.UpdateAsync(vehicle);

            return NoContent();
        }

        // DELETE api/vehicles/10
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await vehicleRepository.ExistsAsync(id);

            if (!exists)
                return NotFound();

            await vehicleRepository.RemoveAsync(id);

            return NoContent();
        }


    }
}
