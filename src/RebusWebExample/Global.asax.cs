using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Rebus;
using Rebus.Castle.Windsor;
using Rebus.Configuration;
using Rebus.Logging;
using Rebus.Messages;
using Rebus.NLog;
using Rebus.Transports.Msmq;
using RebusWebExample.Configuration;
using RebusWebExample.Messaging;
using RebusWebExample.Services;

namespace RebusWebExample
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801


    public class MvcApplication : System.Web.HttpApplication
    {
        public static IWindsorContainer Container;
        public static IBus Bus;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            Container = new WindsorContainer();
            Container.Register(Classes.FromAssemblyContaining<ICommand>()
                      .BasedOn<IHandleMessages>()
                      .WithServiceAllInterfaces()
                      .LifestyleTransient());

            CommandExtensions.GetBus = () => Bus;

            Bus = Configure.With(new WindsorContainerAdapter(Container))
                .Logging(l => l.NLog())
                .Serialization(s => s.UseJsonSerializer()) // ISerializeMessages
                //.Transport(t => t.UseMsmqAndGetInputQueueNameFromAppConfig())
                .Transport(t => t.UseMsmq("my-app.input", "my-app.error"))
                .MessageOwnership(o => o.FromRebusConfigurationSection())
                .SpecifyOrderOfHandlers(c => c
                    .First<ValidationMessageHandler>()
                    .Then<AuthenticationMessageHandler>())
                .Events(e => e.AddUnitOfWorkManager(new WindsorUnitOfWorkManager(Container)))
                .CreateBus()
                .Start();
        }

        protected void Application_End()
        {
            if (Container != null)
                Container.Dispose();
        }
    }
}