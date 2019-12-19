using System;
using System.Threading;
using System.Windows.Threading;
using Prism.Mvvm;
using Security.Common;
using Security.Common.Exchanges;
using Security.Server.Services;

namespace Security.Server.ViewModels
{
    public class DoorsViewModel : BindableBase
    {
        private readonly IDoorViewCreationService _doorViewCreationService;
        private readonly IDoorInformationBroadcastService _doorInformationBroadcastService;
        private readonly Thread _uiThread;

        public DoorsViewModel(IMessageReaderService messageReaderService, IDoorViewCreationService doorViewCreationService, IDoorInformationBroadcastService doorInformationBroadcastService)
        {
            _uiThread = Thread.CurrentThread;
            _doorViewCreationService = doorViewCreationService;
            _doorInformationBroadcastService = doorInformationBroadcastService;
            messageReaderService.SetTheRoutingKey(RoutingKeys.DoorInformationRoutingKey);
            messageReaderService.DoorInformationMessageReceived += OnInformationMessageReceived;
            messageReaderService.InformationRequestMessageReceived += OnInformationRequestMessageReceived;
            messageReaderService.Listen();
        }

        private void OnInformationRequestMessageReceived(string monitorId)
            => _doorInformationBroadcastService.BroadcastAllDoorsInformation(monitorId);

        private void OnInformationMessageReceived(IDoor door)
        {
            if (!string.IsNullOrWhiteSpace(door.MonitorId) && !door.MonitorId.Equals("new")) return;

            Dispatcher.FromThread(_uiThread)
                ?.InvokeAsync(() => _doorViewCreationService.CreateDoorView(door),
                    DispatcherPriority.DataBind);
        }

        public void InitializeModels() 
            => _doorInformationBroadcastService.BroadcastAllDoorsInformation();
    }
}