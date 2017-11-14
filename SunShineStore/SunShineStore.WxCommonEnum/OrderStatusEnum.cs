using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunShineStore.WxCommonEnum
{
    [EnumAsInt]
    public enum  OrderStatusEnum
    {
        /// <summary>
        /// 待付款
        /// </summary>
        toBepaid=0,
        /// <summary>
        /// 商家确认付款
        /// </summary>
        merchantSurepay = 1,
        /// <summary>
        /// 商家待发货,说明商家已确认付款
        /// </summary>
        waitSendGoods = 2,
        /// <summary>
        /// 取消发货
        /// </summary>
        cancelOrder = 3,
        /// <summary>
        /// 商家已发货
        /// </summary>
        shipped = 4,
        /// <summary>
        /// 客户确认已收货
        /// </summary>
        finshOrder = 5
    }
}
