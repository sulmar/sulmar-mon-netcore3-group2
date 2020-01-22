using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Domain.Services
{
    public interface IMessageClient
    {
        Task YouHaveGotMessage(Message message);
        Task Pong(string message = "");
    }
}
