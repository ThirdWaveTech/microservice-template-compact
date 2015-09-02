using Crux.Domain.Persistence.NHibernate;
using __NAME__.App.Domain;

namespace __NAME__.App.Infrastructure.Persistence
{
    public class NHibernateRepository : NHibernateRepositoryOfId<int>, IRepository
    {
        public NHibernateRepository(INHibernateUnitOfWork unitOfWork) : base(unitOfWork) {}
    }
}
