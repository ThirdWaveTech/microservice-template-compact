using System.Linq;
using Nancy;
using StructureMap;

namespace __NAME__.App.Infrastructure.Diagnostics
{
    public class DiagnosticsModule: NancyModule
    {
        public DiagnosticsModule(IContainer container)
        {
            Get["status"] = _ => container.GetAllInstances<IReportStatus>()
                .SelectMany(r => r.ReportStatus())
                .ToList();
        }
    }
}