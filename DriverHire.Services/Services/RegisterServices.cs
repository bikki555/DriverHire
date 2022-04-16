using DriverHire.Entity;
using DriverHire.Entity.Dto;
using DriverHire.Repository;
using DriverHire.Repository.Interfaces;
using DriverHire.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Services
{
    public interface IRegisterServices
    {
        public Task<bool> Register(RegisterDto dto);
    }
    public class RegisterServices : IRegisterServices
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IRegisterRepository _registerRepository;
        private readonly IEmailServices _emailServices;
        private readonly IRandomNumberGeneratorServices _otpGenerator;

        public RegisterServices(IUnitofWork unitofWork,IRegisterRepository registerRepository,IEmailServices emailServices,IRandomNumberGeneratorServices otpGenerator)
        {
            _unitofWork = unitofWork;
            _registerRepository = registerRepository;
            _emailServices = emailServices;
            _otpGenerator = otpGenerator;
        }
        public async Task<bool> Register(RegisterDto dto)
        {

            try
            {
                //sending email//
                var to = dto.Email;
                var otp =  _otpGenerator.GetRandomNumber(4);
                var otpExiryDate = DateTime.Now.AddMinutes(10);//manually adding 10 min for otp expiry

                //this can be store in email template in table//
                var subject = "New Registration Otp";
                var message = "<p>Welcome user," +
                    "</p><p> Use the following Otp </p>" +
                    $"<p> Otp:{otp}</p>" +
                    $"<p> Opt ExpiryDate: {otpExiryDate}</p>" +
                    "<p> &nbsp;</p><p> &nbsp;</p>" +
                    "<p> Regards,</p><p> Driver Hire </p> ";
                //
                var email = await _emailServices.SendEmail(to, subject, message);
                if(email)
                {
                    var entity = new Register
                    {
                        Email = dto.Email,
                        Otp = otp,
                        OtpExpiryDate = otpExiryDate
                    };
                    await _registerRepository.Insert(entity);
                    await _unitofWork.SaveAsync();
                    return true;
                }
                else
                return false;
            }
         
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
