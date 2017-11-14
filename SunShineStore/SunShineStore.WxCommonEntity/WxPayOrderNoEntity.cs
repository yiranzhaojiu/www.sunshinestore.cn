using ServiceStack.DataAnnotations;
using Store_FrameWork.Attributes;
using Store_FrameWork.BaseData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunShineStore.WxCommonEntity
{
    /// <summary>
    /// 生成商户订单号
    /// </summary>
    [Alias("wx_orderno")]
    [BusinessRwType(BusinessType.BusinessMgRead, BusinessType.BusinessMgWrite)]
    public class WxPayOrderNoEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        public long id { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime insertime { get; set; }
    }
}
