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

        public string VehicleType { get; set; }

        public string Brand { get; set; }

        public decimal Duration { get; set; }

        public string Shift { get; set; }

        public string PickUpLocation { get; set;}

        public string Message { get; set; }

        
   }
}
