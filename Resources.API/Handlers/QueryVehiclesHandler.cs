using MediatR;
using Resources.Domain.Models;
using Resources.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resources.API.Handlers
{
    public class QueryCommand : IRequest<Vehicle>
    {
        public QueryCommand(string vIN)
        {
            VIN = vIN;
        }

        public string VIN { get; set; }
    }

    public class QueryVehiclesHandler : IRequestHandler<QueryCommand, Vehicle>
    {
        private readonly IVehicleRepository vehicleRepository;

        public QueryVehiclesHandler(IVehicleRepository vehicleRepository)
        {
            this.vehicleRepository = vehicleRepository;
        }

        public Task<Vehicle> Handle(QueryCommand request, CancellationToken cancellationToken)
        {
            return vehicleRepository.GetAsync(request.VIN);
        }
    }
}
