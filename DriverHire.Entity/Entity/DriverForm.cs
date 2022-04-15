using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Entity.Entity
{
    public class DriverForm
    {
        public string Name { get; set; }
        public string DateOfBirth { get; set; }
        public string VehicleType {get; set;}
        public double Rate { get; set;}
        public string LicensePhoto { get; set; }
        public string CitizenPhoto { get; set; }
        public string Shift { get; set; }

    }
}
