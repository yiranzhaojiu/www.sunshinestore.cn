using Store_Framework_Domain.Event;
using SunShineStore.WxCommonService.Impl.Event.EventData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunShineStore.WxCommonService.Impl.Event.EventHandler
{
    /// <summary>
    /// 库存事件处理
    /// </summary>
    public class RepertoryUpdateHandler :
        IEventHandler<OrderPayFinshNotify>,
        IEventHandler<OrderPayRecordLog>

    { 
        /// <summary>
        /// 处理订单完成库存递减
        /// </summary>
        /// <param name="eventdata"></param>
        public void Handle(OrderPayFinshNotify eventdata)
        { 
            
        }

        /// <summary>
        /// 订单完成，库存递减完成写入日志
        /// </summary>
        /// <param name="eventdata"></param>
        public void Handle(OrderPayRecordLog eventdata)
        {

        } 

    }
}
