using Store.BaseDTO;
using Store.BaseDTO.WXDTO;
using Strore_Common.Component;
using SunShineStore.Customer.Interface;
using SunShineStore.CustomerDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SunShineStoreApi.Controllers.OtherPrj
{
    public class OtherPrjUserController : ApiController
    {
        /// <summary>
        /// 新增用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        public ExecuteResult CallAdd([FromUri]UserInfoDTO model)
        {

            var services = LocalServiceLocator.GetService<IUserServices>();
            return services.UserInfoAdd(model); 
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        public ExecuteResult<List<UserInfoDTO>> UserList([FromUri]UserSerachDTO model)
        { 
            var services = LocalServiceLocator.GetService<IUserServices>();
            return  services.GetList(model);
        }

    }
}
