using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Entity.Dto
{
    public class DriverRecommendationDto
    {
        public int DriverId { get; set; }
        public string DriverName { get; set; }
        public string ContactNo { get; set; }
        public  decimal Price { get; set; }
    }
}
