using Store.BaseDTO.WXDTO;
using SunShineStore.WxCommonDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunShineStore.WxCommonService.Interface
{ 
    public interface IWxBottomMenuServices
    {
        /// <summary>
        /// 更新底部菜单
        /// </summary>
        WxBaseDTO UpdateBottomMen();
        /// <summary>
        /// 删除底部菜单
        /// </summary>
        WxBaseDTO DeleteBottomMenu();
         /// <summary>
        /// 获取Menu数据
        /// </summary>
        /// <returns></returns>
        ReturnMenuDTO GetMenuData();
        /// <summary>
        /// 更新Menu数据
        /// </summary>
        /// <returns></returns>
        WxBaseDTO UpdateMenuData(ReturnMenuDTO dto);
    }
}
