using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Security.Common;
using Security.Server.Services;

namespace Security.Server.ViewModels
{
    public class DoorRegistrationViewModel : BindableBase
    {
        private readonly IDoorRegistrationService _registrationService;
        private string _doorLabel;
        private DoorStates _doorState;

        public DoorRegistrationViewModel(IDoorRegistrationService registrationService)
        {
            _registrationService = registrationService;
            RegisterDoorCommand = new DelegateCommand(RegisterNewDoor, () => CanRegisterNewDoor).ObservesCanExecute(() => CanRegisterNewDoor);
            DoorState = DoorStates.DoorClosed;
        }

        public bool CanRegisterNewDoor
            => !string.IsNullOrWhiteSpace(DoorLabel);

        private void RegisterNewDoor()
        {
            var state = DoorState;
            var label = DoorLabel;
            Task.Factory.StartNew(() => _registrationService.RegisterNewDoor(label, state));
            DoorLabel = string.Empty;
        }

        public string DoorLabel
        {
            get => _doorLabel;
            set
            {
                SetProperty(ref _doorLabel, value);
                RaisePropertyChanged(nameof(CanRegisterNewDoor));
            }
        }

        public ICommand RegisterDoorCommand { get; }

        public DoorStates DoorState 
        {
            get => _doorState;
            set
            {
                SetProperty(ref _doorState, value);
                RaisePropertyChanged(nameof(DoorState));
            }
        }

    }
}