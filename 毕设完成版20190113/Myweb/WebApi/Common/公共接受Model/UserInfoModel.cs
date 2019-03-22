using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common 
{
    public class UserInfoModel
    {
        /// <summary>
        /// 用户ID主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        [Required(ErrorMessage = "用户名称是必需的.")]
        public string UserName { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        [Required(ErrorMessage = "用户密码是必需的.")]
        public string PassWord { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        [Required(ErrorMessage = "真实姓名是必需的.")]
        public string ActualName { get; set; }
        /// <summary>
        /// 用户个性标签
        /// </summary>
        public string Userlabel { get; set; }
        /// <summary>
        /// 用户手机号
        /// </summary>
        [Required(ErrorMessage = "手机号是必需的.")]
        public string UserPhone { get; set; }
        /// <summary>
        /// 用户邮箱（具体邮箱待定）
        /// </summary>
        [Required(ErrorMessage = "邮箱是必需的.")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "邮件地址不正确")]
        public string Email { get; set; }
        /// <summary>
        /// 用户头像(有默认头像)
        /// </summary>
        public string UserImg { get; set; }
        /// <summary>
        /// 用户状态(1.启用，0.不启用)默认0
        /// </summary>
        public int UserState { get; set; }
        /// <summary>
        /// 上次登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

    }
}
