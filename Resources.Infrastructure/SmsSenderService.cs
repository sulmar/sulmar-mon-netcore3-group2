using Microsoft.Extensions.Logging;
using Resources.Domain.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Resources.Infrastructure
{
    public class SmsSenderService : ISenderService
    {
        // dotnet add package Microsoft.Extensions.Logging
        private readonly ILogger<SmsSenderService> logger;

        public SmsSenderService(ILogger<SmsSenderService> logger)
        {
            this.logger = logger;
        }

        public void Send(string message)
        {
            logger.LogInformation($"Send {message}");
        }
    }
}
