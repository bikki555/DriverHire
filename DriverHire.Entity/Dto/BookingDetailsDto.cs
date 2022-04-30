using DriverHire.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Entity.Dto
{
    public class BookingDetailsDto
    {
        public DateTime BookingDate { get; set; }
        public string DestinationFromCoordinate { get; set; }
        public string DestinationFrom { get; set; }
        public string DestinationToCoordinate { get; set; }
        public string DestinationTo { get; set; }
        public VehicleType VehicleType { get; set; }
        public Brand Brand { get; set; }
        public decimal Duration { get; set; }
        public Shift Shift { get; set; }
        public double TotalCharge { get; set; }
        public string PickUpLocationCoordinate { get; set; }
        public string PickUpLocation { get; set; }
        public string Message { get; set; }
        public string CustomerName { get; set; }
        public string DriverPhoneNumber { get; set; }
        public string DriverName { get; set; }
        public string CustomerPhoneNumber { get; set; }
    }
}
