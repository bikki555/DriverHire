using DriverHire.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Entity.Entity
{
    public class Payment
    {
        public int Id { get; set; }
        public PaymentTypes PaymentType { get; set; }
        public PaymentMethod Method { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
