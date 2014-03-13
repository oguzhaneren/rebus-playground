using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rebus;

namespace RebusWebExample.Configuration
{
    public class MessageDestination
        : IDetermineMessageOwnership
    {
        public string GetEndpointFor(Type messageType)
        {
            return "local";
        }
    }
}