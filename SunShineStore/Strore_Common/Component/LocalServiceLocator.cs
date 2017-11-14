using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strore_Common.Component
{
    /// <summary>
    /// 获取特定服务
    /// </summary>
    public class LocalServiceLocator
    {
        /// <summary>
        /// 获取服务
        /// </summary>
        /// <typeparam name="T">接口类型</typeparam>
        /// <returns></returns>
        public static T GetService<T>()
            where T : class
        {

            try
            {
                T instance = StoreFramework.Container.GetInstance<T>();
                return instance;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        /// <summary>
        /// 解析注册为<typeparamref name="T"/>类型的所有服务
        /// </summary>
        /// <typeparam name="T">服务注册类型</typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ResolveAll<T>()
            where T : class
        {
            try
            {
                var instances = StoreFramework.Container.GetAllInstances<T>();
                return instances;
            }
            catch (Exception ex)
            {  
                throw;
            }
        }
    }
}
