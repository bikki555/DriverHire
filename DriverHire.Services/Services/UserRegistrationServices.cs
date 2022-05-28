using DriverHire.Entity.Dto;
using DriverHire.Entity.Entity;
using DriverHire.Entity.Enums;
using DriverHire.Repository;
using DriverHire.Repository.Interfaces;
using DriverHire.Services.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DriverHire.Services.Services
{
    public interface IUserRegistrationServices
    {
        public Task<UserRegistrationDto> Save(UserRegistrationDto dto);
        Task<ModelStateDictionary> Validation(UserRegistrationDto dto);
        Task<(Authtoken, UserSignInResult)> CheckLogin(LoginDto dto);
        public Task<IEnumerable<UserDetailsDto>> Get(int? id);
        public Task<ApplicationUser> GetLoggedInUser();
        public Task<ExpandoObject> ResetPassword(ResetPasswordDto dto);
    }

    public class UserRegistrationServices : IUserRegistrationServices
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IUserRegistrationRepository _UserRegistrationRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IRegisterRepository _registerRepository;
        private readonly JwtGenerator _jwtGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRegistrationServices(IUnitofWork unitofWork,
                                        IUserRegistrationRepository userRegistrationRepository,
                                        UserManager<IdentityUser> userManager,
                                        IRegisterRepository registerRepository,
                                        JwtGenerator jwtGenerator,
                                        IHttpContextAccessor httpContextAccessor)
        {
            _unitofWork = unitofWork;
            _UserRegistrationRepository = userRegistrationRepository;
            _userManager = userManager;
            _registerRepository = registerRepository;
            _jwtGenerator = jwtGenerator;
            _httpContextAccessor = httpContextAccessor;
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
            var register = (await _registerRepository.SelectWhere(x => x.Email == dto.Email && x.IsReset!=true)).OrderByDescending(x => x.OtpExpiryDate).FirstOrDefault();
            if (string.Equals(register.Otp, dto.Otp))
            {
                if (register.OtpExpiryDate < DateTime.Now)
                    modelState.AddModelError(nameof(dto.Otp), "Otp Already Expired! Please Re send Otp");
            }
            else
                modelState.AddModelError(nameof(dto.Otp), "Otp does not Match");
            return modelState;
        }

        public async Task<(Authtoken, UserSignInResult)> CheckLogin(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.Email);
            var isLoginValid = await _userManager.CheckPasswordAsync(user, dto.Password);
            var applicationUser = (await _UserRegistrationRepository.SelectWhere(x => x.UserId == user.Id)).FirstOrDefault();
            if (!isLoginValid)
                return (null, UserSignInResult.LoginInvalid);
            var token = await _jwtGenerator.GenerateJwtTokenAsync(user);
            // await SaveTokens(applicationUser, null);
            applicationUser.IsActive = true;
            _UserRegistrationRepository.Update(applicationUser);
            await _unitofWork.SaveAsync();
            return (new Authtoken
            {
                AccessToken = token.token,
                RefreshToken = null,
                IsCustomer = applicationUser.IsCustomer,
                UserName = user.UserName,
                Role = token.role
            }, UserSignInResult.Success);
        }


        public async Task SaveTokens(ApplicationUser user, string refreshToken)
        {
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryDate = null;
            _UserRegistrationRepository.Update(user);
            await _unitofWork.SaveAsync();
        }
        public async Task<IEnumerable<UserDetailsDto>> Get(int? id)
        {
            var userDetails = _userManager.Users.ToList();
            var applicationUsers = await _UserRegistrationRepository.GetAll();
            var result = userDetails.Select(x => new UserDetailsDto
            {
                Id = applicationUsers.Where(au => au.UserId == x.Id).FirstOrDefault().Id,
                UserName = x.UserName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber
            }).Where(r => !id.HasValue || r.Id == id);
            return result;
        }

        public async Task<ApplicationUser> GetLoggedInUser()
        {
            var applicationUser = (await _UserRegistrationRepository.SelectWhere(u => u.UserId == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))).FirstOrDefault();
            if (applicationUser is null)
                return null;
            return applicationUser;
        }
        public async Task<ExpandoObject> ResetPassword(ResetPasswordDto dto)
        {
            dynamic obj = new ExpandoObject();
            var modelState = new ModelStateDictionary();
            var email = (await _registerRepository.SelectWhere(x => x.Otp == dto.Otp && x.IsReset == true)).FirstOrDefault()?.Email;
            if (email is null)
                //otp doesnot match//
                modelState.AddModelError("", "Otp does not match");
            else
            {
                var applicationUser = await _userManager.FindByNameAsync(email);
                if (applicationUser == null)
                {
                    modelState.AddModelError("", $"No user exist having email {email}");
                }
                var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(applicationUser);
                var changePasword = await _userManager.ResetPasswordAsync(applicationUser, resetPasswordToken, dto.Password);
                if (changePasword.Succeeded)
                    obj.Data = true;
                else
                    modelState.AddModelError("","Erro while Resetting Password");
            }
            obj.ModelState = modelState;
            return obj;
        }
    }
}
