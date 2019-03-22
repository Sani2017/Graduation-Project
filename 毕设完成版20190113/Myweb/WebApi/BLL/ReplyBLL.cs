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
    public class ReplyBLL
    {
        ReplyDAL dt = new ReplyDAL();
        /// <summary>
        /// 添加回復信息
        /// </summary>
        /// <returns></returns>
        public object AddReplyInfoBll(ReplyModel model)
        {
            return dt.AddReplyInfo(model);
        }

            /// <summary>
        /// 查询回复内容根据留言id
        /// </summary>
        /// <param name="OnlineId"></param>
        /// <returns></returns>

        public object GetReplyInfoByOnlineIdBll(int placeId) 
        {
            return dt.GetReplyInfoByOnlineId(placeId);
        }
        /// <summary>
        /// 通过id删除单条回复表内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object DelReplyInfo(int id)
        {
            return dt.DelReplyInfo(id);
        }
    }
}
