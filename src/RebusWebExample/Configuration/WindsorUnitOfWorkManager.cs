using Castle.MicroKernel.Lifestyle;
using Castle.Windsor;
using Rebus;
using Rebus.Bus;

namespace RebusWebExample.Configuration
{
    public class WindsorUnitOfWorkManager : IUnitOfWorkManager
    {
        private readonly IWindsorContainer _container;

        public WindsorUnitOfWorkManager(IWindsorContainer container)
        {
            _container = container;
        }

        public IUnitOfWork Create()
        {
            var context = MessageContext.GetCurrent();
            var lifetimeScope = _container.BeginScope();
            context.Items.Add("WindsorScope", lifetimeScope);
            return new DatabaseUnitOfWork(lifetimeScope);
        }
    }
}