using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class AdminModel
    {
        /// <summary>
        /// 管理员ID主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 管理员名称
        /// </summary>
        public string AdminName { get; set; }
        /// <summary>
        /// 管理员密码
        /// </summary>
        public string AdminPwd { get; set; }
        /// <summary>
        /// 管理员权限（默认:2，一般，1最高）
        /// </summary>
        public int AdminRight { get; set; }

    }
}
