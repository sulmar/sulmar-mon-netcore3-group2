using MediatR;
using Microsoft.AspNetCore.Mvc;
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
    public class SendHandler : INotificationHandler<AddVehicleEvent>
    {
        private readonly ISenderService senderService;

        public SendHandler(ISenderService senderService)
        {
            this.senderService = senderService;
        }

        public Task Handle(AddVehicleEvent notification, CancellationToken cancellationToken)
        {
            Vehicle vehicle = notification.Vehicle;

            senderService.Send($"Vehicle {vehicle.Name} was added");

            return Task.CompletedTask;
        }
    }
}
