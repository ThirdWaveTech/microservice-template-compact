using System;
using System.Collections.Generic;
using NServiceBus;
using __NAME__.Messages.Diagnostics;

namespace __NAME__.App.Infrastructure.Diagnostics
{
    public class MessageBusPingReporter : IReportStatus
    {
        private readonly IBus _bus;

        public MessageBusPingReporter(IBus bus)
        {
            _bus = bus;
        }

        public IList<StatusItem> ReportStatus()
        {
            var statusItem = new StatusItem("__NAME__.MessageBus.Client");

            // Try sending a message
            try {
                _bus.Send(new PingCommand { Sender = "__NAME__.App" });
                statusItem.Status = StatusItem.OK;
            }
            catch (Exception e)
            {
                statusItem.Status = StatusItem.Error;
                statusItem.Comment = e.Message;
            }
                
            return new []{statusItem};
        }
    }
}