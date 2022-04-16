using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Entity.Entity
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }
        public bool IsCustomer { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpiryDate { get; set; }
        public DriverForm DriverForm { get; set; }
    }
}
