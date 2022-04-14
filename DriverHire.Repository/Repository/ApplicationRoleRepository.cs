using DriverHire.Data.Context;
using DriverHire.Entity.Entity;
using DriverHire.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Repository.Repository
{
    public class ApplicationRoleRepository:Repository<ApplicationRole>,IApplicationRoleRepository
    {
        public ApplicationRoleRepository(DriverHireContext context):base(context)
        {

        }
    }
}
