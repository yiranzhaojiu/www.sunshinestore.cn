using Store_FrameWork.BaseData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strore_Data
{
    /// <summary>
    /// 业务类型（读，写
    /// </summary>
    public class BusinessRwType
    {
        /// <summary>
        /// 业务读取权限
        /// </summary>
        public BusinessType BusinessRead { get; set; }

        /// <summary>
        /// 业务写权限
        /// </summary>
        public BusinessType BusinessWrite { get; set; } 
    }
}
