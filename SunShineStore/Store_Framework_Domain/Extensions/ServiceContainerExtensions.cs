using LightInject;
using Store_Framework_Domain.Event;
using Store_Framework_Domain.Event.MessageHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Store_Framework_Domain.Extensions
{
    /// <summary>
    /// 扩展
    /// </summary>
    public  static class ServiceContainerExtensions
    {
        /// <summary>
        /// 事件总线
        /// </summary>
        /// <param name="?"></param>
        /// <param name="assembly"></param>
        public static void RegisterEventHandlers(this IServiceContainer container, Assembly assembly)
        {
            // 默认异步处理
            container.Register<IMessageHandler, SyncMessageHandler>("MessageHandler", new PerContainerLifetime());
             
            var types = assembly.GetTypes();
            HashSet<Type> eventTypes = new HashSet<Type>();

            // 扫描DomainEvent
            foreach (var item in types)
            {
                if (item.BaseType != null && item.BaseType == typeof(DomainEvent))
                {
                    // 注册DomainEventType类型
                    //container.RegisterInstance<DomainEventType>(new DomainEventType { Type = item, TypeName = item.FullName }, item.FullName);
                    var genericType = typeof(IEventHandler<>).MakeGenericType(item);
                    eventTypes.Add(genericType);
                }
            }

            // 注册EventHandler
            foreach (var item in types)
            {
                var implTypes = eventTypes.Where(p => p.IsAssignableFrom(item));
                foreach (var implType in implTypes)
                {
                    var serviceName = implType.FullName + item.Name;
                    container.Register(implType, item, serviceName);
                }
            }

        }
    }
}
