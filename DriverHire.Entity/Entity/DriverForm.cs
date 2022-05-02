using DriverHire.Entity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Entity.Entity
{
    public class DriverForm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public VehicleType VehicleType { get; set; }
        public double Rate { get; set; }
        public string LicensePhoto { get; set; }
        public string CitizenPhoto { get; set; }
        public Shift Shift { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ChangedDate { get; set; }
        public int? UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
