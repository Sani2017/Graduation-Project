using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Org.Common
{
    /// <summary>
    /// 属性描述
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
    public class EnumDescriptionAttribute : Attribute
    {
        /// <summary>
        /// 枚举描述内容
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// 分组
        /// </summary>
        public string Group { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark { get; set; }

        public EnumDescriptionAttribute() { }

        /// <summary>
        /// 指定枚举文本的构造函数
        /// </summary>
        /// <param name="text">枚举文本</param>
        public EnumDescriptionAttribute(string text)
        {
            this.Text = text;
        }

        /// <summary>
        /// 指定枚举文本和排序
        /// </summary>
        /// <param name="text">枚举文本</param>
        /// <param name="order">排列序号</param>
        public EnumDescriptionAttribute(string text, int order)
            : this(text)
        {
            this.Order = order;
        }

        /// <summary>
        /// 指定枚举文本、排列序号、分组
        /// </summary>
        /// <param name="text"></param>
        /// <param name="order"></param>
        /// <param name="group"></param>
        public EnumDescriptionAttribute(string text, int order, string group)
            : this(text, order)
        {
            this.Group = group;
        }
    }
}
