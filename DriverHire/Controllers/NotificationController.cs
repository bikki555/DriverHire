using DriverHire.Entity.NotificationModels;
using DriverHire.Services.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DriverHire.Api.Controllers
{
    [Route("api/[controller]")]
    public class NotificationController : Controller
    {
        private readonly IPushNotificationGenerator _pushNotificationGenerator;
        public NotificationController(IPushNotificationGenerator pushNotificationGenerator)
        {
            _pushNotificationGenerator = pushNotificationGenerator;
        }

        [HttpPost]
        [Route("Send")]
        public async Task<IActionResult> SendNotification([FromBody]NotificationModel notificationModel)
        {
            var result = await _pushNotificationGenerator.SendNotification(notificationModel);
            return Ok(result);
        }
    }
}
