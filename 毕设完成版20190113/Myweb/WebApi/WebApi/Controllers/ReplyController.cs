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
    public class ReplyController : ApiController
    {
        ReplyBLL dt = new ReplyBLL();

        /// <summary>
        /// 添加回復信息
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("post", "Options")]
        [FunctionView("添加回復信息", OperationType.RETRIEVE)]
        [Route("api/Reply/AddReplyInfo")]
        public object AddReplyInfo(ReplyModel model)
        {
            TMessage<OnlineModel> mes = new TMessage<OnlineModel>();
            if (!string.IsNullOrWhiteSpace(model.Recontent))
            {
                return dt.AddReplyInfoBll(model);
            }
            else
            {
                mes.suc = false;
                mes.mes = ConstHelper.NOT_NULL_ELEMENT_CONTENT;
                return mes;
            }
        }
        /// <summary>
        /// 查询回复内容根据留言id
        /// </summary>
        /// <param name="OnlineId"></param>
        /// <returns></returns>
        [AcceptVerbs("get", "Options")]
        [FunctionView("添加回復信息", OperationType.RETRIEVE)]
        [Route("api/Reply/GetReplyInfoByOnlineId")]
        public object GetReplyInfoByOnlineId(int placeId)
        {
            return dt.GetReplyInfoByOnlineIdBll(placeId);
        }

        /// <summary>
        /// 通过id删除单条回复表内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AcceptVerbs("get","Options")]
        [FunctionView("添加回復信息", OperationType.DELETE)]
        [Route("api/Reply/DelReplyInfo")]
        public object DelReplyInfo(int id)
        {
            return dt.DelReplyInfo(id);
        }


    }
}
