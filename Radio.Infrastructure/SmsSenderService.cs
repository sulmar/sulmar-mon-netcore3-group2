using Radio.Domain.Services;
using System;
using System.Diagnostics;

namespace Radio.Infrastructure
{
    public class SmsSenderService : ISenderService
    {
        public void Send(string message)
        {
            Trace.WriteLine($"Send {message}");
        }
    }
}
