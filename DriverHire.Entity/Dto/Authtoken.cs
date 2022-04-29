using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Entity.Dto
{
    public class Authtoken
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public bool IsCustomer { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }
    }
}
