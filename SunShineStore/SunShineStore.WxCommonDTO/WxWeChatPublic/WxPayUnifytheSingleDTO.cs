using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunShineStore.WxCommonDTO
{
    /// <summary>
    /// 统一下单接口，文档地址：https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=9_1
    /// </summary>
    public class WxPayUnifytheSingleDTO
    {
        /// <summary>
        /// 公众账号ID 
        /// 是否必要：是
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 商户号
        /// 是否必要：是
        /// </summary>
        public string mch_id { get; set; }
        /// <summary>
        /// 设备号
        /// 是否必要：否
        /// </summary>
        public string device_info { get; set; }
        /// <summary>
        /// 随机字符串
        /// 是否必要：是
        /// </summary>
        public string nonce_str { get; set; }
        /// <summary>
        /// 签名
        /// 是否必要：是
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 签名类型  HMAC-SHA256 签名类型，默认为MD5,支持HMAC-SHA256和MD5
        /// 是否必要：否
        /// </summary>
        public string sign_type { get; set; }
        /// <summary>
        /// 商品描述
        /// 是否必要：是
        /// </summary>
        public string body { get; set; }
        /// <summary>
        /// 商品详情
        /// 是否必要：否
        /// </summary>
        public string detail { get; set; }
        /// <summary>
        /// 附加数据
        /// 是否必要：否
        /// </summary>
        public string attach { get; set; }
        /// <summary>
        /// 商户订单号
        /// 是否必要：是
        /// </summary>
        public string out_trade_no { get; set; }

        private string _fee_type = "CNY";
        /// <summary>
        /// 标价币种 默认： 符合ISO 4217标准的三位字母代码，默认人民币：CNY
        ///  是否必要：否 
        /// </summary>
        public string fee_type { get { return _fee_type; } set { _fee_type = value; } }
        /// <summary>
        /// 标价金额，订单总金额，单位为分
        /// 是否必要：是
        /// </summary>
        public long total_fee { get; set; }
        /// <summary>
        /// 终端IP
        /// 是否必要：是
        /// </summary>
        public string spbill_create_ip { get; set; }
        /// <summary>
        /// 交易起始时间
        /// 是否必要：是
        /// </summary>
        public string time_start { get; set; }
        /// <summary>
        /// 交易结束时间
        /// 是否必要：否
        /// </summary>
        public string time_expire { get; set; }
        /// <summary>
        /// 订单优惠标记
        /// 是否必要：否
        /// </summary>
        public string goods_tag { get; set; }
        /// <summary>
        /// 通知地址
        /// 是否必要：是
        /// </summary>
        public string notify_url { get; set; }
        /// <summary>
        /// 交易类型
        /// 是否必要：是  
        /// </summary>
        public string trade_type { get; set; }
        /// <summary>
        /// 商品ID
        /// 是否必要：否
        /// </summary>
        public string product_id { get; set; }
        /// <summary>
        /// 指定支付方式
        /// 是否必要：否
        /// </summary>
        public string limit_pay { get; set; }
        /// <summary>
        /// 用户标识
        /// 是否必要：否,trade_type=JSAPI时（即公众号支付），此参数必传
        /// </summary>
        public string openid { get; set; }
    }

    /// <summary>
    /// 调用统一下单接口返回DTO
    /// </summary>
    public class WxPayUnifytheSingleReturnDTO
    {
        /// <summary>
        /// 返回状态码 SUCCESS/FAIL  此字段是通信标识，非交易标识，交易是否成功需要查看result_code来判断
        /// </summary>
        public string return_code { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string return_msg { get; set; }

        /// <summary>
        /// 公众账号ID
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 商户号
        /// </summary>
        public string mch_id { get; set; }
        /// <summary>
        /// 设备号
        /// </summary>
        public string device_info { get; set; }
        /// <summary>
        /// 随机字符串
        /// </summary>
        public string nonce_str { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 业务结果
        /// </summary>
        public string result_code { get; set; }
        /// <summary>
        /// 错误代码
        /// </summary>
        public string err_code { get; set; }
        /// <summary>
        /// 错误代码描述
        /// </summary>
        public string err_code_des { get; set; }

        /// <summary>
        /// 交易类型
        /// </summary>
        public string trade_type { get; set; }
        /// <summary>
        /// 预支付交易会话标识
        /// </summary>
        public string prepay_id { get; set; }
        /// <summary>
        ///  二维码链接 trade_type为NATIVE时有返回，用于生成二维码，展示给用户进行扫码支付
        /// </summary>
        public string code_url { get; set; }
    }

    /// <summary>
    /// 微信公众号发起支付请求DTO 文档地址：https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=7_7&index=6
    /// </summary>
    public class PaymentRequestDTO
    {
        /// <summary>
        /// 公众号名称，由商户传入    
        /// </summary>
        public string appId { get; set; }

        /// <summary>
        /// 时间戳，自1970年以来的秒数       
        /// </summary>
        public string timeStamp { get; set; }

        /// <summary>
        /// 随机串    
        /// </summary>
        public string nonceStr { get; set; }
         
        /// <summary>
        /// 订单详情扩展字符串    
        /// </summary>
        public string package { get; set; }
        /// <summary>
        ///  微信签名方式   
        /// </summary>
        public string signType { get; set; }
        /// <summary>
        ///  微信签名   
        /// </summary>
        public string paySign { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string orderno { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public string price { get; set; }
        /// <summary>
        /// 调用微信统一下单回话标识
        /// </summary>
        public string prepay_id { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        public string downordertime { get; set; }

    }

}
