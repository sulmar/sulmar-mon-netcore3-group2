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
            UseSeed(1);
            StrictMode(true);
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.Name, f => f.Vehicle.Manufacturer());
            RuleFor(p => p.Model, f => f.Vehicle.Model());
            RuleFor(p => p.VIN, f => f.Vehicle.Vin());
            RuleFor(p => p.ProductionYear, f => f.Random.Short(1990, 2019));
            Ignore(p => p.IsSelected);
        }
    }
}
