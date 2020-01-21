using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Resources.Domain.Models;
using Resources.Domain.Services;
using Resources.Infrastructure;
using Resources.Infrastructure.Fakers;

namespace Resources.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IVehicleRepository, FakeVehicleRepository>();
            services.AddScoped<Faker<Vehicle>, VehicleFaker>();
            services.AddScoped<ISenderService, SmsSenderService>();

            services.Configure<FakeVehicleRepositoryOptions>(
                Configuration.GetSection("Vehicles"));

        //    services.AddControllers();
        }

        public void ConfigureDevelopment(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // specific code for development

            Configure(app, env);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            int customersCount = int.Parse(Configuration["VehiclesCount"]);

            int count = int.Parse(Configuration["Vehicles:Count"]);

            string connectionString = Configuration.GetConnectionString("ResourcesConnection");


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }


    public static class IWebHostEnvironmentExtensions
    {
        public static bool IsTesting(this IWebHostEnvironment env)
        {
            return env.EnvironmentName == "Testing";
        }
    }

  
}
