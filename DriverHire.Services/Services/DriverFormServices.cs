using DriverHire.Entity.Dto;
using DriverHire.Entity.Entity;
using DriverHire.Repository;
using DriverHire.Repository.Interfaces;
using DriverHire.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Services.Services
{
    public interface IDriverFormServices
    {
        public Task<DriverFormPostDto> Save(DriverFormPostDto Dto);
        public Task<IEnumerable<DriverDetailDto>> Get(int? driverId);
        Task<IEnumerable<DriverRecommendationDto>> Recommendation(int bookingId);
    }
    public class DriverFormServices : IDriverFormServices
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IDriverFormRepository _DriverFormRepository;
        private readonly IImageProcessingServices _imageProcessingServices;
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRegistrationServices _userRegistrationServices;

        public DriverFormServices(IUnitofWork unitofWork, IDriverFormRepository DriverFormRepository, IImageProcessingServices imageProcessingServices, IBookingRepository bookingRepository, IUserRegistrationServices userRegistrationServices)
        {
            _unitofWork = unitofWork;
            _DriverFormRepository = DriverFormRepository;
            _imageProcessingServices = imageProcessingServices;
            _bookingRepository = bookingRepository;
            _userRegistrationServices = userRegistrationServices;
        }

        public async Task<IEnumerable<DriverDetailDto>> Get(int? driverId)
        {
            var driverData = (await _DriverFormRepository.SelectWhere(x => !driverId.HasValue
            ||
            x.Id == driverId
            )).Select(x => new DriverDetailDto
            {
                Name = x.Name,
                DateOfBirth = x.DateOfBirth,
                VehicleType = x.VehicleType,
                Rate = x.Rate.ToString(),
                LicensePhotoFile = x.LicensePhoto,
                CitizenPhotoFile = x.CitizenPhoto,
                Shift = x.Shift
            });
            return driverData;

        }

        public async Task<IEnumerable<DriverRecommendationDto>> Recommendation(int bookingId)
        {
            var includes = new[]
            {
                $"{nameof(DriverForm.ApplicationUser)}"
            };
            var booking = await _bookingRepository.GetById(bookingId);
            var applicationUser = await _userRegistrationServices.Get(null);
            return (await _DriverFormRepository.SelectWhereInclude(includes)).Select(x => new DriverRecommendationDto
            {
                DriverId=x.Id,
                DriverName = x.Name,
                ContactNo = applicationUser.Where(a => a.Id == x.UserId)?.FirstOrDefault()?.PhoneNumber,
                Price = Convert.ToDecimal(x.Rate) * booking.Duration
            });
        }
        public async Task<DriverFormPostDto> Save(DriverFormPostDto dto)
        {
            var citizenPhoto = await _imageProcessingServices.UploadImage(dto.CitizenPhotoFile, "cz" + DateTime.Now.ToFileTime().ToString() + dto.Name);
            var licensePhoto = await _imageProcessingServices.UploadImage(dto.LicensePhotoFile, "lc" + DateTime.Now.ToFileTime().ToString() + dto.Name);
            var entity = dto.ToEntity();
            entity.CitizenPhoto = citizenPhoto;
            entity.LicensePhoto = licensePhoto;
            var result = (await _DriverFormRepository.Insert(entity));
            await _unitofWork.SaveAsync();
            return dto;
        }
    }
}
