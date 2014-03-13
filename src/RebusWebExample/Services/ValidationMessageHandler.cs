using System.ComponentModel.DataAnnotations;
using Rebus;

namespace RebusWebExample.Services
{
    public class ValidationMessageHandler
        : IHandleMessages<object>
    {
        public void Handle(object message)
        {
            var context = new ValidationContext(message, null, null);
            Validator.ValidateObject(message, context);
        }
    }
}