using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BaseDTO.WXDTO
{
    public class WxBaseDTO
    {
        /// <summary>
        /// 错误状态码
        /// </summary>
        public string errcode { get; set; }
        /// <summary>
        /// 错误状态描述
        /// </summary>
        public string errmsg { get; set; }
    }
}
