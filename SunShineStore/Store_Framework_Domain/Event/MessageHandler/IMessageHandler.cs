using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Framework_Domain.Event.MessageHandler
{
    /// <summary>
    /// 处理消息
    /// </summary>
    public interface IMessageHandler
    {
        void Handle(DomainEvent evnt);
    }

    public interface IMessageHandler<TDomainEvent> : IMessageHandler
       where TDomainEvent : DomainEvent
    {
    }
}
