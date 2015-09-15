using Crux.Domain.Persistence;
using NHibernate;
using NUnit.Framework;

namespace __NAME__.Tests.Infrastructure
{
    [TestFixture]
    public abstract class DomainPersistenceTester : Crux.Domain.Testing.Persistence.DomainPersistenceTester<int>
    {
        public override ISessionFactory SessionFactory => new TestSessionFactoryConfig().CreateSessionFactory();

        public override IDbConnectionProvider ConnectionProvider => new TestConnectionProvider("__NAME__");
    }
}
