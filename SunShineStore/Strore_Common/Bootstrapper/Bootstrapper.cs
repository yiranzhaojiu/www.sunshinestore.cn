using LightInject;
using Strore_Common.Log.Interface;
using Strore_Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strore_Common.Extensions;

namespace Strore_Common.Bootstrapper
{
    public abstract class Bootstrapper : Disposable
    {
        /// <summary>
        /// ioc容器
        /// </summary>
        private IServiceContainer container;

        /// <summary>
        /// 日志类
        /// </summary>
        private ILogger logger;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="container"></param>
        public Bootstrapper(IServiceContainer container)
        {
            this.container = container;
            this.logger = container.GetInstance<ILoggerFactory>().Create("bootstrapper");

            //注册一个全局实例
            container.RegisterInstance<IServiceContainer>(container);

            //注册所有继承自BootstrapperTask的类
            BuildManagerWrapper.Current.ConcreteTypes.Where(type => typeof(BootstrapperTask).IsAssignableFrom(type))
                       .Each(type => container.RegisterMultipleTypesAsSingleton(typeof(BootstrapperTask), type));
      

            ///*分解*/
            ////获取所有继承了BootstrapperTask的类
            //IEnumerable<Type> buildType = BuildManagerWrapper.Current.ConcreteTypes.Where(type => typeof(BootstrapperTask).IsAssignableFrom(type));
            //buildType.Each(type => container.RegisterMultipleTypesAsSingleton(typeof(BootstrapperTask), type));

        }

        public bool Execute()
        {
            bool successful = true;

            var tasks = container.GetAllInstances<BootstrapperTask>();

            Parallel.ForEach(tasks, (task) =>
            {
                try
                {
                    if (task.Execute() == TaskContinuation.Break)
                    {
                        successful = false;
                    }
                }
                catch (System.Reflection.ReflectionTypeLoadException ex)
                {

                }
                catch(Exception ex)
                { 
                
                }
            });

            return successful;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        protected override void InternalDispose()
        {
            container.GetAllInstances<BootstrapperTask>().Each(task=>{
                try
                {
                    task.Dispose();
                }
                catch
                { 
                
                }
            });
        }
    }
}
