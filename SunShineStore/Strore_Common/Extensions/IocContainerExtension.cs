using LightInject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strore_Common.Extensions
{
    public static class IocContainerExtension
    {
        [DebuggerStepThrough]
        public static IServiceContainer RegisterMultipleTypesAsSingleton(this IServiceContainer instance, Type fromType, Type toType)
        {
            lock (instance)
            {
                instance.Register(fromType,toType,toType.FullName,new PerContainerLifetime());
            }
            return instance;
        }
    }
}
