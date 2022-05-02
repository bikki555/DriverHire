using DriverHire.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Entity.Dto
{
    public class DriverDetailDto
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public VehicleType VehicleType { get; set; }
        public string Rate { get; set; }
        public string LicensePhotoFile { get; set; }
        public string CitizenPhotoFile { get; set; }
        public Shift Shift { get; set; }
    }
}
