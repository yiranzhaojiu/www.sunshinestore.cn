using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strore_Data
{
    public class DbFactory
    {
        /// <summary>
        /// 定义当前数据库语言
        /// </summary>
        public static IOrmLiteDialectProvider CurrentDialectProvider { get { return SqlServerDialect.Provider; } }

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static DbFactory()
        {
            OrmLiteConfig.DialectProvider = DbFactory.CurrentDialectProvider; //SQLServer 引擎
            //AosNamingStrategy 实现了 INamingStrategy，目前在于更换表明，分表时的获取方法
            OrmLiteConfig.DialectProvider.NamingStrategy = new AosNamingStrategy();
        }
    }
}
