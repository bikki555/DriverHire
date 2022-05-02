using DriverHire.Entity.Dto;
using DriverHire.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DriverHire.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class RegisterController : Controller
    {
        private readonly IRegisterServices _registerServices;

        public RegisterController(IRegisterServices registerServices)
        {
            _registerServices = registerServices;
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _registerServices.Register(dto);
            if (result)
                return Ok();
            else
                return BadRequest("Error Occured!");


        }
        [HttpPost("[action]")]
        public async Task<IActionResult> ResetOtp(RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _registerServices.ForgetPassword(dto);
            if (result)
                return Ok();
            else
                return BadRequest("Error Occured!");
        }
    }
}
