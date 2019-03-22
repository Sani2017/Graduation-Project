using Common;
using Models;
using Models.ModelTemplate;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DAL
{
    public  class OnlineDAL
    {
        SqlSugarClient db;
        /// <summary>
        /// 与数据库帮助类关联
        /// </summary>
        public OnlineDAL() 
        {
            db = SqlSugarClientHelper.SqlSugarDB();
        }

                //这个接口废弃了，使用ReplyDAL的GetReplyInfoByOnlineId。试试一对多

        /// <summary>
        /// 根据留言地点的id查询留言信息与条数
        /// </summary>
        ///// <param name="placeId"></param>
        /// <returns></returns>
        ///         //关于留言回复的整体输出功能有大问题，从长计议
        ///         , Reply>((ol,re)=>ol.Id==re.OnlineId)
        ///         不行就拆分，先查留言内容，再通过是否有回复去查询回复输出里面拼接ajax
        ///拆开就成功，不过效率没有一对多高         
        public object GetOnlineInfoByPlaceId(int placeId)
        {
            TMessage<Online> mes = new TMessage<Online>();
            var OnlineList = db.Queryable<Online>()
                 .Select((ol) => new OutputOnlineModel
                {
                    //查询出留言所需字段
                    OnlineId = ol.Id,
                    OnlineContent = ol.OnlineContent,
                    PlaceId=ol.PlaceId,
                    Creatime = ol.Creatime,
                    LikesCount = ol.LikesCount,
                    OnlineUserId = ol.Uid,
                    OnlineUserName = SqlFunc.Subqueryable<UserInfo>().Where(ui => ui.Id == ol.Uid).Select(ui => ui.UserName),
                    OnlineUserImg = SqlFunc.Subqueryable<UserInfo>().Where(ui => ui.Id == ol.Uid).Select(ui => ui.UserImg),
                    OnlineRelistCount = SqlFunc.Subqueryable<Reply>().Where(re => re.OnlineId == ol.Id).Count(),

                })
                .Mapper((it, cache) => { 
                    if(it.OnlineRelistCount>0){
                        it.ReplyList = ReplyList(it.OnlineId);
                        //it.ReplyList = UpdatOnlineLikes(1);
                        //it.ReplyList=db.Queryable<Reply>()
                        //.Select((re) => new OutputReplyModel{
                            
                        //})
                    }
                }).OrderBy("Creatime Desc")
                .Where((ol => ol.PlaceId == placeId))
            .ToList();// .ToSql();
            if(OnlineList.Count<1){
                mes.suc = false;
                mes.mes = ConstHelper.ONLINE_ISNULL;
                return mes;
            }
            else { 
                return OnlineList;
            }
        }
            //var OnlineAndReplyList = db.Queryable<Online>()
            //    .Select
            //    ((ol) => new OutputOnlineModel
            //    {
            //        //查询出留言所需字段
            //        OnlineId = ol.Id,
            //        OnlineContent = ol.OnlineContent,
            //        Creatime = ol.Creatime,
            //        LikesCount = ol.LikesCount,
            //        OnlineUserId = ol.Uid,
            //        OnlineUserName = SqlFunc.Subqueryable<UserInfo>().Where(ui => ui.Id == ol.Uid).Select(ui => ui.UserName),
            //        OnlineRelistCount = SqlFunc.Subqueryable<Reply>().Where(re => re.OnlineId == ol.Id).Count(),
            //    })
            //   .Mapper((it, cache) =>
            //        {
            //            var allReply = cache.GetListByPrimaryKeys<Reply>(vmodel => vmodel.OnlineId);
            //            // it.ReplyList = allOnline.Where(i => i.OnlineId == it.OnlineId).ToList();
            //            it.ReplyList =
            //                allReply.Where(re => re.OnlineId == it.OnlineId)
            //                .Select((re) => new OutputReplyModel
            //                {
            //                    //查询回复所需字段
            //                    ReplyId = re.Id,
            //                    Recontent = re.Recontent,
            //                    Retime = re.Retime,
            //                    ReplyUserId = re.RUid,
            //                    //ReplyUserName = SqlFunc.Subqueryable<UserInfo>().Where(ui => ui.Id == re.RUid).Select(ui => ui.UserName),
            //                })
            //                .ToList();

            //        })
            //    // .OrderBy("Creatime desc")
            //       .ToList();// .ToSql();
            //return OnlineAndReplyList;

        /// <summary>
        /// 添加留言信息
        /// </summary>
        /// <returns></returns>
        public object AddOnlineInfo(OnlineModel model) {
            TMessage<Online> mes = new TMessage<Online>();
            Online online = new Online();
            online.PlaceId = model.PlaceId;
            online.Uid = model.Uid;
            online.OnlineContent = model.OnlineContent;
            online.Creatime = DateTime.Now.ToLocalTime();
            var addOnline = db.Insertable(online).ExecuteCommand();
            if (addOnline < 1)
            {
                mes.suc = false;
                mes.mes = ConstHelper.INSERT_MODEL_ERROR;
            }
            else {
                mes.suc = true;
                mes.mes = ConstHelper.INSERT_MODEL_SUCCESS + addOnline + "条";
            }
            return mes;
        }
        /// <summary>
        /// 增加留言的点赞量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object UpdatOnlineLikes(int id)
        {
            TMessage<Online> mes = new TMessage<Online>();

            //留言赞数+1
            var updateLikes = db.Updateable<Online>()
                .UpdateColumns(it => new Online() { LikesCount = it.LikesCount + 1 })
                .Where(it => it.Id == id).ExecuteCommand();
            if (updateLikes<1) {
                mes.suc = false;
                mes.mes = ConstHelper.UPDATE_MODEL_ERROR;
            }else{
                mes.suc=true;
                mes.mes=ConstHelper.UPDATE_MODEL_SUCCESS;
            }
            return mes;
        }
        /// <summary>
        /// 通过留言表id删除留言表与回复表的相关内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object DelOnlineInfo(int id)
        {
            TMessage<Online> mes = new TMessage<Online>();

            var t5 = db.Deleteable<Online>().Where(it => it.Id == id).ExecuteCommand();//删除留言表的相关id
            if(t5<1){
                mes.suc = false;
                mes.mes = ConstHelper.DELETE_MODEL_ERROR;
            }else{
                db.Deleteable<Reply>().Where(it => it.OnlineId == id).ExecuteCommand();//删除与留言相关的回复
                mes.suc=true;
                mes.mes = ConstHelper.DELETE_MODEL_SUCCESS;
            }
            return mes;
        }
        /// <summary>
        /// 通过留言id查询回复列表
        /// </summary>
        /// <param name="OnlineId"></param>
        /// <returns></returns>
        public List<OutputReplyModel> ReplyList(int OnlineId)
        {
            var ReplyList = db.Queryable<Reply>()
                 .Select((re) => new OutputReplyModel
                 {
                     //查询回复所需字段
                     ReplyId = re.Id,
                     Recontent = re.Recontent,
                     Retime = re.Retime,
                     ReplyUserId = re.RUid,
                     ReplyUserName = SqlFunc.Subqueryable<UserInfo>().Where(ui => ui.Id == re.RUid).Select(ui => ui.UserName),
                 })
                 .Where((re => re.OnlineId == OnlineId)).ToList();
            return ReplyList;
        }
    }
}
