using LightInject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strore_Common.Bootstrapper
{
    public abstract class RegisterServiceBootstrapperTask : BootstrapperTask
    {
        public RegisterServiceBootstrapperTask(IServiceContainer container) : base(container) { }

        public override int Order
        {
            get
            {
                return 1;
            }
        } 
    }
}
