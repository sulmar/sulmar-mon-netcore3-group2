using Microsoft.EntityFrameworkCore;
using Resources.Domain.Models;
using Resources.Infrastructure.DbRepositories.Configurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resources.Infrastructure.DbRepositories
{
    // dotnet add package Microsoft.EntityFrameworkCore
    public class ResourcesContext : DbContext
    {
        public ResourcesContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // EF6 modelBuilder.Configurations.Add(new VehicleConfiguration);
            modelBuilder
                //.ApplyConfiguration(new VehicleConfiguration())
                //.ApplyConfiguration(new PersonConfiguration())
                .ApplyConfigurationsFromAssembly(typeof(VehicleConfiguration).Assembly);

            base.OnModelCreating(modelBuilder);
        }


    }

    // dotnet add package Microsoft.EntityFrameworkCore.Tools
    // dotnet add package Microsoft.EntityFrameworkCore.Design
    // dotnet add package Microsoft.EntityFrameworkCore.Relational
}
