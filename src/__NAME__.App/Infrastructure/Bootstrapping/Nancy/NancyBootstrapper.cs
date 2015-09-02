using System.Linq;
using AutoMapper;
using Crux.Core.Bootstrapping;
using Crux.NancyFx.Infrastructure.Pipelines;
using log4net.Config;
using Nancy;
using Nancy.Authentication.Token;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.StructureMap;
using Nancy.Diagnostics;
using StructureMap;
using StructureMap.Graph;

namespace __NAME__.App.Infrastructure.Bootstrapping.Nancy
{
    public class NancyBootstrapper : StructureMapNancyBootstrapper
    {
        private readonly IContainer _container;
        protected override DiagnosticsConfiguration DiagnosticsConfiguration => new DiagnosticsConfiguration { Password = "1234" };

        static NancyBootstrapper()
        {
            InitializeLogging();
        }

        public NancyBootstrapper(IContainer container)
        {
            _container = container;
        }

        protected override IContainer GetApplicationContainer() => _container;

        protected override void ConfigureApplicationContainer(IContainer existingContainer)
        {
            // Setup container
            existingContainer.Configure(c => c.Scan(s => {
                s.TheCallingAssembly();
                s.WithDefaultConventions();
                s.LookForRegistries();
            }));

            // Initialize all IRunAtStartup classes
            InitializeStartupRunners(existingContainer);
        }

        protected override void RequestStartup(IContainer requestContainer, IPipelines pipelines, NancyContext context)
        {
            // Enable token authentication
            TokenAuthentication.Enable(pipelines, new TokenAuthenticationConfiguration(requestContainer.GetInstance<ITokenizer>()));

            // Set up unit of work
            pipelines.BeforeRequest += UnitOfWorkPipeline.BeforeRequest(requestContainer);
            pipelines.AfterRequest += UnitOfWorkPipeline.AfterRequest();

            // Set up validation exception handling
            pipelines.OnError += HttpBadRequestPipeline.OnHttpBadRequest;
        }

        private static void InitializeLogging()
        {
            XmlConfigurator.Configure();
        }

        private static void InitializeStartupRunners(IContainer existingContainer)
        {
            var mappingDefinitions = existingContainer.GetAllInstances<IRunAtStartup>().ToList();
            mappingDefinitions.ForEach(mappingDefinition => mappingDefinition.Init());

            Mapper.AssertConfigurationIsValid();
        }
    }
}