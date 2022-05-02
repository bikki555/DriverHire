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
    [Authorize]
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
        public async Task<IActionResult> Save(ClientBookingDto dto)
        {
            var result = await _bookingServices.Save(dto);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> Get(int bookingId)
        {
            var result = await _bookingServices.BookingDetails(bookingId);
            return Ok(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> History(int?userId)
        {
            var result = await _bookingServices.BookingHistoryDetails(userId);
            return Ok(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> DriverAccept(int bookingId)
        {
            var result = await _bookingServices.BookingAcceptDriver(bookingId);
            return Ok(result);
        }

    }
}
