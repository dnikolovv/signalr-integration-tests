using SignalRIntegrationTesting.Models;
using System.Threading.Tasks;

namespace SignalRIntegrationTesting.Dispatchers
{
    public interface ITestHubDispatcher
    {
        Task Dispatch(Notification notification);
    }
}
