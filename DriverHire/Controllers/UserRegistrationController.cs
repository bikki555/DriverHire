using DriverHire.Entity.Dto;
using DriverHire.Entity.Enums;
using DriverHire.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DriverHire.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserRegistrationController : Controller
    {
        private readonly IUserRegistrationServices _userRegistrationServices;

        public UserRegistrationController(IUserRegistrationServices userRegistrationServices)
        {
            _userRegistrationServices = userRegistrationServices;
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegistrationDto dto)
        {
            var checkValidation = await _userRegistrationServices.Validation(dto);
            ModelState.Merge(checkValidation);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
          
            var result = await _userRegistrationServices.Save(dto);
            return Ok(result);
          

        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get(int?id)
        {
            var result = await _userRegistrationServices.Get(id);
            return Ok(result);
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (ModelState.IsValid)
            {
                var (token, userSignInResult) = await _userRegistrationServices.CheckLogin(dto);
                if (userSignInResult == UserSignInResult.Success)
                    return Ok(token);
                else
                    ModelState.AddModelError(nameof(dto.Email), "Invalid login credentials");
            }
            return BadRequest(ModelState);
        }
    }
}
