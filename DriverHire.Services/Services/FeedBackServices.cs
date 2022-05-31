using DriverHire.Entity.Dto;
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
    public interface IFeedBackServices
    {
        public Task<bool> SaveDriverFeedBack(FeedBackDto dto);
        public Task<FeedBackDto> GetDriverFeedBack(int bookingId);
        public Task<int> GetDriverOverAllRating(int driverId);
    }

    public class FeedBackServices : IFeedBackServices
    {
        private readonly IFeedBackRepository _feedBackRepository;
        private readonly IUnitofWork _unitofWork;

        public FeedBackServices(IFeedBackRepository feedBackRepository, IUnitofWork unitofWork)
        {
            _feedBackRepository = feedBackRepository;
            _unitofWork = unitofWork;
        }
        public async Task<FeedBackDto> GetDriverFeedBack(int bookingId)
        {
            var includes = new[]
            {
                $"{nameof(FeedBack.Booking)}"
            };
            return (await _feedBackRepository.SelectWhereInclude(includes, x => x.BookingId == bookingId))
                .Select(x => new FeedBackDto
                {
                    Ratings = x.DRating,
                    FeedBackMessage = x.DFeedBackMessage
                }).FirstOrDefault();
        }
        public async Task<int> GetDriverOverAllRating(int driverId)
        {
            var includes = new[]
            {
                $"{nameof(FeedBack.Booking)}.{nameof(Booking.Driver)}"
            };
            return (int)(await _feedBackRepository.SelectWhereInclude(includes, x => x.Booking.DriverId == driverId))
                 .Average(x => x.DRating);
        }
        public async Task<bool> SaveDriverFeedBack(FeedBackDto dto)
        {
            var entity = new FeedBack
            {
                DRating = dto.Ratings,
                DFeedBackMessage = dto.FeedBackMessage,
                BookingId=dto.BookingId
            };
            try
            {
                await _feedBackRepository.Insert(entity);
                await _unitofWork.SaveAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            

        }
    }
}
