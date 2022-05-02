using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Entity.Dto
{
    public class ResetPasswordDto
    {
        public string Otp { get; set; }
        public string Password { get; set; }
        [Required]
        [DisplayName("Confirm Password")]
        [Compare(nameof(Password), ErrorMessage = "Password and Confirm Password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
