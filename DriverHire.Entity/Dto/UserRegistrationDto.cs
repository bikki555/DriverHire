using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Entity.Dto
{
    public class UserRegistrationDto
    {
        public string Otp { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [DisplayName("Confirm Password")]
        [Compare(nameof(Password), ErrorMessage = "Password and Confirm Password do not match.")]
        public string ConfirmPassword { get; set; }
        public bool IsCustomer { get; set; }
    }
}
