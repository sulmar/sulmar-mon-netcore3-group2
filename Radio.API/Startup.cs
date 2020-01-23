using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Radio.API.Middlewares;
using Radio.Domain.Services;
using Radio.Infrastructure;

namespace Radio.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddTransient<ISenderService, SmsSenderService>();
            services.AddLogger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }



            app.Use(async (context, next) =>
            {
                Trace.WriteLine($"request {context.Request.Method} {context.Request.Path}");

                await next.Invoke();

                Trace.WriteLine($"response {context.Response.StatusCode}");
            });

            // app.UseMiddleware<LoggerMiddleware>();

            app.UseLogger();

            // GET /dashboard

            app.UseDashboard();

            //app.Map("/dashboard", 
            //    options => options.Run(context => context.Response.WriteAsync("Hello Dashboard")));

            //   app.Map("/dashboard", DashboardHandler);

            app.Map("/sensors", node =>
            {               
                // switch
                node.Map("/temp", 
                    options => options.Run(context => context.Response.WriteAsync("Temperature")));
                node.Map("/humidity",
                   options => options.Run(context => context.Response.WriteAsync("Humidity")));

                // default:
                node.Map(string.Empty,
                    options => options.Run(context => context.Response.WriteAsync("All sensors")));

            });


          //  app.Run(context => context.Response.WriteAsync("Hello World!"));

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });

                endpoints.MapPost("api/radios", async context =>
                {
                    context.Response.StatusCode = 201;

                   await context.Response.WriteAsync("Created!");
                });

                endpoints.MapGet("api/radios/{number:int}", GetRadioHandler);

            });
        }

        private async Task GetRadioHandler(HttpContext context)
        {
            context.Request.RouteValues.TryGetValue("number", out object number);
            await context.Response.WriteAsync($"Hello radio {number}");
        }

        private void DashboardHandler(IApplicationBuilder app)
        {
           
        }
    }
}
