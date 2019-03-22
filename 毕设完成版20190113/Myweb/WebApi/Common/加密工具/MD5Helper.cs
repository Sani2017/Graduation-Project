using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// MD5加密帮助类
    /// </summary>
    public class MD5Helper
    {
        //public static string MD5(string encryptString)
        //{
        //    byte[] result = Encoding.Default.GetBytes(encryptString);
        //    MD5 md5 = new MD5CryptoServiceProvider();
        //    byte[] output = md5.ComputeHash(result);
        //    string encryptResult = BitConverter.ToString(output).Replace("-", "");
        //    return encryptResult;
        //}

        public static string GetMd5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(str);
            byte[] md5data = md5.ComputeHash(data);
            md5.Clear();
            string out_Str = "";
            for (int i = 0; i < md5data.Length; i++)
            {
                out_Str += md5data[i].ToString("x").PadLeft(2, '0');
            }
            return out_Str;
        }



        #region Base64加密解密
        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <returns></returns>
        public static string Base64Encrypt(string input)
        {
            return Base64Encrypt(input, new UTF8Encoding());
        }

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <param name="encode">字符编码</param>
        /// <returns></returns>
        public static string Base64Encrypt(string input, Encoding encode)
        {
            return Convert.ToBase64String(encode.GetBytes(input));
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="input">需要解密的字符串</param>
        /// <returns></returns>
        public static string Base64Decrypt(string input)
        {
            return Base64Decrypt(input, new UTF8Encoding());
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="input">需要解密的字符串</param>
        /// <param name="encode">字符的编码</param>
        /// <returns></returns>
        public static string Base64Decrypt(string input, Encoding encode)
        {
            return encode.GetString(Convert.FromBase64String(input));
        }

        #endregion

        /// <summary>
        /// 对字符串SHA1加密
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="encoding">编码类型</param>
        /// <returns>加密后的十六进制字符串</returns>
        public static string Sha1Encrypt(string source, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;

            // 第一种方式
            byte[] byteArray = encoding.GetBytes(source);
            using (HashAlgorithm hashAlgorithm = new SHA1CryptoServiceProvider())
            {
                byteArray = hashAlgorithm.ComputeHash(byteArray);
                StringBuilder stringBuilder = new StringBuilder(256);
                foreach (byte item in byteArray)
                {
                    stringBuilder.AppendFormat("{0:x2}", item);
                }
                hashAlgorithm.Clear();
                return stringBuilder.ToString();
            }
        }

        public static string GetSHA256HashFromString(string strData)
        {
            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(strData);
            try
            {
                SHA256 sha256 = new SHA256CryptoServiceProvider();
                byte[] retVal = sha256.ComputeHash(bytValue);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetSHA256HashFromString() fail,error:" + ex.Message);
            }
        }


    }
}
