using System;
using System.Collections.Generic;
using System.Text;

namespace Radio.Domain.Services
{
    public interface ISenderService
    {
        void Send(string message);
    }
}
