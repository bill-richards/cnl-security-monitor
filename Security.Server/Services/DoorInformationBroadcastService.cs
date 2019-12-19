using System;
using System.Threading.Tasks;
using Security.Common;
using Security.Data;

namespace Security.Server.Services
{
    public class DoorInformationBroadcastService : IDoorInformationBroadcastService
    {
        private readonly Func<DoorContext> _doorContextFactory;
        private readonly IDoorInformationMessageFactory _informationMessageFactory;
        private readonly IMessageWriterService _messageWriterService;

        public DoorInformationBroadcastService(Func<DoorContext> doorContextFactory, IDoorInformationMessageFactory informationMessageFactory, IMessageWriterService messageWriterService)
        {
            _doorContextFactory = doorContextFactory;
            _informationMessageFactory = informationMessageFactory;
            _messageWriterService = messageWriterService;
        }

        public void BroadcastAllDoorsInformation()
        {
            using var context = _doorContextFactory();
            foreach (var door in context.Doors)
            {
                var informationMessage = _informationMessageFactory.CreateDoorInformationMessage(door);
                Task.Factory.StartNew(() => _messageWriterService.SendMessage(informationMessage));
            }
        }
        public void BroadcastAllDoorsInformation(string monitorId)
        {
            using var context = _doorContextFactory();
            foreach (var door in context.Doors)
            {
                var informationMessage = _informationMessageFactory.CreateDoorInformationMessage(door.SetMonitorId(monitorId));
                Task.Factory.StartNew(() => _messageWriterService.SendMessage(informationMessage));
            }
        }
    }
}