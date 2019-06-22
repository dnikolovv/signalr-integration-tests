using System;

namespace SignalRIntegrationTesting.Tests
{
    public class TestHubConnectionBuilder
    {
        private string _eventName;
        private string _hubUrl;

        public TestHubConnection<TEvent> Build<TEvent>()
        {
            if (string.IsNullOrEmpty(_hubUrl))
                throw new InvalidOperationException($"Use {nameof(OnHub)} to set the hub url.");

            if (string.IsNullOrEmpty(_eventName))
                throw new InvalidOperationException($"Use {nameof(WithExpectedMessage)} to set the expected event name.");

            var testConnection = new TestHubConnection<TEvent>(_hubUrl, _eventName);

            Clear();

            return testConnection;
        }

        public TestHubConnectionBuilder OnHub(string hubUrl)
        {
            _hubUrl = hubUrl;
            return this;
        }

        public TestHubConnectionBuilder WithExpectedMessage(string eventName)
        {
            _eventName = eventName;
            return this;
        }

        private void Clear()
        {
            _eventName = null;
            _hubUrl = null;
        }
    }
}
