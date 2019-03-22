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
    /// 对应数据库的Sort（类型）表
    /// </summary>
    [SugarTable("Sort")]    
    public class Sort
    {
        /// <summary>
        /// 类型ID
        /// </summary>
        [Required(ErrorMessage = "主键ID是必需的.")]
        public int Id { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        [Required(ErrorMessage = "活动名称是必需的.")]
        public string SortName { get; set; }

    }
}
