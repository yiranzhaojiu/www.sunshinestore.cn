using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Framework_Domain.Event.MessageHandler
{
    /// <summary>
    /// 同步执行
    /// </summary>
    public class SyncMessageHandler:IMessageHandler
    {
        public void Handle(DomainEvent domainEvent)
        {
            var type = typeof(MessageHandler<>).MakeGenericType(domainEvent.GetType());
            var messageHandler = Activator.CreateInstance(type) as IMessageHandler;
            messageHandler.Handle(domainEvent);
        }
    }
}
