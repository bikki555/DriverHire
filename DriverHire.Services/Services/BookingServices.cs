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
        private readonly IBookingRepository _bookingRepository;

        public BookingServices(IUnitofWork unitofWork,IBookingRepository bookingRepository)
        {
            _unitofWork = unitofWork;
            _bookingRepository = bookingRepository;
        }

        public async Task<Booking> Save(Booking entity)
        {
            //mapping //
            var result =(await _bookingRepository.Insert(entity)).Entity;
            await _unitofWork.SaveAsync();
            return result;
        }
    }
}
