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
        public async Task<IActionResult> Save(DriverForm entity)
        {
            var result = await _DriverFormServices.Save(entity);
            return Ok(result);


        }
    }
}
