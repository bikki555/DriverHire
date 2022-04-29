using DriverHire.Entity.Dto;
using DriverHire.Entity.Entity;
using DriverHire.Repository;
using DriverHire.Repository.Interfaces;
using DriverHire.Services.Mapping;
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
        public Task<int> Save(ClientBookingDto dto);
        public Task<BookingDetailsDto> BookingDetails(int bookingId);
        public Task<IEnumerable<BookingHistoryDetailDto>> BookingHistoryDetails();
        public class BookingServices : IBookingServices
        {
            private readonly IUnitofWork _unitofWork;
            private readonly IBookingRepository _bookingRepository;
            private readonly IUserRegistrationServices _userregistrationServices;

            public BookingServices(IUnitofWork unitofWork, IBookingRepository bookingRepository,IUserRegistrationServices userRegistrationServices)
            {
                _unitofWork = unitofWork;
                _bookingRepository = bookingRepository;
                _userregistrationServices = userRegistrationServices;
            }
            public Task<BookingDetailsDto> BookingDetails(int bookingId)
            {
                throw new NotImplementedException();
            }
            public async Task<IEnumerable<BookingHistoryDetailDto>> BookingHistoryDetails()
            {
                var includes = new[]
                {
                   nameof(Booking.Customer),
                   nameof(Booking.Driver),
                };
                var loggedUser = (await _userregistrationServices.GetLoggedInUser());
                var applicationUser = await _userregistrationServices.Get(null);
                var bookings = (await _bookingRepository.SelectWhereInclude(includes, x => loggedUser.IsCustomer ? x.CustomerId == loggedUser.Id : x.DriverId == loggedUser.Id
                && x.IsBooked
                ))
                    .Select(x => new BookingHistoryDetailDto
                    {
                        BookingDate=x.DateTime,
                        DestinationFrom=x.DestinationFrom,
                        DestinationTo=x.DestinationTo,
                        TotalCharge=x.TotalCharge,
                        CustomerName= applicationUser.Where(au=>au.Id==x.CustomerId).FirstOrDefault().UserName,
                        DriverName= applicationUser.Where(au=>au.Id==x.DriverId).FirstOrDefault().UserName
                    });
                return bookings;
            }
            public async Task<int> Save(ClientBookingDto dto)
            {
                var entity = dto.ToEntity();
                entity.CustomerId = (await _userregistrationServices.GetLoggedInUser()).Id;
                var result = (await _bookingRepository.Insert(entity)).Entity;
                await _unitofWork.SaveAsync();
                return result.Id;
            }
        }

    }
}
