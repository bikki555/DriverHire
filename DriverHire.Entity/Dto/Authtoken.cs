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
    }
}
