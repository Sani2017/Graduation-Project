using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
using System.ComponentModel.DataAnnotations;//神奇的注释插件

namespace Models.ModelTemplate
{
    /// <summary>
    /// 对应数据库的Reply（回复）表
    /// </summary>
    [SugarTable("Reply")]    
    public class Reply
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 留言id
        /// </summary>
        [Required(ErrorMessage = "留言者id是必需的.")]
        public int OnlineId { get; set; }
        /// <summary>
        /// 回复人id（用户之间直接用id，用户与管理员之间给管理员‘a+ID’）
        /// </summary>
        [Required(ErrorMessage = "留言者id是必需的.")]
        public int RUid { get; set; }
        /// <summary>
        /// 回复内容
        /// </summary>
        [Required(ErrorMessage = "留言者id是必需的.")]
        public string Recontent{get;set;}
        /// <summary>
        /// 回复时间
        /// </summary>
        public DateTime? Retime { get; set; }

    }
}
