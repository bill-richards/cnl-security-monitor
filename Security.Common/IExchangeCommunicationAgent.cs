namespace Security.Common
{
    public interface IExchangeCommunicationAgent
    {
        /// <summary>
        /// Defaults to security-monitor
        /// </summary>
        string ExchangeName { get; }

        /// <summary>
        /// Defaults to cnl.#
        /// </summary>
        string RoutingKey { get; }

        /// <summary>
        /// Defaults to localhost
        /// </summary>
        string MqHost { get; }

        /// <summary>
        /// Defaults to cnl_user
        /// </summary>
        string UsersName { get; }

        /// <summary>
        /// Defaults to cnl_Password1
        /// </summary>
        string Password { get; }

        void SetTheRoutingKey(string key);
        void SetTheExchangeName(string exchange);
        void SetTheMqHost(string host);
        void SetTheUserName(string username);
        void SetThePassword(string password);
    }
}