using System;
using System.Data;
using System.Security.Authentication;
using Microsoft.AspNet.SignalR.Messaging;
using Rebus;
using RebusWebExample.Messaging;

namespace RebusWebExample.Services
{
    public class AuthenticationMessageHandler
        : IHandleMessages<object>
    {
        public void Handle(object message)
        {
            var command = message as ICommand;
            var apiKey = command.GetHeader("api-key");
            if (apiKey != Constants.ApiKey)
            {
                command.Abort();
                throw new AuthenticationException();
            }
        }
    }
}