using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunShineStore.CustomerDTO
{
    /// <summary>
    /// 查询搜索
    /// </summary>
    public class UserSerachDTO:BaseSearch
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        public string Tel { get; set; }
    }

    /// <summary>
    /// 用户返回集合
    /// </summary>
    public class ReturnUserInfoDTO
    {
         public List<UserInfoDTO> UserInfo { get; set; }
    }

}
