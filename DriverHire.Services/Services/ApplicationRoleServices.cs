using DriverHire.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Services.Services
{
    public interface IApplicationRoleSevices
    { 
    }
    public class ApplicationRoleServices: IApplicationRoleSevices
    {
        private readonly IUnitofWork _unitOfWork;
        public ApplicationRoleServices(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
