using System;
using System.IO;

namespace Resources.Domain.Models
{

    public class Vehicle : BaseEntity
    {
        public string Name { get; set; }
        public string VIN { get; set; }
    }
}
