using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class UserInfoAndWorkSum
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string UserImg { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户作品总数
        /// </summary>
        public int UserWorksum { get; set; }
        /// <summary>
        /// 用户个性标签
        /// </summary>
        public string Userlabel { get; set; }
        /// <summary>
        /// 用户状态(1.启用，0.不启用)默认1
        /// </summary>
        public int UserState { get; set; }
        /// <summary>
        /// 用户作品封面
        /// </summary>
        public List<UserWorkImg> UserWorkImg { get; set; }
    }
}
