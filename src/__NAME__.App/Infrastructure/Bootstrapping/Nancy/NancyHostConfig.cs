using System;
using System.Configuration;
using Nancy.Hosting.Self;
using NServiceBus;
using StructureMap;

namespace __NAME__.App.Infrastructure.Bootstrapping.Nancy
{
    public class NancyHostConfig : IWantToRunWhenBusStartsAndStops
    {
        private readonly IContainer _container;
        private readonly string _url;
        private NancyHost _host;

        public NancyHostConfig(IContainer container)
        {
            _container = container;
            _url = ConfigurationManager.AppSettings["Nancy.Url"];
        }

        public void Start()
        {
            _host = new NancyHost(new NancyBootstrapper(_container), new Uri(_url));
            _host.Start();
        }

        public void Stop()
        {
            _host.Stop();
        }
    }
}