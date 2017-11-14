using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strore_Common
{
    [Serializable]  
    public class SimpleResult
    {
        /// <summary>
        /// 方法
        /// </summary>
        /// <param name="status">1成功，0：已知的失败结果，-1未知的失败结果</param>
        /// <param name="msg">消息</param>
        public SimpleResult(int status, string msg)
        {
            Status = status;
            Msg = msg;
            Success = false;
        }

        public SimpleResult(int status)
        {
            Status = status;
            Msg = "";
            Success = false;
        }

        public SimpleResult()
        {
        }

        /// <summary>
        /// 1成功，0：已知的失败结果，-1未知的失败结果
        /// </summary> 
        public int Status { get; set; }
        /// <summary>
        /// 消息
        /// </summary> 
        public string Msg { get; set; }
 
        public bool Success { get; set; }
    }

    public class SimpleResult<T> : SimpleResult
    {
        public SimpleResult(int status, string msg) : base(status, msg)
        {

        }

        public SimpleResult(int status) : base(status)
        {

        }
        public SimpleResult() : base()
        { 
        }
         
        public SimpleResult(int stats, T data) : base(stats)
        {
            this.ReturnData = data;
        }
        public T ReturnData { get; set; } 
    }
}
