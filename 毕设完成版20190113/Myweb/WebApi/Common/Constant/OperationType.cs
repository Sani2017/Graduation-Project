using Org.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Constant
{
    /// <summary>
    /// 操作类型
    /// </summary>
    public enum OperationType
    {
        /// <summary>
        /// 注销
        /// </summary>
        [EnumDescription("注销")]
        LOGOFF = 0,
        /// <summary>
        /// 登录
        /// </summary>
        [EnumDescription("登录")]
        LOGON = 1,
        /// <summary>
        /// 查询
        /// </summary>
        [EnumDescription("查询")]
        RETRIEVE = 2,
        /// <summary>
        /// 编辑（即可用于新增编辑和修改编辑）
        /// </summary>
        [EnumDescription("编辑")]
        EDIT = 3,
        /// <summary>
        /// 新增
        /// </summary>
        [EnumDescription("新增")]
        CREATE = 4,        
        /// <summary>
        /// 修改
        /// </summary>
        [EnumDescription("更新")]
        UPDATE = 5,
        /// <summary>
        /// 删除
        /// </summary>
        [EnumDescription("删除")]
        DELETE = 6,
        /// <summary>
        /// 其他
        /// </summary>
        [EnumDescription("其他")]
        OTHER = 9
    }
}

