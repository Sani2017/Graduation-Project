using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 

namespace Common.Constant
{
    /// <summary>
    /// 表示当前Action请求为一个具体的功能页面
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class FunctionViewAttribute : Attribute
    {
        /// <summary>
        /// 功能名称
        /// </summary>
        public string FunName { get; set; }

        /// <summary>
        /// 操作类别
        /// </summary>
        public OperationType OperationType { get; set; }

        /// <summary>
        /// 是否功能页面（标识可以直接访问的页面（HTML页面））
        /// </summary>
        public bool IsFunView { get; set; }


        public FunctionViewAttribute(string FunName, OperationType OperationType)
        {
            this.IsFunView = false;
            this.FunName = FunName;
            this.OperationType = OperationType;
        }
    }
}