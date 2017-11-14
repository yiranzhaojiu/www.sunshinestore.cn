using Strore_Common;
using Strore_Common.Cache;
using Strore_Common.Utility;
using Strore_Data;
using SunShineStore.Customer.Interface;
using SunShineStore.CustomerDTO;
using SunShineStore.CustomerEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunShineStore.Customer.Impl
{
    public class CustomerInfoServices : ICustomerInfoServices
    {
        /// <summary>
        /// 验证用户信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public SimpleResult ValidUserInfo(CusetomerUserInfoDTO dto)
        { 
            var result = new SimpleResult<CustomerUserObjEntity>()
            {
                Success=false,
                Msg=""
            };

            var keyCode = CacheManager.Get(dto.codeCacheKey);
            if (!CacheManager.KeyExists(dto.codeCacheKey))
            {
                result.Msg = "验证码过期请刷新验证码重试!";
                return result;
            }

            if (!keyCode.ToString().Equals(dto.code))
            {
                result.Msg = "验证码错误!";
                return result;
            }
             
            if (dto == null || string.IsNullOrEmpty(dto.username) || string.IsNullOrEmpty(dto.password) )
            {
                result.Msg = "用户名和密码不能为空";
                return result;
            }  

            var password= EncryptHelper.MD5Encrypt(dto.password); 
            var model=DbUtility.GetSingle<CustomerUserObjEntity>(r=>r.password== password&&r.name==dto.username);

            if (model == null)
            {
                result.Msg = "当前用户不存在";
                return result;
            }
            result.Success = true;
            result.Msg = "登录成功";
            result.ReturnData = model;
            return result; 
        }
    }
}
