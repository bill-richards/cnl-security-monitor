using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Prism.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using Security.Common.Exchanges;
using Security.Common.Messages;
using Security.Models;

namespace Security.Common.Services
{
    public class MessageReaderService : ExchangeCommunicationAgent, IMessageReaderService
    {
        private readonly IDoorControlMessageFactory _controlMessageFactory;
        private readonly IEventAggregator _eventAggregator;
        public event Action<IDoorControlMessage> DoorControlMessageReceived;
        public event Action<IDoor> DoorInformationMessageReceived;
        public event Action<string> InformationRequestMessageReceived;

        public bool IsListening { get; private set; }

        public void StopListening()
            => IsListening = false;

        public MessageReaderService(IDoorControlMessageFactory controlMessageFactory, IEventAggregator eventAggregator)
        {
            _controlMessageFactory = controlMessageFactory;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<ApplicationClosingEvent>().Subscribe(StopListening);
        }

        public void Listen()
        {
            IsListening = true;
            Task.Factory.StartNew(() =>
            {
                var factory = new ConnectionFactory
                {
                    HostName = MqHost,
                    UserName = UsersName,
                    Password = Password
                };

                try
                {
                    using var connection = factory.CreateConnection();
                    using var channel = connection.CreateModel();
                    channel.ExchangeDeclare(exchange: ExchangeName, type: "topic");

                    var queueName = channel.QueueDeclare().QueueName;
                    channel.QueueBind(queue: queueName, exchange: ExchangeName, routingKey: RoutingKey);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += OnReceived;
                    while (IsListening)
                    {
                        channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
                    }
                    consumer.Received -= OnReceived;
                }
                catch (BrokerUnreachableException e)
                {
                    _eventAggregator.GetEvent<CommunicationErrorEvent>().Publish(e.Message);
                }

            });
        }

        private void OnReceived(object sender, BasicDeliverEventArgs basicDeliverEventArgs)
        {
            if (basicDeliverEventArgs.RoutingKey == RoutingKeys.DoorInformationRoutingKey)
                ReceiveDoorInformationMessage(in basicDeliverEventArgs);
            else
                ReceiveDoorControlMessage(in basicDeliverEventArgs);
        }

        private void ReceiveDoorInformationMessage(in BasicDeliverEventArgs basicDeliverEventArgs)
        {
            var json = Encoding.UTF8.GetString(basicDeliverEventArgs.Body);

            var message = JsonConvert.DeserializeObject<Door>(json);
            if (message.Id != 0)
                DoorInformationMessageReceived?.Invoke(message);
            else
            {
                var monitorId = JsonConvert.DeserializeObject<InformationRequestMessage>(json).MonitorId;
                InformationRequestMessageReceived?.Invoke(monitorId);
            }

        }

        private void ReceiveDoorControlMessage(in BasicDeliverEventArgs basicDeliverEventArgs)
        {
            var json = Encoding.UTF8.GetString(basicDeliverEventArgs.Body);
            var temporaryMessage = JsonConvert.DeserializeObject<DoorControlMessage>(json);

            var message = _controlMessageFactory.CreateDoorControlMessage(temporaryMessage.DoorId, temporaryMessage.DoorAction);
            DoorControlMessageReceived?.Invoke(message);
        }
    }
}