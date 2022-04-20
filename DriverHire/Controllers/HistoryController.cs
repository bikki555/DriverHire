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

    public class HistoryController : Controller
    {

        private readonly IBookingServices _bookingServices;
        public HistoryController(IBookingServices bookingServices)
        {
            _bookingServices = bookingServices;
        }
        [HttpPost]
        public async Task<IActionResult> Save(BookingDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _bookingServices.Save(dto);
            return Ok(result);


        }
        [HttpGet]
        [Route("Recommendation")]
        public async Task<IActionResult> Recommendation(int bookingId)
        {


            var result = await _bookingServices.Recommendation(bookingId);
            return Ok(result);


        }
    }
}
