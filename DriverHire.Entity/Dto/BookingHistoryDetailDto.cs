using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Entity.Dto
{
    public class BookingHistoryDetailDto
    {
        public DateTime BookingDate { get; set; }
        public string DestinationFrom { get; set; }

        public string DestinationTo { get; set; }
        public double TotalCharge { get; set; }
        public string CustomerName { get; set; }
        public string DriverName { get; set; }
    }
}
