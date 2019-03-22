using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class EmailModel
    {
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 验证发送时间
        /// </summary>
        public string addData{get;set;}
        /// <summary>
        /// 随机验证码
        /// </summary>
        public string validataCode { get; set; }
    }
}
