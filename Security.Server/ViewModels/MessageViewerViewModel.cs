using Prism.Mvvm;
using Security.Common;

namespace Security.Server.ViewModels
{
    public class MessageViewerViewModel : BindableBase
    {
        private readonly IDoorInformationMessageFactory _informationMessageFactory;
        private string _messageText;

        public MessageViewerViewModel(IMessageReaderService messageReaderService, IDoorInformationMessageFactory informationMessageFactory)
        {
            _informationMessageFactory = informationMessageFactory;
            messageReaderService.DoorControlMessageReceived += OnControlMessageReceived;
            messageReaderService.DoorInformationMessageReceived += OnInformationMessageReceived;
            messageReaderService.Listen();
        }

        private void OnInformationMessageReceived(IDoor door)
        {
            var message = _informationMessageFactory.CreateDoorInformationMessage(door);
            AppendMessage(message.AsJson());
        }

        private void OnControlMessageReceived(IDoorControlMessage message) 
            => AppendMessage(message.AsJson());

        private void AppendMessage(string message)
        {
            MessageText += string.IsNullOrWhiteSpace(MessageText)
                ? message : $"\r{message}";
        }

        public string MessageText
        {
            get => _messageText;
            private set
            {
                _messageText = value;
                RaisePropertyChanged(nameof(MessageText));
            }
        }
    }
}