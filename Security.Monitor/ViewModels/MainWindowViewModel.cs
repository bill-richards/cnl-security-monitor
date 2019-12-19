using System.Diagnostics;
using System.Windows.Input;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Security.Common;

namespace Security.Monitor.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _applicationTitle = "Bill Richards' Technical Exercise - Security - MONITOR component";

        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            ApplicationClosing =
                new DelegateCommand(eventAggregator.GetEvent<ApplicationClosingEvent>().Publish, () => true);
            eventAggregator.GetEvent<CommunicationErrorEvent>().Subscribe(ShowErrorView);
        }

        private void ShowErrorView(string errorMessage)
        {
            // There was an error communicating with RabbitMQ
            // I didn't get around to finishing this bit off since I've probably done
            // more than you're looking for anyhow
            Debugger.Break();
        }

        public string Title
        {
            get => _applicationTitle;
            set => SetProperty(ref _applicationTitle, value);
        }

        public ICommand ApplicationClosing { get; }
        
    }
}