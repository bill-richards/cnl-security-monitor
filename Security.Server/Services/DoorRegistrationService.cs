using System;
using System.Linq;
using Security.Common;
using Security.Common.Exchanges;
using Security.Data;
using Security.Models;

namespace Security.Server.Services
{
    public class DoorRegistrationService : IDoorRegistrationService
    {
        private readonly Func<DoorContext> _createDoorContext;
        private readonly Func<IDoor> _createNewDoor;
        private readonly IMessageWriterService _messageWriterService;
        private readonly IDoorInformationMessageFactory _informationMessageFactory;
        private readonly Func<IDoorEvent> _createEvent;

        public DoorRegistrationService(Func<DoorContext> doorContextFactory, Func<IDoor> doorFactory, 
            Func<IDoorEvent> doorEventFactory, IMessageWriterService messageWriterService,
            IDoorInformationMessageFactory informationMessageFactory)
        {
            _createDoorContext = doorContextFactory;
            _createNewDoor = doorFactory;
            _messageWriterService = messageWriterService;
            _messageWriterService.SetTheRoutingKey(RoutingKeys.DoorRegisterRoutingKey);
            _informationMessageFactory = informationMessageFactory;
            _createEvent = doorEventFactory;
        }

        public void RegisterNewDoor(string label, DoorStates initialState)
        {
            var door = _createNewDoor().WhoseLabelIs(label).And.SetStateTo(initialState);
            using var context = _createDoorContext();
            context.Doors.Add((Door) door);
            context.SaveChanges();

            door = context.Doors.First(d => d.Label == label);
            var informationMessage = _informationMessageFactory.CreateDoorInformationMessage(door.SetMonitorId("new"));
            _messageWriterService.SendMessage(informationMessage);

            var currentEvent = _createEvent().ForDoorWithId(door.Id)
                .And.SetTheEventDescriptionTo(DoorStates.DoorRegistered);
            context.Events.Add((DoorEvent) currentEvent);
            context.SaveChanges();
        }
    }
}