using Microsoft.AspNetCore.SignalR;
using SignalRIntegrationTesting.Hubs;
using SignalRIntegrationTesting.Models;
using System.Threading.Tasks;

namespace SignalRIntegrationTesting.Dispatchers
{
    public class TestHubDispatcher : ITestHubDispatcher
    {
        private readonly IHubContext<TestHub> _hubContext;

        public TestHubDispatcher(IHubContext<TestHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task Dispatch(Notification notification) =>
            _hubContext
                .Clients
                .All
                .SendAsync(nameof(Notification), notification);
    }
}
