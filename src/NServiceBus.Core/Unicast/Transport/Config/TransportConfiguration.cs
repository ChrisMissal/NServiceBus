namespace NServiceBus
{
    using System;
    using Unicast.Transport;

    /// <summary>
    /// Configuration options common for all transports
    /// </summary>
    public class TransportConfiguration
    {
        /// <summary>
        /// Access to the current config instance
        /// </summary>
        public Configure Config { get; private set; }

        internal TransportConfiguration(Configure config)
        {
            Config = config;
        }

        /// <summary>
        /// Configures the transport to use the given string as the connection string
        /// </summary>
        public void ConnectionString(string connectionString)
        {
            Config.Settings.Set<TransportConnectionString>(new TransportConnectionString(()=>connectionString));
        }

        /// <summary>
        /// Configures the transport to use the connection string with the given name
        /// </summary>
        public void ConnectionStringName(string name)
        {
            Config.Settings.Set<TransportConnectionString>(new TransportConnectionString(name));
        }

        /// <summary>
        /// Configures the transport to use the given func as the connection string
        /// </summary>
        public void ConnectionString(Func<string> connectionString)
        {
            Config.Settings.Set<TransportConnectionString>(new TransportConnectionString(connectionString));
        }
    }
}