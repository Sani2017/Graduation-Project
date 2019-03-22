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
    /// 对应数据库的Activity（活动）表
    /// </summary>
    [SugarTable("Activity")]    
    public class Activity
    {
        /// <summary>
        /// 活动id
        /// </summary>
        public int Id{get;set;}
        /// <summary>
        /// 活动封面
        /// </summary>
        public string ActivityImg { get; set; }
        /// <summary>
        /// 活动名称
        /// </summary>
        //[Required(ErrorMessage = "活动名称是必需的.")]
        public string ActivityName{get;set;}
        /// <summary>
        /// 活动内容
        /// </summary>
        //[Required(ErrorMessage = "活动内容是必需的.")]
        public string ActivityContent { get; set; }
        /// <summary>
        /// 启用状态（1.启用，0.禁用）默认1
        /// </summary>
        public int ActivityState { get; set; }
        /// <summary>
        /// 活动发布时间
        /// </summary>
        public DateTime? ActivityDate { get; set; }
        /// <summary>
        /// 活动结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 赞数
        /// </summary>
        public int LikesCount{ get; set; }
        /// <summary>
        /// 浏览量
        /// </summary>
        public int Hits { get; set; }
    }
}
