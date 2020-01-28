using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Resources.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resources.API.Filters
{
    public class VehicleExistsAttribute : TypeFilterAttribute
    {
        public VehicleExistsAttribute() : base(typeof(VehicleExistsFilter))
        {
        }

        private class VehicleExistsFilter : IAsyncActionFilter
        {
            private readonly IVehicleRepository vehicleRepository;

            public VehicleExistsFilter(IVehicleRepository vehicleRepository)
            {
                this.vehicleRepository = vehicleRepository;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (!context.ActionArguments.ContainsKey("id"))
                {
                    context.Result = new BadRequestResult();
                    return;
                }

                if (!(context.ActionArguments["id"] is int id))
                {
                    context.Result = new BadRequestResult();
                    return;
                }

                var exists = await vehicleRepository.ExistsAsync(id);

                if (!exists)
                {
                    context.Result = new NotFoundResult();
                    return;
                }

                await next();
            }
        }
    }
}
