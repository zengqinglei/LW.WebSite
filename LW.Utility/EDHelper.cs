using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Security.Cryptography;
using System.Globalization;
using System.IO;

namespace LW.Utility
{
    /// <summary>
    /// 加解密帮助类:
    /// MD5--加密
    /// DES,AES--加解密
    /// </summary>
    public class EDHelper
    {
        #region MD5加密
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str">欲加密的明文</param>
        /// <returns>加密后的密文</returns>
        public static string MD5Encrypt(string str)
        {
            string ret = string.Empty;

            System.Security.Cryptography.MD5 md5 = new MD5CryptoServiceProvider();

            byte[] data = System.Text.Encoding.Default.GetBytes(str);
            byte[] result = md5.ComputeHash(data);

            for (int i = 0; i < result.Length; i++)
            {
                ret += result[i].ToString("x").PadLeft(2, '0');
            }

            return ret;
        }
        #endregion

        #region DES加密/解密
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="str">欲加密的明文</param>
        /// <param name="key">密钥（转成UTF-8码后的长度必须为8）</param>
        /// <param name="vector">向量（转成UTF-8码后的长度必须为8）</param>
        /// <returns>加密后的密文</returns>
        public static string DESEncode(string str, string key = "12345678", string vector = "ABCDEFGH")
        {
            try
            {
                byte[] bkey = Encoding.UTF8.GetBytes(key);
                if (bkey.Length != 8)
                {
                    throw new Exception("DES加密出错：密钥字符串长度必须为8");
                }
                byte[] bvector = Encoding.UTF8.GetBytes(vector);
                if (bvector.Length != 8)
                {
                    throw new Exception("DES加密出错：初始化向量字符串长度必须为8");
                }
                byte[] data = Encoding.UTF8.GetBytes(str);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                des.Key = bkey;
                des.IV = bvector;
                ICryptoTransform desCrypt = des.CreateEncryptor();//加密
                byte[] result = desCrypt.TransformFinalBlock(data, 0, data.Length);
                return BitConverter.ToString(result).Replace("-", "");
            }
            catch (Exception e)
            {
                throw new Exception("DES加密出错：" + e.Message);
            }
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="str">欲解密的明文</param>
        /// <param name="key">密钥（转成UTF-8码后的长度必须为8）</param>
        /// <param name="vector">向量（转成UTF-8码后的长度必须为8）</param>
        /// <returns>解密后的密文</returns>
        public static string DESDecode(string str, string key = "12345678", string vector = "ABCDEFGH")
        {
            try
            {
                byte[] bkey = Encoding.UTF8.GetBytes(key);
                if (bkey.Length != 8)
                {
                    throw new Exception("DES解密出错：密钥字符串长度必须为8");
                }
                byte[] bvector = Encoding.UTF8.GetBytes(vector);
                if (bvector.Length != 8)
                {
                    throw new Exception("DES解密出错：初始化向量字符串长度必须为8");
                }

                byte[] data = new byte[str.Length / 2];
                for (int i = 0; i < str.Length; i += 2)
                {
                    data[i / 2] = byte.Parse(str.Substring(i, 2), NumberStyles.HexNumber);
                }

                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                des.Key = bkey;
                des.IV = bvector;
                ICryptoTransform desCrypt = des.CreateDecryptor();//解密
                byte[] result = desCrypt.TransformFinalBlock(data, 0, data.Length);
                return Encoding.UTF8.GetString(result);
            }
            catch (Exception e)
            {
                throw new Exception("DES解密出错：" + e.Message);
            }
        }
        #endregion

        #region AES加密/解密 128 CBC PKCS7
        public static string AESEncrypt(string str, string key = "1234567891234567")
        {
            //Set up the encryption objects
            using (AesCryptoServiceProvider acsp = GetProvider(Encoding.Default.GetBytes(key)))
            {
                byte[] sourceBytes = Encoding.ASCII.GetBytes(str);
                ICryptoTransform ictE = acsp.CreateEncryptor();

                //Set up stream to contain the encryption
                MemoryStream msS = new MemoryStream();

                //Perform the encrpytion, storing output into the stream
                CryptoStream csS = new CryptoStream(msS, ictE, CryptoStreamMode.Write);
                csS.Write(sourceBytes, 0, sourceBytes.Length);
                csS.FlushFinalBlock();

                //sourceBytes are now encrypted as an array of secure bytes
                byte[] encryptedBytes = msS.ToArray(); //.ToArray() is important, don't mess with the buffer

                //return the encrypted bytes as a BASE64 encoded string
                return Convert.ToBase64String(encryptedBytes);
            }
        }
        public static string AESDecrypt(string str, string key = "1234567891234567")
        {
            //Set up the encryption objects
            using (AesCryptoServiceProvider acsp = GetProvider(Encoding.Default.GetBytes(key)))
            {
                byte[] RawBytes = Convert.FromBase64String(str);
                ICryptoTransform ictD = acsp.CreateDecryptor();

                //RawBytes now contains original byte array, still in Encrypted state

                //Decrypt into stream
                MemoryStream msD = new MemoryStream(RawBytes, 0, RawBytes.Length);
                CryptoStream csD = new CryptoStream(msD, ictD, CryptoStreamMode.Read);
                //csD now contains original byte array, fully decrypted

                //return the content of msD as a regular string
                return (new StreamReader(csD)).ReadToEnd();
            }
        }
        private static AesCryptoServiceProvider GetProvider(byte[] key)
        {
            AesCryptoServiceProvider result = new AesCryptoServiceProvider();
            result.BlockSize = 128;
            result.KeySize = 128;
            result.Mode = CipherMode.CBC;
            result.Padding = PaddingMode.PKCS7;

            result.GenerateIV();
            result.IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            byte[] RealKey = GetKey(key, result);
            result.Key = RealKey;
            // result.IV = RealKey;
            return result;
        }
        private static byte[] GetKey(byte[] suggestedKey, SymmetricAlgorithm p)
        {
            byte[] kRaw = suggestedKey;
            List<byte> kList = new List<byte>();

            for (int i = 0; i < p.LegalKeySizes[0].MinSize; i += 8)
            {
                kList.Add(kRaw[(i / 8) % kRaw.Length]);
            }
            byte[] k = kList.ToArray();
            return k;
        }
        #endregion
    }
}
