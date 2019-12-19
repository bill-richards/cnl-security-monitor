using System;
using System.Linq;
using Security.Common;
using Security.Data;
using Security.Models;

namespace Security.Server.Services
{
    public class DoorStateService : IDoorStateService
    {
        private readonly Func<DoorContext> _createDoorContext;
        private readonly Func<IDoorEvent> _createEvent;

        public DoorStateService(Func<DoorContext> doorContextFactory, Func<IDoorEvent> doorEventFactory, IMessageReaderService messageReaderService)
        {
            _createDoorContext = doorContextFactory;
            _createEvent = doorEventFactory;
            messageReaderService.DoorControlMessageReceived += OnControlMessageReceived;
            messageReaderService.Listen();
        }

        private void OnControlMessageReceived(IDoorControlMessage message) 
            => LogDoorStateEvent(message.DoorId, message.DoorAction);

        private void LogDoorStateEvent(int doorId, DoorStates state)
        {
            var currentEvent = _createEvent().ForDoorWithId(doorId);

            using var context = _createDoorContext();
            context.Events.Add((DoorEvent) currentEvent.SetTheEventDescriptionTo(state));
            context.Doors.First(d => d.Id == doorId).State = state;
            context.SaveChanges();
        }
    }
}