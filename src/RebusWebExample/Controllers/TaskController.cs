using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RebusWebExample.Messaging;

namespace RebusWebExample.Controllers
{
    public class TaskController : ApiController
    {
        
        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]ChangeTaskNameCommand command)
        {
            command.SetHeader("api-key", Constants.ApiKey);
            MvcApplication.Bus.SendLocal(command);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        
    }
}