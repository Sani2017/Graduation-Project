using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 用户信息修改
    /// </summary>
    public class UpdateUserInfoModel
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string ActualName { get; set; }
        /// <summary>
        /// 用户个性标签
        /// </summary>
        public string Userlabel { get; set; }
        /// <summary>
        /// 用户手机号
        /// </summary>
        public string UserPhone { get; set; }
        /// <summary>
        /// 用户邮箱（具体邮箱待定）
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 用户头像(有默认头像)
        /// </summary>
        public string UserImg { get; set; }
    }
}
