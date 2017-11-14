using ServiceStack.DataAnnotations;
using Store_FrameWork.Attributes;
using Store_FrameWork.BaseData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunShineStore.CustomerEntity
{
    [Alias("userInfo")] 
    [BusinessRwType(BusinessType.BusinessMgRead,BusinessType.BusinessMgWrite)]
    public class CustomerUserObjEntity
    {
        /// <summary>
        /// 主键自增编号
        /// </summary>
        [PrimaryKey]
        public string userId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string IsActivity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string useAuthorityId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ChangeTime { get; set; }

        /// <summary>
        /// QQ授权编号
        /// </summary>
        public string QQID { get; set; }
         
        /// <summary>
        /// 微信授权编号
        /// </summary>
        public string WeChatId { get; set; }

        /// <summary>
        /// 微博授权编号
        /// </summary>
        public string WeBoId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string qq { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string useType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gender { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string age { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string career { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string nickname { get; set; }

        /// <summary>
        /// 客户密码
        /// </summary> 
        [Description("客户密码")]
        public string password { get; set; }

 
         
    }
}
