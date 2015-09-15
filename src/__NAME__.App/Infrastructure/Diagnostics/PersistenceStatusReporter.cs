using System;
using System.Collections.Generic;
using Crux.Domain.Persistence.NHibernate;
using __NAME__.Messages.Diagnostics;

namespace __NAME__.App.Infrastructure.Diagnostics
{
    public class PersistenceStatusReporter : IReportStatus
    {
        private readonly INHibernateUnitOfWork _unitOfWork;

        public PersistenceStatusReporter(INHibernateUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IList<StatusItem> ReportStatus()
        {
            var statusItem = new StatusItem("__NAME__.Domain.Persistence");
            var repo = new MigrationsRepository(_unitOfWork);

            //Try sending a messages
            try
            {
                statusItem.Comment = $"Last migration {repo.LastMigration}";
                statusItem.Status = StatusItem.OK;
            }
            catch (Exception e)
            {
                statusItem.Status = StatusItem.Error;
                statusItem.Comment = e.Message;
            }

            return new[] { statusItem };
        }
    }
}