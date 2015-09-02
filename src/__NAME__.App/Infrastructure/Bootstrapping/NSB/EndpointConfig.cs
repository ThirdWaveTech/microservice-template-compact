using System;
using System.IO;
using NServiceBus;
using NServiceBus.Features;
using NServiceBus.Log4Net;
using StructureMap;
using StructureMap.Graph;
using __NAME__.Messages;

namespace __NAME__.App.Infrastructure.Bootstrapping.NSB
{
    [EndpointName("AllInOne.input")]
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, INeedInitialization
    {
        public void Customize(BusConfiguration config)
        {
            // When running as a service the current directory will be %System%
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            
            InitLogging();

            var container = new Container();

            InitServiceBus(config, container);

            InitStructureMap(container);
        }

        private void InitLogging()
        {
            // Configure Log4net using configuration section
            log4net.Config.XmlConfigurator.Configure();
        }

        private void InitServiceBus(BusConfiguration config, IContainer container)
        {
            // Configure Logging
            NServiceBus.Logging.LogManager.Use<Log4NetFactory>();

            // Configure container
            config.UseContainer<StructureMapBuilder>(c => c.ExistingContainer(container));

            // Xml serialization makes for easy to read messages.
            config.UseSerialization<JsonSerializer>();

            //Define conventions
            ConventionsBuilder conventions = config.Conventions();
            conventions.DefiningCommandsAs(MessageTypeConventions.EndsWith("Command"));
            conventions.DefiningEventsAs(MessageTypeConventions.EndsWith("Event"));
            conventions.DefiningMessagesAs(MessageTypeConventions.EndsWith("Message"));

            // Keep it simple by default
            config.DisableFeature<SecondLevelRetries>();
            config.DisableFeature<AutoSubscribe>();
            config.DisableFeature<TimeoutManager>();
            config.DisableFeature<DataBus>();

            // Configure for MSMQ
            config.UseTransport<AzureServiceBusTransport>();
            config.PurgeOnStartup(false);

            // Configure Saga Persistence
            config.UsePersistence<InMemoryPersistence>();
        }

        private void InitStructureMap(IContainer container)
        {
            container.Configure(c => c.Scan(s => {
                s.TheCallingAssembly(); 
                s.LookForRegistries();
            }));
        }
    }
}
