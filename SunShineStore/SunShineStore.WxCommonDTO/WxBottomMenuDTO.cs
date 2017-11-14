using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunShineStore.WxCommonDTO
{
    public class ReturnMenuDTO
    {

        public List<WxBottomMenuDTO> FirstMenu { get; set; }
        public List<WxBottomMenuDTO> TwoMenu { get; set; }
        public List<WxBottomMenuDTO> ThreeMenu { get; set; }
    }

    public  class WxBottomMenuDTO
    {
        /// <summary>
        /// 标识
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 菜单名
        /// </summary>
        public string Menutext { get; set; }
        /// <summary>
        /// URL
        /// </summary>
        public string MenuUrl { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortIndex { get; set; }
         
    }
}
