using Microsoft.AspNetCore.Mvc;
using SignalRIntegrationTesting.Dispatchers;
using SignalRIntegrationTesting.Models;
using System.Threading.Tasks;

namespace SignalRIntegrationTesting.Controllers
{
    [Route("[controller]")]
    public class HubController : Controller
    {
        private readonly ITestHubDispatcher _dispatcher;

        public HubController(ITestHubDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost("test")]
        public async Task<IActionResult> Test([FromBody] Notification notification)
        {
            await _dispatcher.Dispatch(notification);
            return Ok();
        }
    }
}
