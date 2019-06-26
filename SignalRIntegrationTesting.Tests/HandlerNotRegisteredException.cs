using System;

namespace SignalRIntegrationTesting.Tests
{
    public class HandlerNotRegisteredException : Exception
    {
        public HandlerNotRegisteredException() : base()
        {
        }

        public HandlerNotRegisteredException(string message) : base(message)
        {
        }

        public HandlerNotRegisteredException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public HandlerNotRegisteredException(Type eventType)
            : base ($"Tried to verify that event of type {eventType.FullName} was received before registering it as expected. " +
                  $"Please use the {nameof(TestHubConnection.Expect)} method to register an event as expected before verifying it was called.")
        {
        }
    }
}
