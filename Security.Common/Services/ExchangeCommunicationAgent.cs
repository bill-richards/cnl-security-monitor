using Security.Common.Exchanges;

namespace Security.Common.Services
{
    public abstract class ExchangeCommunicationAgent : IExchangeCommunicationAgent
    {
        public string ExchangeName { get; private set; } = "security-monitor";
        public string RoutingKey { get; private set; } = RoutingKeys.AllDoorMessagesRoutingKey;
        public string MqHost { get; private set; } = "localhost";
        public string UsersName { get; private set; } = "cnl_user";
        public string Password { get; private set; } = "cnl_Password1";

        public void SetTheRoutingKey(string key)
            => RoutingKey = key;

        public void SetTheExchangeName(string exchange)
            => ExchangeName = exchange;

        public void SetTheMqHost(string host)
            => MqHost = host;

        public void SetTheUserName(string username)
            => UsersName = username;

        public void SetThePassword(string password)
            => Password = password;
    }
}