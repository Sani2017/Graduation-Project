using BLL;
using Models;
using Common.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Models.ModelTemplate;
using DAl.Models;
using Common;


namespace WebApi.Controllers
{
    public class OnlineController : ApiController
    {
        OnlineBLL dt = new OnlineBLL();
        /// <summary>
        /// 留言地点id输出留言内容与条数
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("Get", "Options")]
        [FunctionView("留言地点id输出留言内容与条数", OperationType.RETRIEVE)]
        [Route("api/Online/GetOnlineInfoByPlaceId")]
        public object GetOnlineInfoByPlaceId(int placeId)//List<post>JsonResult
        {
           return dt.GetOnlineInfoByPlaceIdBLL(placeId);
        }
        /// <summary>
        /// 用户添加留言信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AcceptVerbs("post", "Options")]
        [FunctionView("用户添加留言信息", OperationType.CREATE)]
        [Route("api/Online/AddOnlineInfo")]
        public object AddOnlineInfo(OnlineModel model)
        {
            TMessage<OnlineModel> mes = new TMessage<OnlineModel>();
            if (!string.IsNullOrWhiteSpace(model.OnlineContent))
            {
                return dt.AddOnlineInfoBLL(model);
            }
            else {
                mes.suc = false;
                mes.mes = ConstHelper.NOT_NULL_ELEMENT_CONTENT;
                return mes;
            }
        }
        /// <summary>
        /// 增加留言的点赞量
        /// </summary>
        /// <param name="id">留言的id</param>
        /// <returns></returns>
        [AcceptVerbs("get", "Options")]
        [FunctionView("增加留言的点赞量", OperationType.UPDATE)]
        [Route("api/Online/UpdatOnlineLikes")]
        public object UpdatOnlineLikes(int id)
        {
            return dt.UpdatOnlineLikesBll(id);
        }

        /// <summary>
        /// 通过留言表id删除留言表与回复表的相关内容
        /// </summary>
        /// <param name="id">留言id</param>
        /// <returns></returns>
        [AcceptVerbs("get", "Options")]
        [FunctionView(" 通过留言表id删除留言表与回复表的相关内容", OperationType.DELETE)]
        [Route("api/Online/DelOnlineInfo")]
        public object DelOnlineInfo(int id)//所以说这个功能还要识别是否是楼主，是才能有删除功能，还有留言人也有此功能。有点晕~~~~
        {
            return dt.DelOnlineInfoBll(id);
        }

    }
}
