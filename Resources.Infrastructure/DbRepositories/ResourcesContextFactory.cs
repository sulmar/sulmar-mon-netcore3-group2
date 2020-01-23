using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resources.Infrastructure.DbRepositories
{
    public class ResourcesContextFactory : IDesignTimeDbContextFactory<ResourcesContext>
    {
        public ResourcesContext CreateDbContext(string[] args)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ResourcesDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            var optionsBuilder = new DbContextOptionsBuilder<ResourcesContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new ResourcesContext(optionsBuilder.Options);
        }
    }
}
