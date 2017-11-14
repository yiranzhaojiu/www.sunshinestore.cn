using ServiceStack.OrmLite;
using SunStore_Web.BaseData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strore_Data
{
    /// <summary>
    /// AOS命名策略
    /// </summary>
    public class AosNamingStrategy : INamingStrategy
    {
        public string ApplyNameRestrictions(string name)
        {
            return name;
        }

        public string GetColumnName(string name)
        {
            return name;
        }

        public string GetSchemaName(ModelDefinition modelDef)
        {
            return GetSchemaName(modelDef.ModelName);
        }

        public string GetSchemaName(string name)
        {
            return name;
        }

        public string GetSequenceName(string modelName, string fieldName)
        {
            return "SEQ_" + modelName + "_" + fieldName;
        }

        /// <summary>
        /// 获取表名
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetTableName(ModelDefinition modelDef)
        { 
            return GetTableName(modelDef.ModelName);
        }

        /// <summary>
        /// 获取表名
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetTableName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("table-name-null");
            }
  
            var tablename = TableManager.GetTableName(name); 
            return tablename; 
        }
    }
}
