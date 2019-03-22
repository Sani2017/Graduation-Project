using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 回复表的输出model
    /// </summary>
    public class OutputReplyModel
    {
        /// <summary>
        /// 回复id
        /// </summary>
        public int ReplyId { get; set; }
        /// <summary>
        /// 留言id
        /// </summary>
        public int OnlineId { get; set; }
        /// <summary>
        /// 回复内容
        /// </summary>
        public string Recontent { get; set; }
        /// <summary>
        /// 回复时间
        /// </summary>
        public DateTime? Retime { get; set; }
        /// <summary>
        /// 回复用户id
        /// </summary>
        public int ReplyUserId { get; set; }
        /// <summary>
        /// 回复人姓名
        /// </summary>
        public string ReplyUserName { get; set; }
    }
}
