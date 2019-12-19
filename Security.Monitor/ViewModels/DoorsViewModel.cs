using System;
using System.Threading;
using System.Windows.Threading;
using Prism.Mvvm;
using Security.Common;
using Security.Common.Exchanges;

namespace Security.Monitor.ViewModels
{
    public class DoorsViewModel : BindableBase
    {
        private readonly IMessageWriterService _messageWriterService;
        private readonly IDoorViewCreationService _doorViewCreationService;
        private readonly Thread _uiThread;
        private readonly string _monitorId = Guid.NewGuid().ToString();

        public DoorsViewModel(IMessageReaderService messageReaderService, IMessageWriterService messageWriterService, IDoorViewCreationService doorViewCreationService)
        {
            _uiThread = Thread.CurrentThread;
            _messageWriterService = messageWriterService;
            _doorViewCreationService = doorViewCreationService;
            messageReaderService.SetTheRoutingKey(RoutingKeys.DoorInformationRoutingKey);
            messageReaderService.DoorInformationMessageReceived += OnInformationMessageReceived;
            messageReaderService.Listen();
        }

        private void OnInformationMessageReceived(IDoor door)
        {
            if (!door.MonitorId.Equals(_monitorId) && !door.MonitorId.Equals("new")) return;

            Dispatcher.FromThread(_uiThread)
                ?.InvokeAsync(() => _doorViewCreationService.CreateDoorView(door),
                    DispatcherPriority.DataBind);
        }

        public void InitializeModels() 
            => _messageWriterService.SendInformationRequestMessage(_monitorId);
    }
}