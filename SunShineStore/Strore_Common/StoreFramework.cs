using LightInject;
using Strore_Common.Component;
using Strore_Common.Log.Impl;
using Strore_Common.Log.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Strore_Common
{
    public class StoreFramework
    {
        /// <summary>
        /// ioc容器
        /// </summary>
        private static readonly IServiceContainer container = new ServiceContainer();

        private static Strore_Common.Bootstrapper.Bootstrapper bootstrapper;
        private const string Version = "2014-07-01-015";//年-月-日-时

        internal static IServiceContainer Container
        {
            get
            {
                return container;
            }
        }

        public static void Start()
        {
            #region IOC框架启动日志

            var Log = new Log4NetLoggerFactory("log4net.config");
            StoreFramework.Container.RegisterInstance<ILoggerFactory>(Log);

            var logger = LocalServiceLocator.GetService<ILoggerFactory>().Create("bootstrapper");
            logger.InfoFormat("StoreFramework开始启动...版本号：{0}", Version);
            #endregion

            bootstrapper = new Bootstrapper.Bootstrapper(container);
            if (bootstrapper.Execute())
            {
                logger.InfoFormat("StoreFramework启动完成！...版本号：{0}", Version);
            }

        }

        /// <summary>
        /// 结束框架，进行资源释放
        /// </summary>
        public static void End()
        {
            var logger = LocalServiceLocator.GetService<ILoggerFactory>().Create("bootstrapper");
            logger.Info("StoreFramework开始清理...");
            bootstrapper.Dispose();
            logger.Info("StoreFramework清理完成！");
        }
    }
}
