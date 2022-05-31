using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Entity.Dto
{
    public class FeedBackDto
    {
        public int Ratings { get; set; }
        public string FeedBackMessage { get; set; }
        public int BookingId { get; set; }
}
}
