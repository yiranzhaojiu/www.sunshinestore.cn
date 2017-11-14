using LightInject;
using Strore_Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strore_Common.Bootstrapper
{
    public abstract class BootstrapperTask : Disposable
    {
        protected IServiceContainer container;

        public BootstrapperTask(IServiceContainer container)
        {
            this.container = container;
        }

        public virtual int Order
        {
            get { return 0; }
        }

        public virtual string Description
        {
            get { return ""; }

        }

        public abstract TaskContinuation Execute();
    }
}
