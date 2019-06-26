using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;

namespace SignalRIntegrationTesting.Tests
{
    public class TestHubConnectionBuilder
    {
        private List<(Type Type, string Name)> _expectedEventNames;
        private string _hubUrl;

        public TestHubConnection Build()
        {
            if (string.IsNullOrEmpty(_hubUrl))
                throw new InvalidOperationException($"Use {nameof(OnHub)} to set the hub url.");

            if (_expectedEventNames == null || _expectedEventNames.Count == 0)
                throw new InvalidOperationException($"Use {nameof(WithExpectedEvent)} to set the expected event name.");

            var hubConnection = new HubConnectionBuilder()
                .WithUrl(_hubUrl)
                .Build();

            var testConnection = new TestHubConnection(_hubUrl);

            foreach (var expected in _expectedEventNames)
            {
                testConnection.Expect(expected.Name, expected.Type);
            }

            Clear();

            return testConnection;
        }

        public TestHubConnectionBuilder OnHub(string hubUrl)
        {
            _hubUrl = hubUrl;
            return this;
        }

        public TestHubConnectionBuilder WithExpectedEvent<TEvent>(string eventName)
        {
            if (_expectedEventNames == null)
                _expectedEventNames = new List<(Type, string)>();

            _expectedEventNames.Add((typeof(TEvent), eventName));
            return this;
        }

        private void Clear()
        {
            _expectedEventNames = null;
            _hubUrl = null;
        }
    }
}
