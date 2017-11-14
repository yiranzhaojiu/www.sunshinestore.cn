using Strore_Common.Cache;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strore_Common.WebHelper
{
    
    /// <summary>
    /// 数学算式的验证码
    /// </summary>
    public sealed class ValidateCodeHelper
    { 
        #region 验证码
        /// <summary>
        /// 生成随机相加字符串
        /// </summary>
        /// <returns></returns>
        public static string GetCalculationCode(string cacheKey)
        {
            int mathResult = 0;
            string expression = null;

            Random rnd = new Random();

            ////生成3个10以内的整数，用来运算
            int operator1 = rnd.Next(0, 10);
            int operator2 = rnd.Next(0, 10);
            int operator3 = rnd.Next(0, 10);

            ////随机组合运算顺序，只做 + 和 * 运算
            switch (rnd.Next(0, 3))
            {
                case 0:
                    mathResult = operator1 + operator2 * operator3;
                    expression = string.Format("{0} + {1} * {2} = ?", operator1, operator2, operator3);
                    break;
                case 1:
                    mathResult = operator1 * operator2 + operator3;
                    expression = string.Format("{0} * {1} + {2} = ?", operator1, operator2, operator3);
                    break;
                default:
                    mathResult = operator2 + operator1 * operator3;
                    expression = string.Format("{0} + {1} * {2} = ?", operator2, operator1, operator3);
                    break;
            } 
            CacheManager.Set(cacheKey, mathResult, DateTime.Now.AddMinutes(5)); 
            return expression;
        }
         
        #endregion 
    }
}
