using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 用于榜单输出列表
    /// </summary>
    public class OutPopularListMode
    {
        /// <summary>
        /// 用户ID主键
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户头像(有默认头像)
        /// </summary>
        public string UserImg { get; set; }

        /// <summary>
        /// 作品ID
        /// </summary>
        public int WorkId { get; set; }
        /// <summary>
        /// 作品作者id
        /// </summary>
        //public int WorkByUserId { get; set; }
        /// <summary>
        /// 作品封面
        /// </summary>
        public string WorkImg { get; set; }
        /// <summary>
        /// 作品标题
        /// </summary>
        public string WorkTitle { get; set; }
        /// <summary>
        /// 作品类型
        /// </summary>
        public string WorkSort { get; set; }
        /// <summary>
        /// 发布时间（管理员发布）
        /// </summary>
        public DateTime PublishedAt { get; set; }
        /// <summary>
        /// 点赞量
        /// </summary>
        public int LikesCount { get; set; }
        /// <summary>
        /// 作品点击量
        /// </summary>
        public int Hits { get; set; }
        /// <summary>
        /// 作品总得分
        /// </summary>
        public float TotalScore { get; set; }
        /// <summary>
        /// 作品总数
        /// </summary>
        public int WorksCount { get; set; }
    }
}
