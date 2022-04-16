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

    public class DriverFormController : Controller
    {

        private readonly IDriverFormServices _DriverFormServices;
        public DriverFormController(IDriverFormServices DriverFormServices)
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
    }
}
