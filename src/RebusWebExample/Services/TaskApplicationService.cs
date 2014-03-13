using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Rebus;
using RebusWebExample.Messaging;

namespace RebusWebExample.Services
{
    public class TaskApplicationService
        : IHandleMessages<ChangeTaskNameCommand>
    {
    
        public void Handle(ChangeTaskNameCommand message)
        {
            Thread.Sleep(3000);
        }
    }
}