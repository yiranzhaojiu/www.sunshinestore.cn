using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BaseDTO
{
    public class BaseDTO
    {
        /// <summary>
        /// 执行是否成功
        /// </summary>　
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 执行失败时的错误信息
        /// </summary>　
        public string ErrorMsg { get; set; } 

       
    }
}
