using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 用户中心登录用户信息
    /// </summary>
    public class LoginUserInfo
    {
        /// <summary>
        /// 用户名 
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 登录Token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// token時間加密
        /// </summary>
        public string ttime { get; set; }
        /// <summary>
        /// 登录权限
        /// </summary>
        public int LoginRight { get; set; }
        /// <summary>
        /// 用户头像地址（待定！）
        /// </summary>
        public string PhotoPath { get; set; }
    }
}
