using DriverHire.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Entity.Entity
{
    public class Booking
    {
        public int Id { get; set; }
        public string DestinationFromCoordinate { get; set; }
        public string DestinationFrom {get; set;}
        public string DestinationToCoordinate { get; set; }
        public string DestinationTo { get; set; }

        public DateTime DateTime { get; set; }

        public VehicleType VehicleType { get; set; }

        public Brand Brand { get; set; }

        public int Duration { get; set; }

        public Shift Shift { get; set; }
        public string PickUpLocationCordinate { get; set; }
        public string PickUpLocation { get; set;}
        public string Message { get; set; }
        public int? CustomerId { get; set; }
        public ApplicationUser Customer { get; set; }
        public bool IsBooked { get; set; }
        public double TotalCharge { get; set; }
        public DateTime? BookingAcceptedDate { get; set; }
        public DateTime? CanceledDate { get; set; }
        public int? CancelById { get; set; }
        public ApplicationUser CancelBy { get; set; }
        public int? DriverId { get; set; }
        public ApplicationUser Driver { get; set; }

        public ICollection<FeedBack> FeedBacks { get; set; }


    }
}
