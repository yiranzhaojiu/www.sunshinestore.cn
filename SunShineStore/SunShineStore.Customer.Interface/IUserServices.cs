using Store.BaseDTO;
using SunShineStore.CustomerDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunShineStore.Customer.Interface
{
    public interface IUserServices
    {
        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExecuteResult UserInfoAdd(UserInfoDTO model);

        /// <summary>
        /// 查找用户列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExecuteResult<List<UserInfoDTO>> GetList(UserSerachDTO model);
    }
}
