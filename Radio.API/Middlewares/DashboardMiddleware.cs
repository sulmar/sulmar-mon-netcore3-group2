using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radio.API.Middlewares
{
    public class DashboardMiddleware
    {
        private readonly RequestDelegate next;

        public DashboardMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string html = "<html><b>Hello dashboard</b></html>";

            context.Response.Headers.Add("Content-Type", new StringValues("text/html"));
            await context.Response.WriteAsync(html);
        }
    }

    public static class DashboardMiddlewareExtensions
    {
        public static IApplicationBuilder UseDashboard(this IApplicationBuilder app, 
            string path = "/dashboard")
        {
           return app.Map(path, 
                options => options.UseMiddleware<DashboardMiddleware>());
        }
    }
}
