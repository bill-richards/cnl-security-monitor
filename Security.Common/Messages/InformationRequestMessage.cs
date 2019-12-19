using System;

namespace Security.Common.Messages
{
    public class InformationRequestMessage : IInformationRequestMessage 
    {
        private readonly IJsonSerializer _serializer;

        public InformationRequestMessage(IJsonSerializer serializer) 
            => _serializer = serializer;

        public string MonitorId { get; set; }

        public string AsJson(string monitorId)
        {
            MonitorId = monitorId;
            return _serializer.ConvertObjectToJson(this);
        }
    }
}