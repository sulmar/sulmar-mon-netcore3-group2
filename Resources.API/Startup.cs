using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Resources.Domain.Models;
using Resources.Domain.Models.Validators;
using Resources.Domain.Services;
using Resources.Infrastructure;
using Resources.Infrastructure.Fakers;
using Hangfire;
using Hangfire.SqlServer;
using MediatR;
using Resources.Infrastructure.DbRepositories;
using Microsoft.EntityFrameworkCore;

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

            services.AddScoped<IProcessorService, MyProcessorService>();

            services.Configure<FakeVehicleRepositoryOptions>(
                Configuration.GetSection("Vehicles"));

            // dotnet add package Microsoft.EntityFrameworkCore.SqlServer
            services.AddDbContext<ResourcesContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ResourcesConnection")));

            services.AddOpenApiDocument(options =>
            {
                options.Title = "Resources API";
                options.DocumentName = "ASP.NET Core 3 REST API";
                options.Version = "v1";
                options.Description = "Demo auto-generated API documentation";
            });

            services
                 .AddHangfire(options => 
                 options.UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection")));

            services
                .AddHangfireServer();


            services.AddMediatR(typeof(Startup).Assembly);

           services
                .AddControllers()
                .AddFluentValidation(
                    options => options.RegisterValidatorsFromAssemblyContaining<VehicleValidator>());
        }

        public void ConfigureDevelopment(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // specific code for development

            Configure(app, env);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseHangfireDashboard();
            }

            int customersCount = int.Parse(Configuration["VehiclesCount"]);

            int count = int.Parse(Configuration["Vehicles:Count"]);

            string connectionString = Configuration.GetConnectionString("ResourcesConnection");


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseOpenApi();
            app.UseSwaggerUi3();

            // open url in webrowser https://localhost:5001/swagger

            // generator NSwag
            // https://github.com/RicoSuter/NSwag/wiki/NSwagStudio

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
