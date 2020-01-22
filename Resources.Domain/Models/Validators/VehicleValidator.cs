using FluentValidation;
using Resources.Domain.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Resources.Domain.Models.Validators
{

    // dotnet add package FluentValidations
    public class VehicleValidator : AbstractValidator<Vehicle>
    {
        private readonly IVehicleRepository vehicleRepository;

        public VehicleValidator(IVehicleRepository vehicleRepository)
        {
            this.vehicleRepository = vehicleRepository;

            RuleFor(p => p.Model).NotEmpty()
                .Must((vehicle, property) => ValidModel(vehicle.Name, property));

            RuleFor(p => p.ProductionYear).InclusiveBetween((short)1945, (short)DateTime.Today.Year);
            RuleFor(p => p.VIN).NotEmpty().Length(17);

            RuleFor(p => p.Id).MustAsync(async (id, token) =>
            {
                bool exists = await vehicleRepository.ExistsAsync(id);
                return !exists;
            }).WithMessage("ID Must be unique");
        }

        private bool ValidModel(string manufacture, string model)
        {
            return true;
        }

    }
}
