using NServiceBus;
using NServiceBus.Features;
using StructureMap;
using __NAME__.Client.Diagnostics;
using __NAME__.Client.Examples;
using __NAME__.Messages;

namespace __NAME__.Client
{
    public static class ClientBootstrapper
    {
        public static void Bootstrap(IContainer container)
        {
            BootstrapApi(container);
            BootstrapBus(container);
        }

        public static void BootstrapApi(IContainer container)
        {
            container.Configure(c => {
                c.ForSingletonOf<IDiagnosticsClient>().Use(ApiClientFactory.GetClient<IDiagnosticsClient>());
                c.ForSingletonOf<IExampleClient>().Use(ApiClientFactory.GetClient<IExampleClient>());
            });
        }
        public static void BootstrapBus(IContainer container)
        {
            // Initialize container
            var configuration = new BusConfiguration();
            configuration.UseContainer<StructureMapBuilder>(c => c.ExistingContainer(container));

            // Define message conventions
            ConventionsBuilder conventions = configuration.Conventions();
            conventions.DefiningCommandsAs(MessageTypeConventions.EndsWith("Command"));
            conventions.DefiningEventsAs(MessageTypeConventions.EndsWith("Event"));
            conventions.DefiningMessagesAs(MessageTypeConventions.EndsWith("Message"));

            // Define features
            configuration.UseSerialization<JsonSerializer>();
            configuration.UsePersistence<InMemoryPersistence>();
            configuration.DisableFeature<Sagas>();
            configuration.DisableFeature<MessageDrivenSubscriptions>();
            configuration.DisableFeature<TimeoutManager>();

            // Add the bus to the container
            var bus = Bus.CreateSendOnly(configuration);
            container.Configure(c => c.ForSingletonOf<ISendOnlyBus>().Use(x => bus).Named("__NAME__"));

        }
    }
}
