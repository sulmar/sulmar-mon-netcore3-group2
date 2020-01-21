using System;
using System.Collections.Generic;
using System.Text;

namespace Resources.Domain.Models
{
    public class VehicleSearchCriteria
    {
        public short? From { get; set; }
        public short? To { get; set; }
        public string Name { get; set; }
    }
}
