using MediatR;
using Resources.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resources.API.Events
{
    public class AddVehicleEvent : INotification
    {
        public AddVehicleEvent(Vehicle vehicle)
        {
            Vehicle = vehicle;
        }

        public Vehicle Vehicle { get; private set; }
    }
}
