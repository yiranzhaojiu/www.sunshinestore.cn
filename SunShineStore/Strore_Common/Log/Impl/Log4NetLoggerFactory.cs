using log4net.Appender;
using log4net.Config;
using log4net.Layout;
using Strore_Common.Log.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strore_Common.Log.Impl
{
    /// <summary>Log4Net based logger factory.
    /// </summary>
    public class Log4NetLoggerFactory : ILoggerFactory
    {
        /// <summary>Parameterized constructor.
        /// </summary>
        /// <param name="configFile"></param>
        public Log4NetLoggerFactory(string configFile)
        {
            var file = new FileInfo(configFile);
            if (!file.Exists)
            {
                file = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config", configFile));
            }

            if (file.Exists)
            {
                XmlConfigurator.ConfigureAndWatch(file);
            }
            else
            {
                BasicConfigurator.Configure(new ConsoleAppender { Layout = new PatternLayout() });
            }
        }
        /// <summary>Create a new Log4NetLogger instance.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ILogger Create(string name)
        {
            return new Log4NetLogger(log4net.LogManager.GetLogger(name));
        }
        /// <summary>Create a new Log4NetLogger instance.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ILogger Create(Type type)
        {
            return new Log4NetLogger(log4net.LogManager.GetLogger(type));
        }
    }
}
