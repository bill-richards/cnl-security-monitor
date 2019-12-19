using System.Windows;
using Prism.Ioc;
using Security.Common;
using Security.Common.Messages;
using Security.Common.Services;
using Security.Data;
using Security.Models;

namespace Security.Monitor
{
    public partial class App 
    {
        protected override Window CreateShell() 
            => Container.Resolve<Views.MainWindow>();

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Services
            containerRegistry.Register<IMessageReaderService, MessageReaderService>();
            containerRegistry.Register<IMessageWriterService, MessageWriterService>();
            containerRegistry.Register<IDoorViewCreationService, DoorViewCreationService>();

            // Factories
            containerRegistry.RegisterSingleton<IDoorControlMessageFactory, DoorControlMessage>();
            containerRegistry.RegisterSingleton<IDoorInformationMessageFactory, DoorInformationMessage>();
            containerRegistry.RegisterSingleton<IInformationRequestMessage, InformationRequestMessage>();

            // Models
            containerRegistry.Register<Door>();
            containerRegistry.Register<IDoorEvent, DoorEvent>();
            containerRegistry.Register<IDoor, Door>();

            // Data Contexts
            containerRegistry.Register<DoorContext>();

            // Utilities
            containerRegistry.Register<IJsonSerializer, JsonSerializer>();
        }
    }
}