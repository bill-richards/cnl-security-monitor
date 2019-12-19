using System.Windows;
using Prism.Ioc;
using Prism.Unity;
using Security.Common;
using Security.Common.Messages;
using Security.Common.Services;
using Security.Data;
using Security.Models;
using Security.Server.Services;
using Unity;

namespace Security.Server
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
            containerRegistry.Register<IDoorRegistrationService, DoorRegistrationService>();
            containerRegistry.Register<IDoorViewCreationService, DoorViewCreationService>();
            containerRegistry.Register<IDoorInformationBroadcastService, DoorInformationBroadcastService>();
            containerRegistry.RegisterSingleton<IDoorStateService, DoorStateService>();

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

            // start up the services that run in the background
            containerRegistry.GetContainer().Resolve<IDoorStateService>();
        }
    }
}
