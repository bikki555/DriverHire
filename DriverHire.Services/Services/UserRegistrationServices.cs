using DriverHire.Repository;
using DriverHire.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Services.Services
{
    public interface IUserRegistrationServices
    {
        public Task<IdentityUser> Save(IdentityUser entity);
    }

    public class UserRegistrationServices : IUserRegistrationServices
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IUserRegistrationRepository _UserRegistrationRepository;

        public UserRegistrationServices(IUnitofWork unitofWork, IUserRegistrationRepository userRegistrationRepository)
        {
            _unitofWork = unitofWork;
            _UserRegistrationRepository = userRegistrationRepository;
        }

        public async Task<IdentityUser> Save(IdentityUser entity)
        {
            var result = (await _UserRegistrationRepository.Insert(entity)).Entity;
            await _unitofWork.SaveAsync();
            return result;
        }
    }
}
