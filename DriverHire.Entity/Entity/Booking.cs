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
        public string DestinationFrom {get; set;}

        public string DestinationTo { get; set; }

        public DateTime DateTime { get; set; }

        public VehicleType VehicleType { get; set; }

        public string Brand { get; set; }

        public decimal Duration { get; set; }

        public string Shift { get; set; }

        public string PickUpLocation { get; set;}
        public string Message { get; set; }
        public int? CustomerId { get; set; }
        public ApplicationUser Customer { get; set; }
        public bool IsBooked { get; set; }
        public DateTime? BookingAcceptedDate { get; set; }
        public DateTime? CanceledDate { get; set; }
        public int? CancelById { get; set; }
        public ApplicationUser CancelBy { get; set; }
        public int? DriverId { get; set; }
        public ApplicationUser Driver { get; set; }

        public ICollection<FeedBack> FeedBacks { get; set; }


    }
}
