using DriverHire.Data.Context;
using DriverHire.Entity;
using DriverHire.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Repository.Repository
{
    public class RegisterRepository:Repository<Register>,IRegisterRepository
    {
        public RegisterRepository(DriverHireContext context):base(context)
        {

        }
    }
}
