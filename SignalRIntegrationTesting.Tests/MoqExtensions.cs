using Moq;
using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SignalRIntegrationTesting.Tests
{
    public static class MoqExtensions
    {
        public static async Task VerifyWithTimeoutAsync<T>(this Mock<T> mock, Expression<Action<T>> expression, Times times, int timeoutInMs)
            where T : class
        {
            bool hasBeenExecuted = false;
            bool hasTimedOut = false;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            while (!hasBeenExecuted && !hasTimedOut)
            {
                if (stopwatch.ElapsedMilliseconds > timeoutInMs)
                {
                    hasTimedOut = true;
                }

                mock.Verify(expression, times);
                hasBeenExecuted = true;

                // Feel free to make this configurable
                await Task.Delay(20);
            }

            mock.Verify(expression, times);
        }
    }
}
