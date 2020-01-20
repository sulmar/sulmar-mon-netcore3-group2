using Bogus;
using Resources.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resources.Infrastructure.Fakers
{
    public class VehicleFaker : Faker<Vehicle>
    {
        public VehicleFaker()
        {
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.Name, f => f.Vehicle.Model());
            RuleFor(p => p.VIN, f => f.Vehicle.Vin());
        }
    }
}
