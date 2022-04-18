using DriverHire.Entity.Dto;
using DriverHire.Entity.Entity;
using DriverHire.Entity.Enums;
using DriverHire.Repository;
using DriverHire.Repository.Interfaces;
using DriverHire.Services.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Services.Services
{
    public interface IUserRegistrationServices
    {
        public Task<UserRegistrationDto> Save(UserRegistrationDto dto);
        Task<ModelStateDictionary> Validation(UserRegistrationDto dto);
        Task<(Authtoken, UserSignInResult)> CheckLogin(LoginDto dto);

    }

    public class UserRegistrationServices : IUserRegistrationServices
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IUserRegistrationRepository _UserRegistrationRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IRegisterRepository _registerRepository;
        private readonly JwtGenerator _jwtGenerator;

        public UserRegistrationServices(IUnitofWork unitofWork,
                                        IUserRegistrationRepository userRegistrationRepository,
                                        UserManager<IdentityUser> userManager,
                                        IRegisterRepository registerRepository,
                                        JwtGenerator jwtGenerator)
        {
            _unitofWork = unitofWork;
            _UserRegistrationRepository = userRegistrationRepository;
            _userManager = userManager;
            _registerRepository = registerRepository;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<UserRegistrationDto> Save(UserRegistrationDto dto)
        {
            var identityUser = new IdentityUser
            {
                UserName = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email
            };
            var identityResult = await _userManager.CreateAsync(identityUser, dto.Password);
            var applicationUser = new ApplicationUser
            {
                IsCustomer = dto.IsCustomer,
                UserId = identityUser.Id,
            };
            if (identityResult.Succeeded)
            {
                await _UserRegistrationRepository.Insert(applicationUser);
                await _unitofWork.SaveAsync();
            }
            else
                throw new Exception(string.Join("][", identityResult.Errors));

            return dto;
        }

        public async Task<ModelStateDictionary> Validation(UserRegistrationDto dto)
        {
            ModelStateDictionary modelState = new ModelStateDictionary();
            var register = (await _registerRepository.SelectWhere(x => x.Email == dto.Email)).OrderByDescending(x => x.OtpExpiryDate).FirstOrDefault();
            if (string.Equals(register.Otp, dto.Otp))
            {
                if (register.OtpExpiryDate < DateTime.Now)
                    modelState.AddModelError(dto.Otp, "Otp Already Expired! Please Re send Otp");
            }
            else
                modelState.AddModelError(dto.Otp, "Otp does not Match");
            return modelState;
        }

        public async Task<(Authtoken, UserSignInResult)> CheckLogin(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.Email);
            var isLoginValid = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (!isLoginValid)
                return (null, UserSignInResult.LoginInvalid);
            // var applicationUser = (await _UserRegistrationRepository.SelectWhere(a => a.UserId == user.Id)).FirstOrDefault();
            var token = isLoginValid ? await _jwtGenerator.GenerateJwtTokenAsync(user) : string.Empty;
            // await SaveTokens(applicationUser, null);
            return (new Authtoken
            {
                AccessToken = token,
                RefreshToken = null
            }, UserSignInResult.Success);
        }


        public async Task SaveTokens(ApplicationUser user, string refreshToken)
        {
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryDate = null;
            _UserRegistrationRepository.Update(user);
            await _unitofWork.SaveAsync();
        }


    }
}
