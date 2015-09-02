using FluentAssertions;
using NUnit.Framework;
using __NAME__.App.Domain;
using __NAME__.Tests.Infrastructure;

namespace __NAME__.Tests.Persistence.Examples
{
    public class ExamplePersistenceTester : DomainPersistenceTester
    {
        [Test]
        public void should_save_and_load()
        {
            var entity = new ExampleEntity("test");
            var newEntity = VerifyPersistence(entity);

            newEntity.ShouldBeEquivalentTo(entity, DefaultCompareConfig.Compare);
        }
    }
}
