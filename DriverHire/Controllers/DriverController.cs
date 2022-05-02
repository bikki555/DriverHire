using DriverHire.Entity.Dto;
using DriverHire.Entity.Entity;
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
    [Authorize]

    public class DriverController : Controller
    {

        private readonly IDriverFormServices _DriverFormServices;
        public DriverController(IDriverFormServices DriverFormServices)
        {
            _DriverFormServices = DriverFormServices;
        }
        [HttpPost]
        public async Task<IActionResult> Save(DriverFormPostDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _DriverFormServices.Save(dto);
            return Ok(result);


        }
        [HttpGet]
        public async Task<IActionResult> Get(int? driverId)
        {
            var result = await _DriverFormServices.Get(driverId);
            return Ok(!driverId.HasValue?result:result.FirstOrDefault());
        }
        [HttpGet]
        [Route("Recommendation")]
        public async Task<IActionResult> Recommendation(int bookingId)
        {
            var result = await _DriverFormServices.Recommendation(bookingId);
            return Ok(result);
        }
    }
}
