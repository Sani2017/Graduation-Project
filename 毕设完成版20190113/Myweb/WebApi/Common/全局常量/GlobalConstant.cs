using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 全局常量信息
    /// </summary>
    public sealed class GlobalConstant
    {//可添加如短信信息，微信什么的
        #region 路径相关配置
        /// <summary>
        /// 用户头像
        /// </summary>
        public static string USER_PHOTO_DIR
        {
            get
            {
                return "/upload/userImg/0";
            }
        }
        #endregion
    }
}
