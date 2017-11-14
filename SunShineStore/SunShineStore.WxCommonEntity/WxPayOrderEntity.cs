using ServiceStack.DataAnnotations;
using Store_FrameWork.Attributes;
using Store_FrameWork.BaseData;
using SunShineStore.WxCommonEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunShineStore.WxCommonEntity
{
    /// <summary>
    /// 微信支付订单表
    /// </summary>
    [Alias("wx_order")]
    [BusinessRwType(BusinessType.BusinessMgRead, BusinessType.BusinessMgWrite)]
    public class WxPayOrderEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        public int id { get; set; }
        /// <summary>
        /// 订单编号 设置唯一键
        /// </summary>
        public string orderno { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime orderInsertime { get; set; }
        /// <summary>
        /// 订单商品数量
        /// </summary>
        public int ordernum { get; set; }
        /// <summary>
        /// 商品编号
        /// </summary>
        public string goodsid { get; set; }
        /// <summary>
        /// 商品标题
        /// </summary>
        public string goodstitle { get; set; }
        /// <summary>
        /// 商品单件价格
        /// </summary>
        public double goodsprice { get; set; }
        /// <summary>
        /// 商品图片地址
        /// </summary>
        public string goodsimgurl { get; set; }
        /// <summary>
        /// 收货地址
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 订单总额
        /// </summary>
        public double totalfee
        {
            get
            {
                return this.goodsprice * this.ordernum;
            }
        }
        /// <summary>
        /// 用户编号
        /// </summary>
        public int userid { get; set; }
        /// <summary>
        /// 订单的状态
        /// </summary>
        public OrderStatusEnum orderstatus { get; set; }
        /// <summary>
        /// 订单是否删除,0:删除，1-正常
        /// </summary>
        public int isDelete { get; set; }

        /// <summary>
        /// 微信订单编号
        /// </summary>
        public string transaction_id { get; set; }

        /// <summary>
        /// 微信数据回填时间
        /// </summary>
        private DateTime? _callbackTime = null;
        public DateTime? callbackTime
        {
            get { return _callbackTime; }
            set { _callbackTime = value; }
        }

        /// <summary>
        /// 是否可以回填数据
        /// </summary>
        private int _isCanCallbackWxData = 0; 
        public int isCanCallbackWxData { get { return _isCanCallbackWxData; } set { _isCanCallbackWxData = value; } }
          
    }
}
