using DriverHire.Entity.Dto;
using DriverHire.Services.Services;
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
    }
}
