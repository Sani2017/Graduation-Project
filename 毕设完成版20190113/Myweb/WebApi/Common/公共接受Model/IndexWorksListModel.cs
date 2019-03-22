using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 用于类似首页作品列表输出
    /// </summary>
    public class IndexWorksListModel
    {
        /// <summary>
        /// 作品封面
        /// </summary>
        public string WorkImg{get;set;}
        /// <summary>
        /// 作品id
        /// </summary>
        public int WorksId {get;set;} 
        /// <summary>
        /// /标题
        /// </summary>
        public string WorksTitle {get;set;}
        /// <summary>
        /// 作品内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 点赞量
        /// </summary>
        public int LikesCount {get;set;}
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime PublishedAt {get;set;}
        /// <summary>
        /// 创建时间（用户创建）
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// 附件地址
        /// </summary>
        public string FileAddress { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId {get;set;}
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName {get;set;}
        /// <summary>
        /// 用户头像
        /// </summary>
        public string UserImg {get;set;}
        /// <summary>
        /// 作品分类
        /// </summary>
        public string WorksSort { get; set; }
        /// <summary>
        /// 時間差
        /// </summary>
        public string TimeDifference { get; set; }
        /// <summary>
        /// 作品点击浏览量
        /// </summary>
        public int Hits { get; set; }
        /// <summary>
        /// 用户签名
        /// </summary>
        public string Userlabel { get; set; }
    }
}
