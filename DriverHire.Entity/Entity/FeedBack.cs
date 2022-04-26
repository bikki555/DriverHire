using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Entity.Entity
{
    public class FeedBack
    {
        public int Id { get; set; }
        public int DRating { get; set; }
        public string DFeedBackMessage { get; set; }
        public int BookingId { get; set; }
        public Booking Booking { get; set; }
        public int CRating { get; set; }
        public string CFeedBackMessage { get; set; }

    }
}
