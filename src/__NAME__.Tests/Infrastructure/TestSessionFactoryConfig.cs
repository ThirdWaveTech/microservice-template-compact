using FluentNHibernate.Cfg.Db;
using NHibernate;
using __NAME__.App.Infrastructure.Persistence;

namespace __NAME__.Tests.Infrastructure
{
    public class TestSessionFactoryConfig : SessionFactoryConfig
    {
        protected override IPersistenceConfigurer GetDatabaseConfiguration()
        {
            var provider = new TestConnectionProvider("AllInOne");

            return MsSqlConfiguration.MsSql2012
                .ConnectionString(provider.GetConnectionString());
        }

        public override ISessionFactory CreateSessionFactory()
        {
            return CreateSessionFactory(typeof(SessionFactoryConfig).Assembly);
        }
    }
}
