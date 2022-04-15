using DriverHire.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Repository
{
    public interface IUnitofWork
    {
      
        //unit of work main //
        public Task BeginTransactionAsync();
        public Task CommitAsync();
        public Task RollbackAsync();
        public Task<int> SaveAsync();

        //IRepositories//
        public IApplicationRoleRepository ApplicationRoleRepository { get; }
        public IBookingRepository BookingRepository { get; }
    }
}
