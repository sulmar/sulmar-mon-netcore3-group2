using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Radio.Domain.Services;
using Radio.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Radio.API.Middlewares
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ISenderService senderService;

        public LoggerMiddleware(RequestDelegate next, ISenderService senderService)
        {
            this.next = next;
            this.senderService = senderService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Trace.WriteLine($"request {context.Request.Method} {context.Request.Path}");
            senderService.Send(context.Request.Path);

            await next.Invoke(context);

            Trace.WriteLine($"response {context.Response.StatusCode}");
        }
    }

    public static class LoggerMiddlewareExtensions
    {
        public static IServiceCollection AddLogger(this IServiceCollection services)
        {
            return services.AddTransient<ISenderService, SmsSenderService>();
        }

        public static IApplicationBuilder UseLogger(this IApplicationBuilder builder)
        {
            ISenderService senderService = builder.ApplicationServices.GetService<ISenderService>();

            if (senderService == null)
                throw new InvalidOperationException("Please add AddLogger in ConfigureServices");

            return builder.UseMiddleware<LoggerMiddleware>();
        }
    }

  
}
