using Common;
using DAL;
using Models.ModelTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BLL
{
    public class OnlineBLL
    {
        OnlineDAL dt = new OnlineDAL();
        /// <summary>
        /// 根据留言地点的id查询留言信息与条数
        /// </summary>
        /// <param name="placeId"></param>
        /// <returns></returns>
        public object GetOnlineInfoByPlaceIdBLL(int placeId)
        {
            return dt.GetOnlineInfoByPlaceId(placeId);
        }
        /// <summary>
        /// 添加留言信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public object AddOnlineInfoBLL(OnlineModel model)
        {
            return dt.AddOnlineInfo(model);
        }
        /// <summary>
        /// 增加留言的点赞量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object UpdatOnlineLikesBll(int id)
        {
            return dt.UpdatOnlineLikes(id);
        }
        /// <summary>
        /// 通过留言表id删除留言表与回复表的相关内容
        /// </summary>
        /// <param name="id">留言id</param>
        /// <returns></returns>
        public object DelOnlineInfoBll(int id)
        {
            return dt.DelOnlineInfo(id);
        }

    }
   
}
