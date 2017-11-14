using Store_Framework_Domain.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunShineStore.WxCommonService.Impl.Event.EventData
{
    /// <summary>
    /// 订单支付完成通知
    /// </summary>
    public class OrderPayFinshNotify : DomainEvent
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public int OrderID { get; set; } 
        /// <summary>
        /// 支付时间 
        /// </summary>
        public DateTime OrderTime { get; set; }
    }
}
