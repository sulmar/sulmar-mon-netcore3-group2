using Resources.Domain.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Resources.Infrastructure
{
    public class SmsSenderService : ISenderService
    {
        public void Send(string message)
        {
            Trace.WriteLine($"Send {message}");
        }
    }
}
