using Store_Framework_Domain.Event.MessageHandler;
using Strore_Common.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Framework_Domain.Event
{
    /// <summary>
    /// 表示继承于该类的类型为领域事件。
    /// </summary>
    [Serializable]
    public abstract class DomainEvent : IEvent
    {
        #region Private Fields
        private Guid id = Guid.NewGuid();
        private DateTime timeStamp = DateTime.Now;
        #endregion 

        #region IDomainEvent Members
        /// <summary>
        /// 获取领域事件的全局唯一标识。
        /// </summary>
        public Guid ID
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        /// 获取产生领域事件的时间戳。
        /// </summary>
        public DateTime TimeStamp
        {
            get { return timeStamp; }
            set { timeStamp = value; }
        }
        #endregion
          
        /// <summary>
        /// 发布领域事件
        /// </summary>
        /// <typeparam name="TDomainEvent"></typeparam>
        /// <param name="domainEvent"></param>
        public static void Publish<TDomainEvent>(TDomainEvent domainEvent)
            where TDomainEvent : DomainEvent
        {
            try
            {
                var handler = LocalServiceLocator.GetService<IMessageHandler>();
                handler.Handle(domainEvent);
            }
            catch (Exception ex)
            {
                //记录日志
                throw;
            }
        }
    }
}
