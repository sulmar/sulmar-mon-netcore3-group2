using System;
using System.IO;

namespace Resources.Domain.Models
{

    // 
    public class Vehicle : BaseEntity
    {
        public string Name { get; set; }
        public string VIN { get; set; }
        public string Model { get; set; }
        public short ProductionYear { get; set; }
        public bool IsSelected { get; set; }
        public Person Owner { get; set; }
        public Address GarageAddress { get; set; }
        public bool IsRemoved { get; set; }
        public byte[] Version { get; set; }
    }
}
