using System;

namespace Security.Common.Messages
{
    public class DoorControlMessage : IDoorControlMessageFactory, IDoorControlMessage
    {
        private readonly IJsonSerializer _serializer;
        private readonly Func<int, DoorStates, IJsonSerializer, IDoorControlMessage> _factoryMethod;

        public IDoorControlMessage CreateDoorControlMessage(int doorId, DoorStates doorAction) 
            => _factoryMethod?.Invoke(doorId, doorAction, _serializer);

        public DoorControlMessage(IJsonSerializer serializer)
        {
            _serializer = serializer;
            _factoryMethod = (doorId, doorAction, jsonSerializer) =>
                new DoorControlMessage(doorId, doorAction, jsonSerializer);
        }

        private DoorControlMessage(int doorId, DoorStates doorAction, IJsonSerializer serializer)
        {
            _serializer = serializer;
            DoorId = doorId;
            DoorAction = doorAction;
        }

        public int DoorId { get; set; }
        public DoorStates DoorAction { get; set; }

        public string AsJson()
            => _serializer.ConvertObjectToJson(this);
    }
}