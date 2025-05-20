using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PaymentService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly HttpClient _httpClient;

        public PaymentController(ILogger<PaymentController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpPost("process")]
        public async Task<IActionResult> ProcessPayment()
        {
            var traceId = Activity.Current?.TraceId.ToString() ?? "-";
            _logger.LogInformation("💸 Processando pagamento. TraceId: {TraceId}", traceId);

            var response = await _httpClient.PostAsync("http://localhost:5139/notification/send", null);

            _logger.LogInformation("Resposta da notificação: {StatusCode}", response.StatusCode);
            return Ok("Pagamento processado.");
        }
    }
}