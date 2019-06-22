using Microsoft.AspNetCore.SignalR.Client;
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SignalRIntegrationTesting.Tests
{
    public class TestHubConnection<TEvent>
    {
        private readonly HubConnection _connection;
        private readonly string _expectedEventToReceive;
        private readonly Mock<Action<TEvent>> _handlerMock;
        private readonly int _verificationTimeout;

        internal TestHubConnection(string hubUrl, string expectedEventToReceive, int verificationTimeout = 10000)
        {
            _handlerMock = new Mock<Action<TEvent>>();
            _expectedEventToReceive = expectedEventToReceive;
            _verificationTimeout = verificationTimeout;
            _connection = new HubConnectionBuilder()
                .WithUrl(hubUrl)
                .Build();
        }

        public async Task StartAsync()
        {
            await _connection.StartAsync();
            _connection.On(_expectedEventToReceive, _handlerMock.Object);
        }

        public Task VerifyMessageReceived(Expression<Func<TEvent, bool>> predicate, Times times) =>
            _handlerMock.VerifyWithTimeoutAsync(x => x(It.Is<TEvent>(predicate)), times, _verificationTimeout);
    }
}
