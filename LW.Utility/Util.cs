using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace LW.Utility
{
    public class Util
    {
        #region 生成数字验证码
        /// <summary>
        /// 默认生成6位数字验证码
        /// </summary>
        /// <param name="length"></param>
        public static string GetRandomNumber(int length = 6)
        {
            string code = string.Empty; ;
            for (int i = 0; i < length; i++)
            {
                code += new Random(GetRandomSeed()).Next(0, 9).ToString();
            }
            return code;
        }
        /// <summary>
        /// 获取随机的Seed数，保证生成的随机数不重复
        /// </summary>
        /// <returns></returns>
        private static int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
        #endregion

        #region 生成n位随机验证码
        /// <summary>
        /// 生成n位随机验证码算法
        /// </summary>
        /// <param name="n">生成随机验证的位数</param>
        /// <returns>返回生成的验证码</returns>
        public static string GetRandomAlphanumeric(int n = 4)
        {
            int number;
            char code;
            string StrCode = String.Empty;

            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                number = random.Next();

                if (number % 2 == 0)
                    code = (char)('0' + (char)(number % 10));
                else
                    code = (char)('A' + (char)(number % 26));
                StrCode += code.ToString();
            }

            return StrCode;
        }
        #endregion

        #region 中英文字符串等长截取
        /// <summary>
        /// 中英文字符串等长截取
        /// </summary>
        /// <param name="str"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string CutString(string str, int len)
        {
            if (String.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            int strlen = str.Length;

            #region 计算长度
            int cutlen = 0;
            while (cutlen < len && cutlen < strlen)
            {
                if ((int)str[cutlen] > 128)
                {
                    len--;
                }
                cutlen++;
            }
            #endregion

            if (cutlen < strlen)
            {
                return str.Substring(0, cutlen) + " ...";
            }
            else
            {
                return str;
            }
        }
        #endregion
    }
}
