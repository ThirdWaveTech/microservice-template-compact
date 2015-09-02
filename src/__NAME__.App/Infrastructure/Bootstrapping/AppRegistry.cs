using AutoMapper;
using Crux.Core.Bootstrapping;
using Crux.NancyFx.Infrastructure.Serialization;
using Nancy.Authentication.Token;
using Newtonsoft.Json;
using NServiceBus.UnitOfWork;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using __NAME__.App.Infrastructure.Diagnostics;

namespace __NAME__.App.Infrastructure.Bootstrapping
{
    public class AppRegistry : Registry
    {
        public AppRegistry()
        {
            Scan(s => {
                s.TheCallingAssembly(); 
                s.WithDefaultConventions();

                s.AddAllTypesOf<IRunAtStartup>();
                s.AddAllTypesOf<IReportStatus>();
            });

            For<IManageUnitsOfWork>().Use<UnitOfWorkManager>();

            For<ITokenizer>().Use(new Tokenizer());
            For<JsonSerializer>().Use<CustomJsonSerializer>();
            ForSingletonOf<IMappingEngine>().Use(c => Mapper.Engine);
        }
    }
}
