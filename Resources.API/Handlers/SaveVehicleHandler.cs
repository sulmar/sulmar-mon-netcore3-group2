using MediatR;
using Resources.API.Events;
using Resources.Domain.Models;
using Resources.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resources.API.Handlers
{
    public class SaveVehicleHandler : INotificationHandler<AddVehicleEvent>
    {
        private readonly IVehicleRepository vehicleRepository;

        public SaveVehicleHandler(IVehicleRepository vehicleRepository)
        {
            this.vehicleRepository = vehicleRepository;
        }

        public async Task Handle(AddVehicleEvent notification, CancellationToken cancellationToken)
        {
            await vehicleRepository.AddAsync(notification.Vehicle);
        }
    }
}
