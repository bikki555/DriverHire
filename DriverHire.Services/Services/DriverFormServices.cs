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
    }
    public class DriverFormServices : IDriverFormServices
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IDriverFormRepository _DriverFormRepository;
        private readonly IImageProcessingServices _imageProcessingServices;

        public DriverFormServices(IUnitofWork unitofWork, IDriverFormRepository DriverFormRepository,IImageProcessingServices imageProcessingServices)
        {
            _unitofWork = unitofWork;
            _DriverFormRepository = DriverFormRepository;
            _imageProcessingServices = imageProcessingServices;
        }

        public async Task<DriverFormPostDto> Save(DriverFormPostDto dto)
        {
            var citizenPhoto= await _imageProcessingServices.UploadImage(dto.CitizenPhotoFile, "cz" + DateTime.Now.ToFileTime().ToString() + dto.Name);
            var licensePhoto=await _imageProcessingServices.UploadImage(dto.LicensePhotoFile, "lc" + DateTime.Now.ToFileTime().ToString() + dto.Name);
            var entity = dto.ToEntity();
            entity.CitizenPhoto = citizenPhoto;
            entity.LicensePhoto = licensePhoto;
            var result = (await _DriverFormRepository.Insert(entity));
            await _unitofWork.SaveAsync();
            return dto;
        }
    }
}
