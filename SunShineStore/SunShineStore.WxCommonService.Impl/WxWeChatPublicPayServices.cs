using Newtonsoft.Json;
using Store.BaseDTO;
using Strore_Common.Utility;
using Strore_Common.WebHelper;
using Strore_Common.WxHelper;
using Strore_Data;
using SunShineStore.WxCommonDTO;
using SunShineStore.WxCommonDTO.WxWeChatPublic;
using SunShineStore.WxCommonEntity;
using SunShineStore.WxCommonEnum;
using SunShineStore.WxCommonService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace SunShineStore.WxCommonService.Impl
{
    ///<summary>
    ///微信公众号支付
    ///</summary>
    public class WxWeChatPublicPayServices : IWxWeChatPublicPayServices
    {
        #region 生成支付订单
       
        ///<summary>
        ///统一下单接口
        ///</summary>
        public ExecuteResult<PaymentRequestDTO> UnifytheSingleInterface()
        {
            ExecuteResult<PaymentRequestDTO> result = new ExecuteResult<PaymentRequestDTO>()
            {
                IsSuccess = false
            };
   
            var tran = DbUtility.OpenTransaction<WxPayOrderEntity>();
            try
            { 
                var rt = CreateOrder(); 
                //创建订单失败，事务回滚
                if (!rt.IsSuccess)
                {
                    tran.Rollback();
                    result.ErrorMsg = rt.ErrorMsg;
                    return result;
                }
                
                string url = "https://api.mch.weixin.qq.com/pay/unifiedorder";

                //获取统一下单调用的接口 
                WxPayUnifytheSingleDTO dto = GetSingleInterfaceDTO(rt.ReturnValue.orderno,Convert.ToInt64(rt.ReturnValue.totalfee * 100),"JSAPI");
                var Dic = WebHelper.ObjToDic(dto);
                string dataxml = WxCommonHelper.ArrayToXml(Dic);
                string returnStr = HttpHelper.HttpPost(url, dataxml);

                XmlDocument requestDocXml = new XmlDocument();
                requestDocXml.LoadXml(returnStr);
                XmlElement rootElement = requestDocXml.DocumentElement;
                if (rootElement.SelectSingleNode("return_code").InnerText.Equals("SUCCESS"))
                {
                    PaymentRequestDTO returnDto = new PaymentRequestDTO()
                    {
                        orderno = dto.out_trade_no,
                        price = ((decimal)dto.total_fee /100).ToString(),
                        downordertime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        prepay_id = rootElement.SelectSingleNode("prepay_id").InnerText
                    };

                    tran.Commit();
                    result.IsSuccess = true;
                    result.ErrorMsg = "请求成功！";
                    result.ReturnValue = returnDto;
                    return result;

                    #region  先保留
                    /*
                    if (rootElement.SelectSingleNode("result_code").Equals("SUCCESS"))
                    {
                        result.IsSuccess = true;
                        result.ErrorMsg = "请求成功！";
                        result.ReturnValue =rootElement.SelectSingleNode("prepay_id").InnerText;
                        return result;
                    }
                    else
                    {
                        result.ErrorMsg = string.Concat(rootElement.SelectSingleNode("err_code").InnerText, ":", rootElement.SelectSingleNode("err_code_des").InnerText);
                        return result;
                    }*/
                    #endregion
                }
                else
                {
                    tran.Rollback(); 
                    result.ErrorMsg = rootElement.SelectSingleNode("return_msg").InnerText;
                    return result;
                }
            }
            catch
            {
                tran.Rollback();
                result.ErrorMsg = "微信支付请求失败，请刷新重试！";
                return result;
            }

        }

        ///<summary>
        ///生产订单
        ///</summary>
        ///<returns></returns>
        public ExecuteResult<WxPayOrderEntity> CreateOrder()
        {
            var result =new  ExecuteResult <WxPayOrderEntity>(){
                IsSuccess=false
            };
            WxPayOrderEntity entity = new WxPayOrderEntity();
            entity.goodsid = "1";
            entity.goodsprice = 0.01;
            entity.ordernum = 1; 
            entity.goodstitle = "支付测试商品，请勿购买";
            entity.address = "北京市";
            entity.isDelete = 1;
            entity.orderInsertime = DateTime.Now;
            entity.orderno = GetOrderNomber();
            entity.orderstatus = OrderStatusEnum.toBepaid;
            entity.userid = 1;

            long id = DbUtility.Add<WxPayOrderEntity>(entity,true);

            if (id> 0)
            {
                result.IsSuccess = true;
                result.ReturnValue=entity;
                return result;
            }
            else
            {
                result.ErrorMsg = "订单生产失败";
                return result;
            }
        }

        ///<summary>
        ///获取统一下单调用的接口
        ///</summary>
        ///<returns></returns>
        private WxPayUnifytheSingleDTO GetSingleInterfaceDTO(string orderno, long total_fee = 0, string trade_type="")
        {
            WxPayUnifytheSingleDTO dto = new WxPayUnifytheSingleDTO();
            var time = DateTime.Now;
            dto.appid = WxHelper.WxAppid;
            dto.mch_id = WxHelper.mch_id;
            dto.device_info = "WEB";
            dto.nonce_str = WxCommonHelper.GetNewGuidStr();
            dto.sign_type = "MD5";
            dto.body = "JSAPI支付测试";
            dto.detail = "JSAPI支付测试";
            dto.attach = "支付测试";
            dto.out_trade_no = orderno;
            dto.fee_type = "CNY";
            dto.total_fee = total_fee;
            dto.spbill_create_ip = WebHelper.GetAddressIp();
            dto.time_start = time.ToString("yyyyMMddHHmmss");
            dto.time_expire = time.AddMinutes(30).ToString("yyyyMMddHHmmss");
            dto.notify_url = CommonHelper.GetConfigData("OrderCallbackUrl");
            dto.trade_type = trade_type;
            dto.openid = "o9oX1vnJ2h1tmBaRJDWbsQGnvA7g";
            string signStr = WxCommonHelper.GetPaySignStr(dto) + "key=" + WxHelper.WxAPIPaySecret;
            dto.sign = MD5Util.MD5(signStr).ToUpper();

            return dto;
        }

        ///<summary>
        ///获取商户订单号
        ///</summary>
        public string GetOrderNomber()
        {
            long id = DbUtility.Add<WxPayOrderNoEntity>(new WxPayOrderNoEntity()
            {
                insertime = DateTime.Now
            }, true);
            var ids = id.ToString();

            #region 整理订单号 
            string orderno = string.Empty;
            switch (ids.Length)
            {
                case 1:
                    {
                        orderno = "000000000" + ids;
                        break;
                    }
                case 2:
                    {
                        orderno = "00000000" + ids;
                        break;
                    }
                case 3:
                    {
                        orderno = "0000000" + ids;
                        break;
                    }
                case 4:
                    {
                        orderno = "000000" + ids;
                        break;
                    }
                case 5:
                    {
                        orderno = "00000" + ids;
                        break;
                    }
                case 6:
                    {
                        orderno = "0000" + ids;
                        break;
                    }
                case 7:
                    {
                        orderno = "000" + ids;
                        break;
                    }
                case 8:
                    {
                        orderno = "00" + ids;
                        break;
                    }
                case 9:
                    {
                        orderno = "0" + orderno;
                        break;

                    }
                default:
                    {
                        orderno = ids;
                        break;
                    }
            };
            #endregion

            return string.Concat("ST", DateTime.Now.ToString("yyyyMMdd"), orderno);
        }

        #endregion

        #region 

        ///<summary>
        ///模式二：统一下单扫码支付
        ///</summary>
        public ExecuteResult<string> NATIVEUnifytheSingleInterface()
        {
            ExecuteResult<string> result = new ExecuteResult<string>()
            {
                IsSuccess = false
            };

            var tran = DbUtility.OpenTransaction<WxPayOrderEntity>();
            try
            {
                var rt = CreateOrder();
                //创建订单失败，事务回滚
                if (!rt.IsSuccess)
                {
                    tran.Rollback();
                    result.ErrorMsg = rt.ErrorMsg;
                    return result;
                }

                string url = "https://api.mch.weixin.qq.com/pay/unifiedorder";

                //获取统一下单调用的接口 
                WxPayUnifytheSingleDTO dto = GetSingleInterfaceDTO(rt.ReturnValue.orderno, Convert.ToInt64(rt.ReturnValue.totalfee * 100), "NATIVE");
                var Dic = WebHelper.ObjToDic(dto);
                string dataxml = WxCommonHelper.ArrayToXml(Dic);
                string returnStr = HttpHelper.HttpPost(url, dataxml);

                XmlDocument requestDocXml = new XmlDocument();
                requestDocXml.LoadXml(returnStr);
                XmlElement rootElement = requestDocXml.DocumentElement;
                if (rootElement.SelectSingleNode("return_code").InnerText.Equals("SUCCESS") && rootElement.SelectSingleNode("result_code").InnerText.Equals("SUCCESS"))
                { 
                    tran.Commit();
                    result.IsSuccess = true;
                    result.ErrorMsg = "请求成功！";
                    result.ReturnValue = rootElement.SelectSingleNode("code_url").InnerText;
                    return result; 
                }
                else
                {
                    tran.Rollback();
                    result.ErrorMsg = rootElement.SelectSingleNode("return_msg").InnerText;
                    return result;
                }
            }
            catch
            {
                tran.Rollback();
                result.ErrorMsg = "微信支付请求失败，请刷新重试！";
                return result;
            }
        }

        #endregion

        #region 确认订单

        public ExecuteResult<WxPayCallBackReturnDTO> CallBackSureOrder()
        {
            var result = new ExecuteResult<WxPayCallBackReturnDTO>()
            {
                IsSuccess = false
            };
            //解析数据并验证签名
            var rt = TransferXmlToDicAndValideSign();
            //解析或签名失败
            if (!rt.IsSuccess)
            {
                result.ReturnValue = new WxPayCallBackReturnDTO()
                {
                    return_code = WxPayCallBackRentnCodeEnum.FAIL,
                    return_msg = rt.ErrorMsg
                };
                return result;
            }

            try
            {
                var _dic = rt.ReturnValue;
                var order_no = _dic["out_trade_no"];
                var userid = GetUserInfo(_dic["openid"]);

                var model = DbUtility.GetSingle<WxPayOrderEntity>(r => r.orderno == order_no && r.userid == userid);
                if (model == null)
                {
                    result.ReturnValue = new WxPayCallBackReturnDTO()
                    {
                        return_code = WxPayCallBackRentnCodeEnum.FAIL,
                        return_msg = "订单信息错误"
                    };
                    return result;
                }

                if (model.isCanCallbackWxData.Equals(1))
                {
                    result.ReturnValue = new WxPayCallBackReturnDTO()
                    {
                        return_code = WxPayCallBackRentnCodeEnum.FAIL,
                        return_msg = "订单信息不支持修改"
                    };
                    return result;
                }

                var sqlexp = DbUtility.GetGetSqlExpression<WxPayOrderEntity>();
                sqlexp.UpdateFields.Add("transaction_id");
                sqlexp.UpdateFields.Add("orderstatus");
                sqlexp.UpdateFields.Add("callbackTime");
                sqlexp.UpdateFields.Add("isCanCallbackWxData");
                sqlexp.Where(r => r.id == model.id);
                DbUtility.Update<WxPayOrderEntity>(new WxPayOrderEntity()
                {
                    transaction_id = _dic["transaction_id"],
                    orderstatus = OrderStatusEnum.waitSendGoods,
                    callbackTime = DateTime.Now,
                    isCanCallbackWxData=1
                }, sqlexp);

                var ss = new WxPayOrderEntity();
 
                result.IsSuccess = true;
                result.ReturnValue = new WxPayCallBackReturnDTO()
                {
                    return_code = WxPayCallBackRentnCodeEnum.SUCCESS,
                    return_msg = "OK"
                };
                return result;

            }
            catch(Exception ex)
            {
                result.ReturnValue = new WxPayCallBackReturnDTO()
                {
                    return_code = WxPayCallBackRentnCodeEnum.FAIL,
                    return_msg = "数据回填异常"
                };
                return result;
            } 
        }

        public int GetUserInfo(string wxopenid)
        {
            return 1;
        }

        ///<summary>
        ///接收从微信支付后台发送过来的数据并验证签名
        ///</summary>
        ///
        ///<returns></returns> 
        private ExecuteResult<Dictionary<string, string>> TransferXmlToDicAndValideSign()
        {
            var result = new ExecuteResult<Dictionary<string, string>>()
            {
                IsSuccess = false
            };
            try
            { 
                var Context = HttpContext.Current;
                //接收从微信后台POST过来的数据 
                System.IO.Stream s = Context.Request.InputStream;
                int count = 0;
                byte[] buffer = new byte[1024];
                StringBuilder builder = new StringBuilder();
                while ((count = s.Read(buffer, 0, 1024))> 0)
                {
                    //编码方式UTF-8
                    builder.Append(Encoding.UTF8.GetString(buffer, 0, count));
                }
                s.Flush();
                s.Close();
                s.Dispose();

         
                //验证信息是否正确
                var _dic = WxCommonHelper.StrToDic(builder.ToString());
                if (_dic["return_code"] == null || !_dic["return_code"].ToString().Equals("SUCCESS"))
                {
                    result.ErrorMsg = "微信支付后台发送数据格式错误（请检验return_code值）";
                    return result;
                }

                var RuleOutSign = _dic.Where(r => r.Key != "sign").ToDictionary(r => r.Key, r => r.Value.ToString());

                var signStr = WxCommonHelper.GetPaySignStr(RuleOutSign) + "key=" + WxHelper.WxAPIPaySecret;
                var sign = MD5Util.MD5(signStr).ToUpper();

                if (!sign.Equals(_dic["sign"].ToString()))
                {
                    result.ErrorMsg = "签名失败";
                    return result;
                }
                result.IsSuccess = true;
                result.ReturnValue = _dic;
                return result;
            }
            catch
            {
                result.ErrorMsg = "微信支付后台发送数据解析失败";
                return result;
            }
        }

        #endregion
    }
}
