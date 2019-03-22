using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 验证号码有效帮助类
    /// </summary>
    public class NumberHelper
    {
        /// <summary>
        /// 验证是否是手机号
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public static bool ValidPhoneNumber(string phoneNumber)
        {

            bool isPhoneNumber = false;
            if (!string.IsNullOrEmpty(phoneNumber))
            {
                if (phoneNumber.Trim().Length == 11)
                {
                    //string phoneNumberRegex = @"^1[0-9]{10}$";
                    /**
                    * 手机号码: 
                    * 13[0-9], 14[5,7], 15[0, 1, 2, 3, 5, 6, 7, 8, 9], 17[0, 1, 6, 7, 8], 18[0-9]
                    * 移动号段: 134,135,136,137,138,139,147,150,151,152,157,158,159,170,178,182,183,184,187,188
                    * 联通号段: 130,131,132,145,155,156,170,171,175,176,185,186
                    * 电信号段: 133,149,153,170,173,177,180,181,189
                    */
                    string mobileRegex = "^1(3[0-9]|4[57]|5[0-35-9]|7[0135678]|8[0-9])\\d{8}$";
                    /**
                     * 中国移动：China Mobile   2017年新加了 198 号段
                     * 134,135,136,137,138,139,147,150,151,152,157,158,159,170,178,182,183,184,187,188,[198]
                     */
                    string chinaMobileRegex = "^1(3[4-9]|4[7]|5[0-27-9]|7[08]|8[2-478]|9[8])\\d{8}$";
                    /**
                     * 中国联通：China Unicom   2017年新加了 166 号段
                     * 130,131,132,145,155,156,[166],170,171,175,176,185,186
                     */
                    string chinaUnicomRegex = "^1(3[0-2]|4[5]|6[6]|5[56]|7[0156]|8[56])\\d{8}$";
                    /**
                     * 中国电信：China Telecom  2017年新加了 199 号段
                     * 133,149,153,170,173,177,180,181,189,[199]
                     */
                    string chinaTelecomRegex = "^1(3[3]|4[9]|53|7[037]|8[019]|9[9])\\d{8}$";

                    /* 用户中心电话规则*/
                    string usercenterTelecomRegex = "^1[3456789][0-9]{9}$";

                    isPhoneNumber = Regex.IsMatch(phoneNumber, mobileRegex)
                                  || Regex.IsMatch(phoneNumber, chinaMobileRegex)
                                  || Regex.IsMatch(phoneNumber, chinaUnicomRegex)
                                  || Regex.IsMatch(phoneNumber, chinaTelecomRegex)
                                  || Regex.IsMatch(phoneNumber, usercenterTelecomRegex);
                }
            }
            return isPhoneNumber;
        }
        /// <summary>
        /// 验证是否是固定电话
        /// </summary>
        /// <param name="FixedPhoneNumber"></param>
        /// <returns></returns>
        public static bool ValidFixedPhoneNumber(string FixedPhoneNumber)
        {
            bool isFixedPhoneNumber = false;
            if (!string.IsNullOrEmpty(FixedPhoneNumber))
            {
                //固定电话号码正则        
                string FixedPhone = @"^(0[0-9]{2,3}\-?)?([2-9][0-9]{6,7})+(\-[0-9]{1,4})?$";
                Regex dReg = new Regex(FixedPhone);

                isFixedPhoneNumber = dReg.IsMatch(FixedPhoneNumber);
            }
            return isFixedPhoneNumber;
        }
        /// <summary>
        /// 验证是否是数字
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsNumber(string number)
        {
            bool isNumber = false;
            if (!string.IsNullOrEmpty(number))
            {
                string numberRegex = @"^[0-9]*$";
                isNumber = Regex.IsMatch(number, numberRegex);
            }
            return isNumber;
        }
    }
}
