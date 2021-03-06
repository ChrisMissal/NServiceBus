﻿namespace NServiceBus.Persistence
{
    using System;
    using System.Linq;
    using System.Windows.Forms;
    using Logging;

    class EnableSelectedPersistences:IWantToRunBeforeConfigurationIsFinalized
    {
        public void Run(Configure config)
        {
            DefaultToInMemoryIfNeeded(config);

            foreach (var selectedPersistence in config.Settings.Get<EnabledPersistences>().GetEnabled())
            {
                var definitionType = selectedPersistence.PersitenceType;

                var type = config.TypesToScan.SingleOrDefault(t => typeof(IConfigurePersistence<>).MakeGenericType(definitionType).IsAssignableFrom(t));

                if (type == null)
                {
                    throw new InvalidOperationException("We couldn't find a IConfigurePersistence implementation for your selected persistence: " + definitionType.Name);
                }

                Logger.InfoFormat("Activating persistence {0} to provide {1} storage(s)", definitionType.Name, string.Join(",", selectedPersistence.StoragesToEnable));

                ((IConfigurePersistence)Activator.CreateInstance(type)).Enable(config, selectedPersistence.StoragesToEnable);
            }

          
        }

        static void DefaultToInMemoryIfNeeded(Configure config)
        {
            if (!config.Settings.HasSetting<EnabledPersistences>())
            {
                if (SystemInformation.UserInteractive)
                {
                    const string warningMessage = "No persistence has been selected, NServiceBus will now use InMemory persistence. We recommend that you change the persistence before deploying to production. To do this,  please add a call to config.UsePersistence<T>() where T can be any of the supported persistence options supported. http://docs.particular.net/nservicebus/persistence-in-nservicebus.";
                    Logger.Warn(warningMessage);
                }
                else
                {
                    const string errorMessage = "No persistence has been selected, please add a call to config.UsePersistence<T>() where T can be any of the supported persistence options supported. http://docs.particular.net/nservicebus/persistence-in-nservicebus";
                    throw new Exception(errorMessage);
                }

                config.UsePersistence<InMemory>();
            }
        }

        static ILog Logger = LogManager.GetLogger<EnableSelectedPersistences>();
    }
}