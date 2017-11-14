using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunShineStore.WxCommonDTO.WxWeChatPublic
{
    /// <summary>
    /// 微信支付结果通知
    /// </summary>
    public class WxPayNoticeDTO
    {
        /// <summary>
        /// 公众账号ID
        /// 必填：是
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 商户号ID
        /// 必填：是
        /// </summary>
        public string mch_id { get; set; } 
        /// <summary>
        /// 设备号 
        /// 必填：否
        /// </summary>
        public string device_info { get; set; } 
        /// <summary>
        /// 随机字符串 
        /// 必填：是
        /// </summary>
        public string nonce_str { get; set; } 
        /// <summary>
        /// 签名 
        /// 必填：是
        /// </summary>
        public string sign { get; set; } 
        /// <summary>
        /// 签名类型 
        /// 必填：否
        /// </summary>
        public string sign_type { get; set; }
        /// <summary>
        /// 业务结果 
        /// 必填：是
        /// </summary>
        public string result_code { get; set; } 
        /// <summary>
        /// 错误代码 
        /// 必填：否
        /// </summary>
        public string err_code { get; set; }
        /// <summary>
        /// 错误代码描述 
        /// 必填：否
        /// </summary>
        public string err_code_des { get; set; }
        /// <summary>
        /// 用户标识 
        /// 必填：是
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 是否关注公众账号  Y	用户是否关注公众账号，Y-关注，N-未关注，仅在公众账号类型支付有效
        /// 必填：否
        /// </summary>
        public string is_subscribe { get; set; }
        /// <summary>
        /// 交易类型 JSAPI	JSAPI、NATIVE、APP
        /// 必填：是
        /// </summary>
        public string trade_type { get; set; }  
        /// <summary>
        /// 付款银行
        /// 必填：是
        /// </summary>
        public string bank_type { get; set; }
        /// <summary>
        /// 订单金额
        /// 必填：是
        /// </summary>
        public string total_fee { get; set; } 
        /// <summary>
        /// 应结订单金额
        /// 必填：否
        /// </summary>
        public int settlement_total_fee { get; set; } 
        /// <summary>
        /// 货币种类
        /// 必填：否
        /// </summary>
        public string fee_type { get; set; } 
        /// <summary>
        /// 现金支付金额
        /// 必填：是
        /// </summary>
        public int cash_fee { get; set; } 
        /// <summary>
        /// 现金支付货币类型
        /// 必填：否
        /// </summary>
        public string cash_fee_type { get; set; } 
        /// <summary>
        /// 总代金券金额
        /// 必填：否
        /// </summary>
        public int coupon_fee { get; set; }
        /// <summary>
        /// 代金券使用数量
        /// 必填：否
        /// </summary>
        public int coupon_count { get; set; }
      
        /// <summary>
        /// 微信支付订单号ID
        /// 必填：是
        /// </summary>
        public string transaction_id { get; set; } 
        /// <summary>
        /// 商户订单号ID
        /// 必填：是
        /// </summary>
        public string out_trade_no { get; set; } 
        /// <summary>
        /// 商家数据包
        /// 必填：否
        /// </summary>
        public string attach { get; set; } 
        /// <summary>
        /// 支付完成时间
        /// 必填：是
        /// </summary>
        public string time_end { get; set; }

    }
}
