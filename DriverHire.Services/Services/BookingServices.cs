using DriverHire.Entity.Entity;
using DriverHire.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Services.Services
{
    public interface IBookingServices
    {
        //add
        public Task<Booking> Save(Booking entity);
        //delete
        //update
    }
    public class BookingServices : IBookingServices
    {
        private readonly IUnitofWork _unitofWork;

        public BookingServices(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<Booking> Save(Booking entity)
        {
            //mapping //
            var result =await _unitofWork.BookingRepository.Insert(entity);
            return result;
           
        }
    }
}
