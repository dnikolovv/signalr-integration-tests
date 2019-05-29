using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SignalRIntegrationTesting.Tests
{
    public class AppFixture
    {
        public const string BaseUrl = "http://localhost:54321";

        static AppFixture()
        {
            var webhost = WebHost
                .CreateDefaultBuilder(null)
                .UseStartup<Startup>()
                .UseUrls(BaseUrl)
                .Build();

            webhost.Start();
        }

        public async Task ExecuteHttpClientAsync(Func<HttpClient, Task> action)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(BaseUrl);

            using (httpClient)
            {
                await action(httpClient);
            }
        }

        public string GetCompleteServerUrl(string route)
        {
            route = route.TrimStart('/', '\\');
            return $"{BaseUrl}/{route}";
        }
    }
}
