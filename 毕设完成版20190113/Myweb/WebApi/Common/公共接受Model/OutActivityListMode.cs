using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 用于活动表输出活动列表
    /// </summary>
    public class OutActivityListMode
    {
        /// <summary>
        /// 活动ID
        /// </summary>
        public int ActivityId { get; set; }
        /// <summary>
        /// 活动封面
        /// </summary>
        public string ActivityImg { get; set; }
        /// <summary>
        /// 活动标题
        /// </summary>
        public string ActivityTitle { get; set; }
        /// <summary>
        /// 活动发布时间
        /// </summary>
        public DateTime? ActivityDate { get; set; }
        /// <summary>
        /// 活动结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 距活动结束的時間差
        /// </summary>
        public string ActivityTimeDifference { get; set; }
        /// <summary>
        /// 点赞量
        /// </summary>
        public int LikesCount { get; set; }
        /// <summary>
        /// 发布至今的時間差
        /// </summary>
        public string NewTimeDifference { get; set; }
        /// <summary>
        /// 活动点击浏览量
        /// </summary>
        public int Hits { get; set; }
        /// <summary>
        /// 参加作品数量
        /// </summary>
        public int NumberOfWorks { get; set; }
        /// <summary>
        /// 活动内容
        /// </summary>
        public string ActivityContent { get; set; }
        /// <summary>
        /// 活动状态
        /// </summary>
        public int ActivityState { get; set; }

    }
}
