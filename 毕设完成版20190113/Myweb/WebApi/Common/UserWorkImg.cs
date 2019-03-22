
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class UserWorkImg
    { 
        /// <summary>
        /// 作品id
        /// </summary>
        public int WorkId { get; set; }
        /// <summary>
        /// 作者id
        /// </summary>
        public int AuthorId { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 作品头像
        /// </summary>
        public string WorksImg { get; set; }
        /// <summary>
        /// 点赞量
        /// </summary>
        public int LikesCount { get; set; }
        /// <summary>
        /// 作品点击浏览量
        /// </summary>
        public int Hits { get; set; }
    }
}
