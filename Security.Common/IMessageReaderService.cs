using System;

namespace Security.Common
{
    public interface IMessageReaderService : IExchangeCommunicationAgent
    {
        event Action<IDoorControlMessage> DoorControlMessageReceived;
        event Action<IDoor> DoorInformationMessageReceived;
        event Action<string> InformationRequestMessageReceived;

        void StopListening();
        void Listen();
        bool IsListening { get; }
    }
}