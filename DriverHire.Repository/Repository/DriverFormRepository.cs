using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Repository.Repository
{
     public class DriverFormRepository: Repository<DriverFormRepository>, IDriverFormRepository
     {
        public DriverFormRepository(DriverHireContext context): base(context)
        {

        }
     }
}
