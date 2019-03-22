using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common 
{
    public class SortModel
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Required(ErrorMessage = "主键ID是必需的.")]
        public int Id { get; set; }
        /// <summary>
        /// 活动名称
        /// </summary>
        [Required(ErrorMessage = "活动名称是必需的.")]
        public string SortName { get; set; }
        /// <summary>
        /// 类别下作品总数
        /// </summary>
        public int SortWorkCount { get; set; }

    }
}
