using DriverHire.Entity.Dto;
using DriverHire.Entity.Entity;
using DriverHire.Repository;
using DriverHire.Repository.Interfaces;
using DriverHire.Services.Services;
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
        public Task<BookingDto> Save(BookingDto Dto);
        Task<IEnumerable<BookingDto>> Recommendation(int bookingId);
        //delete
        //update
        public class BookingServices : IBookingServices
        {
            private readonly IUnitofWork _unitofWork;
            private readonly IBookingRepository _bookingRepository;
            private readonly IBookingServices _bookingServices;

            public BookingServices(IUnitofWork unitofWork, IBookingRepository bookingRepository)
            {
                _unitofWork = unitofWork;
                _bookingRepository = bookingRepository;
            }

            public async Task<IEnumerable<BookingDto>> Recommendation(int bookingId)
            {

                var booking = await _bookingRepository.GetById(bookingId);
                return (await _bookingRepository.GetAll()).Select(x => new BookingDto
                {

                    Duration = x.Duration,
                    DateTime = x.DateTime
                }
                  );

            }
        }

    }


    public class BookingServices : IBookingServices
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IBookingRepository _bookingRepository;

        public BookingServices(IUnitofWork unitofWork, IBookingRepository bookingRepository)
        {
            _unitofWork = unitofWork;
            _bookingRepository = bookingRepository;
        }



        public async Task<Booking> Save(BookingDto entity)
        {
            //mapping //
            var result = (await _bookingRepository.Insert(entity)).Entity;
            await _unitofWork.SaveAsync();
            return result;
        }
    }
}


