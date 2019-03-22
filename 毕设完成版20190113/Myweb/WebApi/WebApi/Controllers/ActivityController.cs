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

namespace DAl.Controllers
{
    public class ActivityController : ApiController
    {
        ActivityBLL dt = new ActivityBLL();
        /// <summary>
        /// 添加活动信息
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("post", "Options")]
        [FunctionView("添加活动信息", OperationType.CREATE)]
        [Route("api/Activity/AddActivityInfo")]
        public TMessage<Activity> AddActivityInfo(ActivityModel model)
        {
            TMessage<Activity> mes = new TMessage<Activity>();
            if(string.IsNullOrWhiteSpace(model.ActivityName)
             ||string.IsNullOrWhiteSpace(model.ActivityContent)){
                mes.suc=false;
                mes.mes = ConstHelper.NOT_NULL_ELEMENT_CONTENT;
                return mes;
            }
            return dt.AddActivity(model);
           
        }
        /// <summary>
        /// 删除活动信息
        /// </summary>
        /// <param name="model">删除活动所用的Id</param>
        /// <returns></returns>
        [AcceptVerbs("get", "Options")]
        [FunctionView("删除活动信息", OperationType.DELETE)]
        [Route("api/Activity/DeleActivityInfo")]
        public TMessage<Activity> DeleActivityInfo(int id)
        {
            TMessage<Activity> mes = new TMessage<Activity>();
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                mes.suc = false;
                mes.mes = ConstHelper.NOT_NULL_ELEMENT_CONTENT;
                return mes;
            }
            return dt.DeleActivity(id);
        }
        /// <summary>
        /// 修改活动信息
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("post", "Options")]
        [FunctionView("修改活动信息", OperationType.UPDATE)]
        [Route("api/Activity/UpdateActivityInfo")]
        public TMessage<Activity> UpdateActivityInfo(ActivityModel model)
        {
            TMessage<Activity> mes = new TMessage<Activity>();
            if (JudgeState(model.ActivityState))
            {
                if (string.IsNullOrWhiteSpace(model.ActivityName)
                    || string.IsNullOrWhiteSpace(model.ActivityContent))
                {
                    mes.suc = false;
                    mes.mes = ConstHelper.NOT_NULL_ELEMENT_CONTENT;
                    return mes;
                }
                return dt.UpdateActivity(model);
            }
            else
            {
                mes.suc = false; mes.mes = ConstHelper.PARAMETER_ERROR;
            }
            return mes;

            
        }
                /// <summary>
        /// 根据id获取活动信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Activity/GetActivityInfoById")]
        public object GetActivityInfoById(int id)
        {
            return dt.GetActivityInfoById(id);
        }
        /// <summary>
        /// 查询所有的活动表内容
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("get", "Options")]
        [FunctionView("查询所有的活动表内容", OperationType.RETRIEVE)]
        //[Route("api/Activity/GetAllActivityInfo")]
        [HttpGet]
        public object GetAllActivityInfo(int outState, int lookState, int page, int searchId)
            {
            return dt.AllActivityInfo(outState,lookState,page,searchId);
        }
        /// <summary>
        /// 模糊查询活动表相关记录
        /// </summary>
        /// <param name="SelectName">查询的名称</param>
        /// <returns></returns>
        [AcceptVerbs("get", "Options")]
        [FunctionView("查询活动表相关记录", OperationType.RETRIEVE)]
        [Route("api/Activity/GetSelectActivityInfo")]
        public object GetSelectActivityInfo(string SelectName, int SelectState)
        {
            TMessage<Activity> mes = new TMessage<Activity>();

            //if (SelectState != 0 && SelectState != 1)//判断转态是否正确
            //{
            //    mes.suc = false; mes.mes = "状态错误,没有该状态"; 
            //}
            if (JudgeState(SelectState))
            {
                return dt.SelectActivityInfo(SelectName, SelectState);
            }
            else {
                mes.suc = false; mes.mes = ConstHelper.PARAMETER_ERROR;
            }
            return mes;
            
        }

        /// <summary>
        /// 批量删除活动信息
        /// </summary>
        /// <param name="list">批量删除所用的Id数组</param>
        /// <returns></returns>
        [AcceptVerbs("post", "Options")]
        [FunctionView("批量删除活动信息", OperationType.DELETE)]
        [Route("api/Activity/DeleBatchActivity")]
        public object DeleBatchActivity(int[] deleBatchById)//List<int> XXX,int[] XXX 相同
        {
            return dt.DeleBatchActivity(deleBatchById);
        }
        /// <summary>
        /// 根据活动信息划分作品（作品与活动表的查询，涉及到多表与分页）
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("get", "Options")]
        [FunctionView("查询活动表相关记录", OperationType.RETRIEVE)]
        [Route("api/Activity/GetAllActivitybyWorks")]
        public object GetAllActivitybyWorks()
        {
            return dt.GetAllActivitybyWorks();
        }

        /// <summary>
        /// 判断活动状态是否有效
        /// </summary>
        /// <param name="SelectState">活动状态</param>
        /// <returns></returns>
        public bool JudgeState(int SelectState)
        {
            TMessage<Activity> mes = new TMessage<Activity>();

            if (SelectState != (int)Common.EnumType.StateResolution.ZeroType 
                && SelectState != (int)Common.EnumType.StateResolution.ZeroType)//判断转态是否正确
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 通过id增加活动浏览量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        [AcceptVerbs("get", "Options")]
        [Route("api/Activity/UpdatActivityHits")]
        public object UpdatActivityHits(int id)
        {
            return dt.UpdatActivityHits(id);
        }
        /// <summary>
        /// 通过id增加活动赞数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AcceptVerbs("get", "Options")]
        [Route("api/Activity/UpdatActivityLikesCount")]
        public object UpdatActivityLikesCount(int id)
        {
            return dt.UpdatActivityLikesCount(id);
        }
        /// <summary>
        /// 判断活动名称是否重复
        /// </summary>
        /// <param name="activityTitle">查询用的类型名称</param>
        /// <returns></returns>
        [AcceptVerbs("get", "Options")]
        [Route("api/Activity/SeleactivityName")]
        public bool SeleactivityName(string activityTitle)
        {
            return dt.SeleactivityName(activityTitle);
        }
    }
}
