using System;

namespace Security.Common
{
    public interface IMessageWriterService : IExchangeCommunicationAgent
    {
        void SendMessage(IDoorControlMessage message);
        void SendMessage(IDoorInformationMessage message);
        void SendInformationRequestMessage(string monitorId);
    }
}