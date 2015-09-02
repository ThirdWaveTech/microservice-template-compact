using System.Collections.Generic;
using __NAME__.Messages.Diagnostics;

namespace __NAME__.App.Infrastructure.Diagnostics
{
    public interface IReportStatus
    {
        IList<StatusItem> ReportStatus();
    }
}
