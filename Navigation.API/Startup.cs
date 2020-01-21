using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Navigation.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // dotnet add package Microsoft.AspNetCore.Owin
            app.UseOwin(pipeline => pipeline(env => OwinHandler));
            
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }

        // OWIN
        // http://owin.org/html/spec/owin-1.0.html
        private async Task OwinHandler(IDictionary<string, object> environment)
        {
            string requestMethod = (string) environment["owin.RequestMethod"];
            string requestPath = (string)environment["owin.RequestPath"];

            Stream responseBody = (Stream)environment["owin.ResponseBody"];
            var responseHeaders = 
                (IDictionary<string, string[]>)environment["owin.ResponseHeaders"];
            responseHeaders["Content-Type"] = new string[] { "text/plain" };

            string response = "Hello OWIN!";
            byte[] responseBytes = Encoding.UTF8.GetBytes(response);
            await responseBody.WriteAsync(responseBytes, 0, responseBytes.Length);

        }
    }
}
