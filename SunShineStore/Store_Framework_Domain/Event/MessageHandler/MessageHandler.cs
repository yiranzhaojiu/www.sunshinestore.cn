using Strore_Common.Component;
using Strore_Common.Log.Interface;
using Strore_Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Framework_Domain.Event.MessageHandler
{
    class MessageHandler<TDomainEvent> : IMessageHandler<TDomainEvent>
       where TDomainEvent : DomainEvent
    {
        //private ILogger _logger;

        //public MessageHandler(ILogger logger)
        //{
        //    _logger = logger;
        //}

        void IMessageHandler.Handle(DomainEvent domainEvent)
        {
            var handlers = LocalServiceLocator.ResolveAll<IEventHandler<TDomainEvent>>();

            foreach (var handler in handlers)
            {
                try
                {
                    handler.Handle(domainEvent as TDomainEvent); 
                }
                catch (Exception ex)
                {
                    var str = CommonHelper.JsonSerializer<TDomainEvent>(domainEvent as TDomainEvent);
                    throw;
                }
            }
        }
    }
}
