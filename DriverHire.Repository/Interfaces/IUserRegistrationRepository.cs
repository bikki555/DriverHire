using DriverHire.Entity.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Repository.Interfaces
{
    public interface IUserRegistrationRepository : IRepository<ApplicationUser>
    {
    }
}
