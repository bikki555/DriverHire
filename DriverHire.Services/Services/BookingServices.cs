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
        public Task<IEnumerable<BookingHistoryDetailDto>> BookingHistoryDetails(int? userId);
        public Task<int> BookingAcceptDriver(int bookingId);
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

            public async Task<int> BookingAcceptDriver(int bookingId)
            {
                try
                {
                    var booking = await _bookingRepository.GetById(bookingId);
                    booking.IsBooked = true;
                    booking.BookingAcceptedDate = DateTime.Now;
                    booking.DriverId = (await _userregistrationServices.GetLoggedInUser()).Id;
                    _bookingRepository.Update(booking);
                    await _unitofWork.SaveAsync();
                    return bookingId;
                }
                catch (Exception)
                {
                    throw;
                }
            }

            public async Task<BookingDetailsDto> BookingDetails(int bookingId)
            {
                var includes = new[]
               {
                   nameof(Booking.Customer),
                   nameof(Booking.Driver),
                };
                var applicationUser = await _userregistrationServices.Get(null);
                var booking = (await _bookingRepository.SelectWhereInclude(includes, x =>x.Id==bookingId
                ))
                    .Select(x => new BookingDetailsDto
                    {
                        BookingDate = x.DateTime,
                        DestinationFromCoordinate=x.DestinationFromCoordinate,
                        DestinationFrom = x.DestinationFrom,
                        DestinationToCoordinate=x.DestinationToCoordinate,
                        DestinationTo = x.DestinationTo,
                        VehicleType=x.VehicleType,
                        Brand=x.Brand,
                        Duration=x.Duration,
                        Shift=x.Shift,
                        TotalCharge = x.TotalCharge,
                        PickUpLocationCoordinate=x.PickUpLocationCordinate,
                        PickUpLocation=x.PickUpLocation,
                        CustomerName = applicationUser.Where(au => au.Id == x.CustomerId).FirstOrDefault().UserName,
                        CustomerPhoneNumber= applicationUser.Where(au => au.Id == x.CustomerId).FirstOrDefault().PhoneNumber,
                        DriverName = applicationUser.Where(au => au.Id == x.DriverId).FirstOrDefault().UserName,
                        DriverPhoneNumber = applicationUser.Where(au => au.Id == x.DriverId).FirstOrDefault().PhoneNumber
                    }).FirstOrDefault();
                return booking;
            }
            public async Task<IEnumerable<BookingHistoryDetailDto>> BookingHistoryDetails(int? userId)
            {
                var includes = new[]
                {
                   nameof(Booking.Customer),
                   nameof(Booking.Driver),
                };

                if (userId is null)
                {
                    var loggedUser = (await _userregistrationServices.GetLoggedInUser());
                    userId = loggedUser.Id;
                }
                var applicationUser = await _userregistrationServices.Get(userId);
                var bookings = (await _bookingRepository.SelectWhereInclude(includes, x => (!userId.HasValue
                || 
                ((applicationUser.FirstOrDefault().IsCustomer==true && x.CustomerId==userId)
                ||
                (applicationUser.FirstOrDefault().IsCustomer == false &&  x.DriverId == userId))
                )
                
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
