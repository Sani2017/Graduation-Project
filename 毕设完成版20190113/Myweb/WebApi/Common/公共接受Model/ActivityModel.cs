using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ActivityModel
    {
        /// <summary>
        /// model主键ID
        /// </summary>
        //[Required]
        public int Id { get; set; }
        /// <summary>
        /// 活动封面
        /// </summary>
        public string ActivityImg { get; set; }
        /// <summary>
        /// 活动名称
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string ActivityName { get; set; }
        /// <summary>
        /// 活动简介255
        /// </summary>
        [Required]
        [StringLength(255, MinimumLength = 10)]
        public string ActivityContent { get; set; }
        /// <summary>
        /// 启用状态（1.启用，0.禁用）默认1
        /// </summary>
        public int ActivityState{get;set;}
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
        public int LikesCount { get; set; }
        /// <summary>
        /// 浏览量
        /// </summary>
        public int Hits { get; set; }
    }
}
