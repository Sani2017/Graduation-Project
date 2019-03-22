using DAL;
using Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Models.ModelTemplate;

namespace BLL
{
    public class ActivityBLL
    {
        ActivityDAL dt = new ActivityDAL();
        /// <summary>
        /// 新增活动BLL层
        /// </summary>
        /// <returns></returns>
        public TMessage<Activity> AddActivity(ActivityModel model)
        {
            return dt.AddActivity(model);
        }
        /// <summary>
        /// 删除活动BLL层
        /// </summary>
        /// <param name="Id">删除所用的Id</param>
        /// <returns></returns>
        public TMessage<Activity> DeleActivity(int Id) {
            return dt.DeleActivity(Id);
        }
        /// <summary>
        /// 批量删除活动信息BLL层
        /// </summary>
        /// <param name="deleBatchById">批量删除所用的Id数组</param>
        /// <returns></returns>
        public object DeleBatchActivity(int[] deleBatchById) {
            return dt.DeleBatchActivityInfo(deleBatchById);
        }
        /// <summary>
        /// 修改活动BLL层
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public TMessage<Activity> UpdateActivity(ActivityModel model)
        {
            return dt.UpdateActivity(model);
        }
                /// <summary>
        /// 根据id获取活动信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object GetActivityInfoById(int id)
        {
            return dt.GetActivityInfoById(id);
        }
        /// <summary>
        /// 获取所有活动信息BLL层
        /// </summary>
        /// <returns></returns>
        public object AllActivityInfo(int outState, int lookState, int page, int searchId)
        {
            return dt.GetActivityInfo(outState,lookState, page,searchId);
        }
        /// <summary>
        /// 查询活动表相关记录BLL层
        /// </summary>
        /// <param name="SelectName">查询的名称</param>
        /// <returns></returns>
        public object SelectActivityInfo(string SelectName, int SelectState)
        {
            return dt.GetSelectActivity(SelectName, SelectState);
        }
        /// <summary>
        /// 根据活动信息划分作品BLL层
        /// </summary>
        /// <returns></returns>
        public object GetAllActivitybyWorks() {
            return dt.GetAllActivitybyWorksInfo();
        }
                /// <summary>
        /// 通过id增加活动浏览量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object UpdatActivityHits(int id)
        {
            return dt.UpdatActivityHits(id);
        }
                /// <summary>
        /// 通过id增加活动赞数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object UpdatActivityLikesCount(int id)
        {
            return dt.UpdatActivityLikesCount(id);
        }
                /// <summary>
        /// 判断活动名称是否重复
        /// </summary>
        /// <param name="activityTitle">查询用的类型名称</param>
        /// <returns></returns>
        public bool SeleactivityName(string activityTitle)
        {
            return dt.SeleactivityName(activityTitle);
        }
    }
}
