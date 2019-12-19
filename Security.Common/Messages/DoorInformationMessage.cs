using System;
using Security.Models;

namespace Security.Common.Messages
{
    public class DoorInformationMessage : IDoorInformationMessage, IDoorInformationMessageFactory
    {
        private readonly IJsonSerializer _serializer;
        private readonly Func<Door, IJsonSerializer, IDoorInformationMessage> _factoryMethod;

        public DoorInformationMessage(IJsonSerializer serializer)
        {
            _serializer = serializer;
            _factoryMethod = (door, jsonSerializer) => new DoorInformationMessage(door, jsonSerializer);
        }

        private DoorInformationMessage(Door door, IJsonSerializer serializer)
        {
            _serializer = serializer;
            Door = door;
        }

        public Door Door { get; }
        public string MonitorId { get; private set; } = string.Empty;

        public IDoorInformationMessage CreateDoorInformationMessage(IDoor door)
            => _factoryMethod?.Invoke((Door)door, _serializer);


        public string AsJson()
            => _serializer.ConvertObjectToJson(Door);

    }
}