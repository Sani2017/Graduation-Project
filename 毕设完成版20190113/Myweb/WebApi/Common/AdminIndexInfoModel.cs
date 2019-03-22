using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 管理页首页输出的各表总数
    /// </summary>
   public class AdminIndexInfoModel
    {
       /// <summary>
       /// 用户总数
       /// </summary>
       public int UserCount { get; set; }
       /// <summary>
       /// 作品总数
       /// </summary>
       public int WorkCount { get; set; }
       /// <summary>
       /// 单人作品数最多
       /// </summary>
       public int UserForWorkCount { get; set; }
       /// <summary>
       /// 最多作品用户信息
       /// </summary>
       public List<UserInfoAndWorkSum> MaxWorkCountUserInfo { get; set; }
   }
}
