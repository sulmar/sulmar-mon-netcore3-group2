using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Resources.Domain.Services
{
    public interface ISenderService
    {
        void Send(string message);
    }


}
