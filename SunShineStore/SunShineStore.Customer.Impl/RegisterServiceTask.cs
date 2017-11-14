using LightInject;
using Strore_Common.Bootstrapper;
using SunShineStore.Customer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunShineStore.Customer.Impl
{
    public class RegisterServiceTask : BootstrapperTask
    {
        public RegisterServiceTask(IServiceContainer container)
            : base(container)
        { 
        
        }

        public override TaskContinuation Execute()
        {
            container.Register<ICustomerInfoServices, CustomerInfoServices>(new PerRequestLifeTime());
            container.Register<IUserServices, UserServices>(new PerRequestLifeTime());
            return TaskContinuation.Continue;
        }
    }
}
