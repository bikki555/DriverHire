using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Entity.Dto
{
    public class LoginDto
    {
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
