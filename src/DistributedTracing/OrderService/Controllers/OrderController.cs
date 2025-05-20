using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace OrderService.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;
    private readonly HttpClient _httpClient;

    public OrderController(ILogger<OrderController> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClient = httpClientFactory.CreateClient();
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateOrder()
    {
        var activity = Activity.Current;
        _logger.LogInformation("Pedido recebido. TraceId: {TraceId}", activity?.TraceId);

        var response = await _httpClient.PostAsync("http://localhost:5216/payment/process", null);

        _logger.LogInformation("Pagamento solicitado. Status: {StatusCode}", response.StatusCode);
        
        return Ok("Pedido criado!");
    }
}
