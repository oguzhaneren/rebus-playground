using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rebus;
using RebusWebExample.Messaging;

namespace RebusWebExample
{
    public static class CommandExtensions
    {
        public static Func<IBus> GetBus = () => null;

        public static void SetHeader(this ICommand command, string key, string value)
        {
            var bus = GetBusThrowIfNotRegistered();
            bus.AttachHeader(command, key, value);
        }

        public static string GetHeader(this ICommand command, string key)
        {
            object value = null;
            RunHasCurrentContext(c => c.Headers.TryGetValue(key, out value));
            return value != null ? value.ToString() : null;
        }

        public static void Abort(this ICommand command)
        {
            RunHasCurrentContext(c => c.Abort());
        }

        private static void RunHasCurrentContext(Action<IMessageContext> func)
        {
            if (MessageContext.HasCurrent)
            {
                var context = MessageContext.GetCurrent();
                func(context);
            }
        }

        private static IBus GetBusThrowIfNotRegistered()
        {
            var bus = GetBus();
            if (bus == null)
                throw new InvalidOperationException("Bus not registered");
            return bus;
        }
    }
}