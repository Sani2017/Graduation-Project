using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 留言回复的相关model
    /// </summary>
    public class OutputOnlineModel
    {
        /// <summary>
        /// 留言id
        /// </summary>
        public int OnlineId { get; set; }
        /// <summary>
        /// 留言地点id
        /// </summary>
        public int PlaceId { get; set; }
        /// <summary>
        /// 留言内容
        /// </summary>
        public string OnlineContent { get; set; }
        /// <summary>
        /// 留言时间
        /// </summary>
        public DateTime? Creatime { get; set; }
        /// <summary>
        /// 留言点赞量
        /// </summary>
        public int LikesCount { get; set; }
        /// <summary>
        /// 留言人Id
        /// </summary>
        public int OnlineUserId { get; set; }
        /// <summary>
        /// 留言人姓名
        /// </summary>
        public string OnlineUserName { get; set; }
        /// <summary>
        /// 留言人头像
        /// </summary>
        public string OnlineUserImg { get; set; }
        /// <summary>
        /// 留言下回复的条数
        /// </summary>
        public int OnlineRelistCount { get; set; }
        /// <summary>
        /// 回复列表
        /// </summary>
        public List<OutputReplyModel> ReplyList { get; set; }
    }
}
