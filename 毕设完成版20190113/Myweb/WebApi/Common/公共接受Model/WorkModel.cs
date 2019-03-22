using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class WorkModel
    {
        /// <summary>
        /// model主键ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 作品封面
        /// </summary>
        public string WorkImg { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [Required(ErrorMessage = "标题不能为空.")]
        [StringLength(20, ErrorMessage = "标题最长为20字符")]
        public string Title { get; set; }
        /// <summary>
        /// 内容简介
        /// </summary>
        [Required(ErrorMessage = "内容简介不能为空.")]
        //[StringLength(20, ErrorMessage = "标题最长为20字符")]
        public string Content { get; set; }
        /// <summary>
        /// 文件地址
        /// </summary>
        public string FileAddress { get; set; }
        /// <summary>
        /// 活动ID
        /// </summary>
        public int ActivityId { get; set; }
        /// <summary>
        /// 作者ID
        /// </summary>
        public int AuthorId { get; set; }
        /// <summary>
        /// 作者姓名
        /// </summary>
        public string AuthorName { get; set; }
        /// <summary>
        /// 作品类型
        /// </summary>
        [Required(ErrorMessage = "作品类别不能为空.")]
        public int Sort { get; set; }
        /// <summary>
        /// 创建时间（用户创建）
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// 发布时间（管理员发布）
        /// </summary>
        [Display(Name = "发布时间")]
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
        /// 作品点击量
        /// </summary>
        public int Hits { get; set; }
    }
}
