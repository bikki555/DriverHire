using DriverHire.Data.Context;
using DriverHire.Entity.Entity;
using DriverHire.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Repository.Repository
{
    public class UserRegistrationRepository:Repository<ApplicationUser>, IUserRegistrationRepository
    {

        public UserRegistrationRepository(DriverHireContext context):base(context)
        {

        }
    }
}
