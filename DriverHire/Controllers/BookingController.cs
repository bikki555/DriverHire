using DriverHire.Entity.Dto;
using DriverHire.Entity.Entity;
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

    public class BookingController : Controller
    {

        private readonly IBookingServices _bookingServices;

        public BookingController(IBookingServices bookingServices)
        {
            _bookingServices = bookingServices;
        }
        [HttpPost]
        public async Task<IActionResult> Save(Booking entity)
        {
            var result = await _bookingServices.Save(entity);
            return Ok(result);


        }
    }
}
