using Strore_Common;
using SunShineStore.CustomerDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunShineStore.Customer.Interface
{
    public interface ICustomerInfoServices
    {
        /// <summary>
        /// 验证用户信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        SimpleResult ValidUserInfo(CusetomerUserInfoDTO dto);
    }
}
