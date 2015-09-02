using System;
using Crux.Domain.Entities;
using Crux.Domain.Persistence;
using Crux.Domain.Persistence.NHibernate;
using NHibernate;
using StructureMap.Configuration.DSL;
using __NAME__.App.Domain;
using __NAME__.App.Infrastructure.Persistence;

namespace __NAME__.App.Infrastructure.Bootstrapping
{
    public class NHibernateRegistry : Registry
    {
        public NHibernateRegistry()
        {
            // Configure session factory
            ForSingletonOf<ISessionFactory>()
                .Use(c => new SessionFactoryConfig().CreateSessionFactory());

            // Configure connection provider
            ForSingletonOf<IDbConnectionProvider>()
                .Use(c => SqlConnectionProvider.FromConnectionStringKey("AllInOne"));

            For<INHibernateUnitOfWork>().Use<NHibernateUnitOfWork>();

            // Persistence Infrastructure
            For<IRepositoryOfId<int>>().Use<NHibernateRepositoryOfId<int>>();
            For<IRepositoryOfId<Guid>>().Use<NHibernateRepositoryOfId<Guid>>();
            For<IRepository>().Use<NHibernateRepository>();

            For<IStatelessSession>()
                .Use(c => c.GetInstance<ISessionFactory>().OpenStatelessSession());
        }
    }
}