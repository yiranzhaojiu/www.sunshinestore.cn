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
    [Alias(BaseTable.Users)]
    [BusinessRwType(BusinessType.UserInfoRead, BusinessType.UserInfoWrite)]
    public class UserInfoEntity
    {
        /// <summary>
        /// 主键自增编号
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        public string ID { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string tel { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string back1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string back2 { get; set; } 
        
    }
}
