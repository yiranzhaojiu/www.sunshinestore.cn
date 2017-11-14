using Store_FrameWork.BaseData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_FrameWork.Attributes
{
    /// <summary>
    /// 业务类型（读，写
    /// </summary>
    public class BusinessRwTypeAttribute : Attribute
    {
        /// <summary>
        /// 业务读取权限
        /// </summary>
        public BusinessType BusinessRead { get; set; }

        /// <summary>
        /// 业务写权限
        /// </summary>
        public BusinessType BusinessWrite { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="readBusinessType">读业务类型</param>
        /// <param name="writeBusinessType">写业务类型</param>
        public BusinessRwTypeAttribute(BusinessType _BusinessRead, BusinessType _BusinessWrite)
        {
            this.BusinessRead = _BusinessRead;
            this.BusinessWrite = _BusinessWrite;
        }
    }
}
