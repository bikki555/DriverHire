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
        public Task<int> Save(ClientBookingDto dto);
        public Task<BookingDetailsDto> BookingHistory(int bookingId, bool?isCanceled);
        public class BookingServices : IBookingServices
        {
            private readonly IUnitofWork _unitofWork;
            private readonly IBookingRepository _bookingRepository;

            public BookingServices(IUnitofWork unitofWork, IBookingRepository bookingRepository)
            {
                _unitofWork = unitofWork;
                _bookingRepository = bookingRepository;
            }

            public Task<BookingDetailsDto> BookingHistory(int bookingId, bool? isCanceled)
            {
                throw new NotImplementedException();
            }
            public async Task<int> Save(ClientBookingDto dto)
            {
                //mapping //
                var entity = new Booking
                {
                    DestinationFrom = dto.DestinationFrom,
                    DestinationTo = dto.DestinationTo,
                    DateTime = dto.DateTime,
                    VehicleType = dto.VehicleType,
                    Brand = dto.Brand,
                    Duration = dto.Duration,
                    Shift = dto.Shift,
                    PickUpLocation = dto.PickUpLocation,
                    Message = dto.Message
                };
                var result = (await _bookingRepository.Insert(entity)).Entity;
                await _unitofWork.SaveAsync();
                return result.Id;
            }
        }

    }
}
