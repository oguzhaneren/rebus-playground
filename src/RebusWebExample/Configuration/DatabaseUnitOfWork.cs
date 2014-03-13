using System;
using Microsoft.AspNet.SignalR;
using Rebus;
using Rebus.Bus;
using RebusWebExample.Hubs;

namespace RebusWebExample.Configuration
{
    public class DatabaseUnitOfWork
        : IUnitOfWork
    {
        private readonly IDisposable _scope;
        private IHubContext _hubContext;

        public DatabaseUnitOfWork(IDisposable scope)
        {
            _scope = scope;
            _hubContext = GlobalHost.ConnectionManager.GetHubContext<MessageNotificationHub>();
            _hubContext.Clients.All.notify(string.Format("'{0}' message unit of work started", GetMessageId()));
        }

        public string GetMessageId()
        {
            return MessageContext.GetCurrent().Headers["rebus-correlation-id"].ToString();
        }

        public void Dispose()
        {
            _scope.Dispose();
            _hubContext.Clients.All.notify(string.Format("'{0}' message unit of work disposed", GetMessageId()));
        
        }

        public void Commit()
        {
            _hubContext.Clients.All.notify(string.Format("'{0}' message unit of work commited", GetMessageId()));
        }

        public void Abort()
        {
            _hubContext.Clients.All.notify(string.Format("'{0}' message unit of work aborted", GetMessageId()));
        }
    }
}