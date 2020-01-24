using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Resources.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Resources.Infrastructure.DbRepositories.Configurations
{



    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder
                .Ignore(p => p.IsSelected);

            builder
                .Property(p => p.Model)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder
             .Property(p => p.VIN)
             .HasMaxLength(20)
             .IsRequired();

            builder
                .HasQueryFilter(p => p.IsRemoved == false);

            //builder.Property(p => p.GarageAddress)
            //    .HasConversion(new JsonValueConverter<Address>());

            builder.Property(p => p.GarageAddress)
                .HasJsonValueConversion();

            builder
                .Property<DateTime>("LastUpdated");

            builder
                .Property(p => p.ProductionYear);

            builder
                .Property(p => p.Version)
                .IsRowVersion()
                .IsConcurrencyToken();
        }
    }

    
}
