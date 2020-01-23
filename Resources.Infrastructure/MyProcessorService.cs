using Microsoft.Extensions.Logging;
using Resources.Domain.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Resources.Infrastructure
{
    public class MyProcessorService : IProcessorService
    {
        private ILogger<MyProcessorService> logger;

        public MyProcessorService(ILogger<MyProcessorService> logger)
        {
            this.logger = logger;
        }

        public void Proccess(string filename)
        {
            logger.LogInformation($"Processing {filename}...");

            Thread.Sleep(TimeSpan.FromMinutes(3));

            logger.LogInformation($"Done {filename}");
        }
    }
}
