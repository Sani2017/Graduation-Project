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
    public class WorksBLL
    {
        WorksDAL dt = new WorksDAL();
        /// <summary>
        /// 获取所有信息BLL
        /// </summary>
        /// <returns></returns>
        public TMessage<List<Works>> AllWorks()
        {
            return dt.GetWorksInfo();
        }
        /// <summary>
        /// 可修改作品表中所有信息BLL
        /// </summary>
        /// <returns>object</returns>
        public TMessage<Works> UpdateWorksAll(WorkModel model)
        {
            return dt.UpdateWorksInfo(model);
        }
        /// <summary>
        /// 根据作者Id去获取相应的作品数量BLL
        /// </summary>
        /// <param name="AuthorId">作者Id</param>
        /// <returns></returns>
        public object GetAllWorksInfoByAuthorId(int AuthorId)
        {
            return dt.GetWorksInfoByAuthorId(AuthorId);
        }
        /// <summary>
        /// 获取点赞榜排名与用户表关联BLL
        /// </summary>
        /// <returns></returns>
        public object GetAllWorksLikesCount() {
            return dt.GetAllWorksLikesCountInfo();
        }
        /// <summary>
        /// 批量删除作品表信息BLL
        /// </summary>
        /// <param name="deleBatchById"></param>
        /// <returns></returns>
        public object DeleBatchWorksInfo(int[] deleBatchById) {
            return dt.DeleBatchWorksInfo(deleBatchById);
        }

        /// <summary>
        /// 批量软删除作品信息update
        /// </summary>
        /// <param name="Id">软删除作品id</param>
        /// <returns></returns>
        public object UpdateWorkIsDeleted(int[] Id)
        {
            return dt.UpdateWorkIsDeleted(Id);
        }
                /// <summary>
        /// 批量复原作品信息update
        /// </summary>
        /// <param name="Id">软删除作品id</param>
        /// <returns></returns>
        public object UpdateWorkrevert(int[] Id)
        {
            return dt.UpdateWorkrevert(Id);
        }
                /// <summary>
        /// 复原作品可查询show update
        /// </summary>
        /// <param name="Id">软删除作品id</param>
        /// <returns></returns>
        public object UpdateWorkShow(int Id)
        {
            return dt.UpdateWorkShow(Id);
        }
        /// <summary>
        /// 根据是否删除的状态去删除作品信息（定时任务）
        /// </summary>
        /// <param name="isDeleted">是否标识已删除[1:是，0:否],默认值:0</param>
        /// <returns></returns> async Task<int>
        public object DelWorkInfoByIsDeleted()
        {
            return dt.DelWorkInfoByIsDeleted();
        }
        /// <summary>
        /// 增加作品的点赞量
        /// </summary>
        /// <param name="id">作品的id</param>
        /// <returns></returns>
        public object UpdatWorksLikesBll(int id)
        {
            return dt.UpdatWorksLikes(id);
        }
        /// <summary>
        /// 增加作品信息Bll
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public object AddWorksInfoBll(WorkModel model)
        {
            return dt.AddWorksInfo(model);
        }

        /// <summary>
        /// 输出作品与用户信息列表，用于首页,发现页等
        /// </summary>
        /// <param name="lookState">
        /// 添加状态：
        ///     最新发布：0
        ///     赞数最多：1
        ///     浏览最多：2
        /// </param>
        /// <param name="searchValue">搜索内容</param>
        /// <param name="page">当前页数</param>
        /// <param name="pageName">页面名称</param>
        /// <param name="sortId">类型Id</param>
        /// <returns></returns>
        public object GetWorkAndUserInfoList(int lookState, string searchValue, int page, string pageName, int sortId)
        {
            return dt.GetWorkAndUserInfoList(lookState, searchValue, page, pageName,sortId);
        }
                /// <summary>
        /// 根据用户Id输出作品信息
        /// </summary>
        /// <param name="lookState">
        /// 添加状态：
        ///     最新发布：0
        ///     赞数最多：1
        ///     浏览最多：2
        /// </param>
        /// <param name="lookState">查看状态</param>
        /// <param name="page">当前页数</param>
        /// <param name="userId">类型Id</param>
        /// <returns></returns>
        public object GetWorkInfoByUserId(int lookState, int page, int userId)
        {
            return dt.GetWorkInfoByUserId(lookState, page, userId);
        }
        /// <summary>
        /// 根据用户Id输出作品信息,用于用户私人页的作品输出
        /// </summary>
        /// <param name="page">当前页数</param>
        /// <param name="userId">用户Id</param>
        /// <param name="sortId">类型Id</param>
        /// <param name="if_show">是否显示[0:否,1:是],默认值:0</param>
        /// <param name="if_Deleted">是否软删除[1:是，0:否],默认值:0</param>
        /// 全部 3:3
        /// 是否显示 0:0
        /// 已通过 1:0 
        /// 已删除 3:1
        /// <returns></returns>
        public object GetWorkInfoByUserIdForUserPersonal(int page, int userId, int sortId, int if_show, int if_Deleted)
        {
            return dt.GetWorkInfoByUserIdForUserPersonal(page, userId, sortId, if_show, if_Deleted);
        }
        /// <summary>
        /// 调用两张表（用户，作品）模糊查询名称
        /// </summary>
        /// <param name="seleName"></param>
        /// <returns></returns>
        public object SelectInputBll(string selectName)
        {
           return dt.SelectInput(selectName);
        }
        /// <summary>
        /// 用于榜单的输出。未测试
        /// </summary>
        ///  <param name="SortId">类型id，默认为0，不筛选全表无条件输出</param>
        /// <returns></returns>
        public object PopularList(int SortId) {
            return dt.PopularList(SortId);
        }
                /// <summary>
        /// 根据活动id查找活动
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public object GetWorkByActivityId(int activityId, int page, int lookState)
        {
            return dt.GetWorkByActivityId(activityId,page,lookState);
        }
                /// <summary>
        /// 根据id查找作品信息
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public object GetWorkInfoByWorkId(int workId)
        {
            return dt.GetWorkInfoByWorkId(workId);
        }
        /// <summary>
        /// 通过id增加作品浏览量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object UpdatWorksHits(int id)
        {
            return dt.UpdatWorksHits(id);
        }
        
        /// <summary>
        /// 通过id增加作品赞数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object UpdatWorksLikesCount(int id)
        {
            return dt.UpdatWorksLikesCount(id);
        }
        /// <summary>
        /// 根据作品id查询该作品是否通过审核与是否删除
        /// </summary>
        /// <param name="workId"></param>
        /// <returns></returns>
        public object outputWorkStateById(int workId) {
            return dt.outputWorkStateById(workId);
        }
    }
}
