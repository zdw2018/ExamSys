using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class PassWordHelper
    {
        /// <summary>
        /// 对字符串进行MD5加密,加密后不能解密。
        /// </summary>
        /// <param name="s">要进行MD5加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string GetMD5(string s)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(Encoding.GetEncoding("gb2312").GetBytes(s));
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }
    }
}
