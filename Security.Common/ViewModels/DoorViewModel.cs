using System.Threading.Tasks;
using Prism.Mvvm;
using Security.Common.Exchanges;

namespace Security.Common.ViewModels
{
    public class DoorViewModel : BindableBase
    {
        private readonly IMessageReaderService _messageReaderService;
        private readonly IMessageWriterService _messageWriterService;
        private readonly IDoorControlMessageFactory _messageFactory;
        private string _doorId;
        private string _doorLabel;
        private DoorStates _doorState;

        public DoorViewModel(IMessageReaderService messageReaderService, 
                             IMessageWriterService messageWriterService, 
                             IDoorControlMessageFactory messageFactory)
        {
            _messageReaderService = messageReaderService;
            _messageWriterService = messageWriterService;
            _messageFactory = messageFactory;
        }

        private void OnControlMessageReceived(IDoorControlMessage message)
        {
            Updating();
            State = message.DoorAction;
            FinishedUpdating();
        }

        private void Updating() => IsUpdating = true;
        private void FinishedUpdating() => IsUpdating = false;

        private bool IsUpdating { get; set; } = false;

        public void SetDoorModel(IDoor door)
        {
            Updating();
            DoorId = $"{door.Id}";
            DoorLabel = door.Label;
            State = door.State;
            FinishedUpdating();

            var routingKey = $"{RoutingKeys.SpecificDoorRoutingKey}{door.Id}";
            _messageWriterService.SetTheRoutingKey(routingKey);
            _messageReaderService.SetTheRoutingKey(routingKey);
            _messageReaderService.DoorControlMessageReceived += OnControlMessageReceived;
            _messageReaderService.Listen();
        }

        private void SendTheMessage(int doorId, DoorStates state)
        {
            Task.Factory.StartNew(() =>
            {
                var message = _messageFactory.CreateDoorControlMessage(doorId, state);
                _messageWriterService?.SendMessage(message);
            });
        }

        public string DoorId
        {
            get => _doorId;
            set => SetProperty(ref _doorId, value);
        }
        public string DoorLabel
        {
            get => _doorLabel;
            set => SetProperty(ref _doorLabel, value);
        }

        public DoorStates State
        {
            get => _doorState;
            set
            {
                if (_doorState == value) return;
                SetProperty(ref _doorState, value);
                RaisePropertyChanged(nameof(State));

                if(!IsUpdating)
                    SendTheMessage(int.Parse(DoorId), State);
            }
        }
    }
}