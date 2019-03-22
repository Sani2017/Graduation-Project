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
    /// 对应数据库的Online（线上留言）表
    /// </summary>
    [SugarTable("Online")]    
    public class Online
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 留言地点id
        /// </summary>
        public int PlaceId { get; set; }
        /// <summary>
        /// 留言者id
        /// </summary>
        [Required(ErrorMessage = "留言者id是必需的.")]
        public int Uid{get;set;}
        /// <summary>
        /// 留言内容
        /// </summary>
        [Required(ErrorMessage = "留言内容是必需的.")] 
        public string OnlineContent { get; set; }
        /// <summary>
        /// 留言时间
        /// </summary>
        public DateTime? Creatime{get;set;}
        /// <summary>
        /// 留言点赞量
        /// </summary>
        public int LikesCount { get; set; }
    }
}
