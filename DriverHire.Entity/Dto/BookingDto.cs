using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Entity.Dto
{
    public class BookingDto
    {
        public string Name { get; set; }

        public decimal Rate { get; set; }

        public decimal Duration { get; set; }

        public DateTime DateTime { get; set; }

    }
}
