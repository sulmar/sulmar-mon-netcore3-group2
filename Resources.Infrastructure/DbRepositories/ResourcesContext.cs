using Microsoft.EntityFrameworkCore;
using Resources.Domain.Models;
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


    }
}
