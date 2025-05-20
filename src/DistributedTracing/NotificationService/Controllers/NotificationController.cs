using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace NoticationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly ILogger<NotificationController> _logger;

        public NotificationController(ILogger<NotificationController> logger)
        {
            _logger = logger;
        }

        [HttpPost("send")]
        public IActionResult SendNotification()
        {
            var traceId = Activity.Current?.TraceId.ToString() ?? "-";
            _logger.LogInformation("Enviando notificação. TraceId: {TraceId}", traceId);
            
            return Ok("Notificação enviada.");
        }
    }
}