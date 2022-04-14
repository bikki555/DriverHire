using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Entity.Entity
{
    public class ApplicationUser:IdentityUser
    {
        public bool IsCustomer { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpiryDate { get; set; }
        public string Otp { get; set; }
        public DateTime OtpExpiryDate { get; set; }
    }
}
