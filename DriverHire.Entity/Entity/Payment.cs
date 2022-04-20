using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Entity.Entity
{
    public class Payment
    {
        public string PaymentType { get; set; }
        public string Method { get; set; }
        public decimal TotalAmount { get; set; }


    }
}
