using System;

namespace Messaging.Domain
{

    public abstract class Base
    {

    }

    public class Message : Base
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
