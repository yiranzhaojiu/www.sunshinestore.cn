using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Strore_Common.Utility
{
    public static class EncryptHelper
    {
        #region MD5加密
        ///   <summary>
        ///   给一个字符串进行MD5加密
        ///   </summary>
        ///   <param   name="strText">待加密字符串</param>
        ///   <returns>加密后的字符串</returns> 
        ///   
        public static string MD5Encrypt(string prestr)
        {
            StringBuilder str = new StringBuilder(32); 

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(Encoding.Default.GetBytes(prestr));
            for (int i = 0; i < t.Length; i++)
            {
                str.Append(t[i].ToString("x").PadLeft(2, '0'));
            }

            return str.ToString();
        }

        #endregion

    }
}
