using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using NLog.LayoutRenderers;
using Resources.API.Events;
using Resources.API.Filters;
using Resources.API.Handlers;
using Resources.Domain.Models;
using Resources.Domain.Services;
using Resources.Infrastructure;
using Resources.Infrastructure.Fakers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Resources.API.Controllers
{
    // api/vehicles
    [Authorize(Roles = "Major")]
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleRepository vehicleRepository;
        private readonly IMediator mediator;

        public VehiclesController(
            IVehicleRepository vehicleRepository,
            IMediator mediator)
        {
            this.vehicleRepository = vehicleRepository;
            this.mediator = mediator;
        }

        // GET api/vehicles
        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    var vehicles = await vehicleRepository.GetAsync();

        //    return Ok(vehicles);
        //}

        // Przykład tworzenie własnych ograniczeń (constraints)
        // https://github.com/sulmar/dotnet-core-routecontraint-polish-validators


        // GET api/vehicles/10
        [HttpGet("{id:int}")]
        // MVC 
        // [HttpGet]
        // [Route("{id}")]
        [VehicleExists]
        public async Task<IActionResult> GetById(int id)
        {
            var vehicle = await vehicleRepository.GetAsync(id);

            // zastąpione przez atrybut [VehicleExists]
            //if (vehicle == null)
            //    return NotFound();

            return Ok(vehicle);
        }

        [HttpGet("{vin}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string vin)
        {
            //   var vehicle = await vehicleRepository.GetAsync(vin);

            var vehicle = await mediator.Send(new QueryCommand(vin));

            if (vehicle == null)
                return NotFound();

            return Ok(vehicle);
        }


        // POST api/vehicles/upload
        // Content-Type: multipart/form-data
        [AllowAnonymous]
        [HttpPost("upload")]
        public async Task<IActionResult> OnPostUploadAsync(
            IList<IFormFile> files,
            [FromServices] IBackgroundJobClient backgroundJobs,
            [FromServices] IProcessorService processorService
            // [FromServices] IHubContext<ProgressHub> progressHubContext;
            )
        {
            foreach (var file in files)
            {
                backgroundJobs.Enqueue(() => processorService.Proccess(file.FileName));
            }

            long size = files.Sum(f => f.Length);

            // TODO: Accepted 202

            return Accepted(new { count = files.Count, size });
        }

        // POST api/vehicles
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(Vehicle vehicle, 
            [FromServices] ISenderService senderService)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //await vehicleRepository.AddAsync(vehicle);
            //senderService.Send($"Vehicle {vehicle.Name} was added");
           
            await mediator.Publish(new AddVehicleEvent(vehicle));

            return CreatedAtAction(nameof(GetById), new { Id = vehicle.Id }, vehicle);
        }

        // PUT api/vehicles/10
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Vehicle vehicle)
        {

            if (id != vehicle.Id)
                return BadRequest();
            try
            {
                await vehicleRepository.UpdateAsync(vehicle);
            }
            catch(InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }


            return NoContent();
        }

      
        // DELETE api/vehicles/10
        [HttpDelete("{id}")]
        [VehicleExists]
        public async Task<IActionResult> Delete(int id)
        {
            await vehicleRepository.RemoveAsync(id);

            return NoContent();
        }

        // api/vehicles?from=2000&to=2020&name=Toyota

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get([FromQuery] VehicleSearchCriteria criteria)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            ClaimsIdentity identity = User.Identity as ClaimsIdentity;
            string mobilePhone = identity.FindFirst(ClaimTypes.MobilePhone).Value;

            var vehicles = await vehicleRepository.GetAsync(criteria);
            return Ok(vehicles);
        }
    }
}
