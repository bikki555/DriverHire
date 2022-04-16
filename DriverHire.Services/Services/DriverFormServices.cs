using DriverHire.Entity.Entity;
using DriverHire.Repository;
using DriverHire.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Services.Services
{
    public interface IDriverFormServices
    {
        public Task<DriverForm> Save(DriverForm entity);
    }
    public class DriverFormServices : IDriverFormServices
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IDriverFormRepository _DriverFormRepository;

        public DriverFormServices(IUnitofWork unitofWork, IDriverFormRepository DriverFormRepository)
        {
            _unitofWork = unitofWork;
            _DriverFormRepository = DriverFormRepository;
        }

        public async Task<DriverForm> Save(DriverForm entity)
        {
            //mapping //
            var result = (await _DriverFormRepository.Insert(entity)).Entity;
            await _unitofWork.SaveAsync();
            return result;
        }
    }
}
