using Store_FrameWork.BaseData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunStore_Web.BaseData
{
    public class TableManager
    {
        /// <summary>
        /// 表管理
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetTableName(string name)
        {
            switch (name)
            {
                case " ":
                    return string.Empty;
                case BaseTable.Users:
                    return BaseTable.Users;
                default:
                    return string.Concat("tb_", name);
            }
        }
    }
}
