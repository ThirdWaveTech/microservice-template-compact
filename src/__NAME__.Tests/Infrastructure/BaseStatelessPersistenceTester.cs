using NHibernate;
using NUnit.Framework;

namespace __NAME__.Tests.Infrastructure
{
    public abstract class BaseStatelessPersistenceTester
    {
        protected BaseStatelessPersistenceTester()
        {
            StatelessSession = GetStatelessSession();
        }

        public IStatelessSession GetStatelessSession()
        {
            var sessionFactory = new TestSessionFactoryConfig().CreateSessionFactory();
            var connectionProvider = new TestConnectionProvider("AllInOne");
            return sessionFactory.OpenStatelessSession(connectionProvider.GetConnection());
        }

        protected IStatelessSession StatelessSession { get; }

        [TearDown]
        public void TearDown()
        {
            StatelessSession.Dispose();
        }
    }
}
