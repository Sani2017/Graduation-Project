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
    /// 对应数据库的Works（作品）表
    /// </summary>
    [SugarTable("Works")]    
    public class Works
    {
        /// <summary>
        /// 作品主键ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 作品封面
        /// </summary>
        public string WorkImg { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [Required(ErrorMessage = "标题是必需的.")]
        [StringLength(20, ErrorMessage = "标题最长为20字符")]
        public string Title { get; set; }
        /// <summary>
        /// 内容简介
        /// </summary>
        [Required(ErrorMessage = "内容简介是必需的.")]
        //[StringLength(20, ErrorMessage = "标题最长为20字符")]
        public string Content { get; set; }
        /// <summary>
        /// 文件地址
        /// </summary>
        public string FileAddress { get; set; }
        /// <summary>
        /// 作者ID
        /// </summary>
        [Required(ErrorMessage = "作者id是必需的.")]
        public int AuthorId { get; set; }
        /// <summary>
        /// 活动ID
        /// </summary>
        public int ActivityId { get; set; }
        /// <summary>
        /// 作品类型
        /// </summary>
        [Required(ErrorMessage = "作品类别是必需的.")]
        public int Sort { get; set; }
        /// <summary>
        /// 作者姓名
        /// </summary>
        //[Required(ErrorMessage = "上传人用户名是必需的.")]
        public string AuthorName { get; set; }
        /// <summary>
        /// 创建时间（用户创建）
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// 发布时间（管理员发布）
        /// </summary>
        public DateTime PublishedAt { get; set; }
        /// <summary>
        /// 是否标识已删除[1:是，0:否],默认值:0
        /// </summary>
        public int IsDeleted { get; set; }
        /// <summary>
        /// 是否允许展示[0:否,1:是],默认值:0
        /// </summary>
        public int AllowShow { get; set; }
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
