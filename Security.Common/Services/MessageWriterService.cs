using System;
using System.Text;
using RabbitMQ.Client;
using Security.Common.Exchanges;

namespace Security.Common.Services
{
    public class MessageWriterService : ExchangeCommunicationAgent, IMessageWriterService
    {
        private readonly IInformationRequestMessage _informationRequestMessage;
        public MessageWriterService(IInformationRequestMessage informationRequestMessage) 
            => _informationRequestMessage = informationRequestMessage;

        public void SendMessage(IDoorControlMessage message) 
            => SendTheMessage(message.AsJson());

        public void SendInformationRequestMessage(string monitorId)
        {
            SetTheRoutingKey($"{RoutingKeys.DoorInformationRoutingKey}");
            SendTheMessage(_informationRequestMessage.AsJson(monitorId));
        }

        public void SendMessage(IDoorInformationMessage message)
        {
            SetTheRoutingKey(RoutingKeys.DoorInformationRoutingKey);
            SendTheMessage(message.AsJson());
        }

        private void SendTheMessage(string message)
        {
            var factory = new ConnectionFactory
            {
                HostName = MqHost, 
                UserName = UsersName, 
                Password = Password
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange: ExchangeName, type: "topic");

            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: ExchangeName, routingKey: RoutingKey, basicProperties: null, body: body);
        }
    }
}