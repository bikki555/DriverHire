using DriverHire.Entity.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DriverHire.Entity.Dto
{
    public class DriverFormPostDto
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string VehicleType { get; set; }
        public double Rate { get; set; }
        public IFormFile LicensePhotoFile { get; set; }
        public IFormFile CitizenPhotoFile { get; set; }
        public Shift Shift { get; set; }
    }
}
