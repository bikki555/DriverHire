using DriverHire.Entity.Dto;
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
    public class FeedBackController : Controller
    {
        private readonly IFeedBackServices _feedBackServices;

        public FeedBackController(IFeedBackServices feedBackServices)
        {
            _feedBackServices = feedBackServices;
        }
        [HttpPost]
        public async Task<IActionResult> Save(FeedBackDto dto)
        {
            var result = await _feedBackServices.SaveDriverFeedBack(dto);
            return Ok(result);
        }
        [HttpGet("bookingId")]
        public async Task<IActionResult> Get(int bookingId)
        {
            var result = await _feedBackServices.GetDriverFeedBack(bookingId);
            return Ok(result);
        }
        [HttpGet("driverId")]
        public async Task<IActionResult> GetAll(int driverId)
        {
            var result = await _feedBackServices.GetDriverOverAllRating(driverId);
            return Ok(result);
        }
    }
}
