using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 消息模型
    /// </summary>
    public class Message
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool suc { get; set; }
        /// <summary>
        /// 返回消息
        /// </summary>
        public string mes { get; set; }
        /// <summary>
        /// 用户token
        /// </summary>
        public string token { get; set; }
        /// <summary>
        /// token時間加密
        /// </summary>
        public string ttime {get;set;}

    }
}
