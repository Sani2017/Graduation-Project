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
    public class ReplyDAL
    {
        SqlSugarClient db;
        /// <summary>
        /// 与数据库帮助类关联
        /// </summary>
        public ReplyDAL() 
        {
            db = SqlSugarClientHelper.SqlSugarDB();
        }

        /// <summary>
        /// 添加回復信息
        /// </summary>
        /// <returns></returns>
        public object AddReplyInfo(ReplyModel model)
        {
            TMessage<Reply> mes = new TMessage<Reply>();
            Reply reply = new Reply();
            reply.OnlineId = model.OnlineId;
            reply.RUid = model.RUid;
            reply.Recontent = model.Recontent;
            reply.Retime = DateTime.Now.ToLocalTime();
            var addOnline = db.Insertable(reply).ExecuteCommand();
            if (addOnline < 1)
            {
                mes.suc = false;
                mes.mes = ConstHelper.INSERT_MODEL_ERROR;
            }
            else
            {
                mes.suc = true;
                mes.mes = ConstHelper.INSERT_MODEL_SUCCESS + addOnline + "条";
            }
            return mes;
        }
        /// <summary>
        /// 滚！！！
        /// 废弃，查询出的留言重复，用GetOnlineInfoByPlaceId
        /// 根据留言地点的id查询留言、回复信息与条数，分页显示（上/下一页 132...121314）
        /// </summary>
        /// <param name="onlineId"></param>
        /// <returns></returns>
        //关于留言回复的整体输出功能有大问题，从长计议
        public object GetReplyInfoByOnlineId(int placeId)
        {
            var totalCount = 0;//总数
            TMessage<Reply> mes=new TMessage<Reply>();
            //表的排序影响到下面表的关联
            var getAll = db.Queryable<Online, Reply, UserInfo, UserInfo>
            ((ol, re, ui, ui2) => new object[]{
                JoinType.Left,re.OnlineId==ol.Id,
                JoinType.Left,ui.Id==re.RUid,
                JoinType.Left,ui2.Id==ol.Uid
            })
            .OrderBy((ol, re, ui, ui2) => re.Retime, OrderByType.Desc)//活动发布时间是顺序
            .Where((ol, re, ui, ui2) => ol.PlaceId == placeId)
            .Select((ol, re, ui, ui2) => new
            {//ol是留言，re是回复
                OnlineId = ol.Id,
                OnlineUserImg = ui2.UserImg,
                OnlineUserName = ui2.UserName,
                ol.OnlineContent,
                ol.Creatime,
                ol.LikesCount,
                //以上要输出留言表内容
                ReplyId = re.Id,
                ReplyUserImg = ui.UserImg,
                ReplyName = ui.UserName,
                re.Recontent,
                re.Retime
                //以上是輸出回復表的内容
            }).ToPageList(1, 10, ref totalCount);//.ToSql();//.ToList();//
                      //第几页，显示几条，总行数
            int totalPages = (totalCount - 1) / 2 + 1;//总页数
            if(getAll.Count<1){
                //mes.extra = new { Data = getAll, Total = page };
                mes.suc = false;
                mes.mes = ConstHelper.NO_EXIST_DATA;
                return mes;
            }
            return new { Data = getAll, Total = totalPages };//返回多個值。

            //我還沒有確定要留言回復輸出的形式所以暫時不進行分類去重
            //总页数 = (总行数 - 1) / 每页显示行数 + 1
            //比如  101行   每页10行   那就是  (101 - 1) / 10 + 1 = 11

        }
        /// <summary>
        /// 通过id删除单条回复表内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object DelReplyInfo(int id)
        {
            TMessage<Online> mes = new TMessage<Online>();

            var t5 = db.Deleteable<Reply>().Where(it => it.OnlineId == id).ExecuteCommand();//删除单条回复
            if (t5 < 1)
            {
                mes.suc = false;
                mes.mes = ConstHelper.DELETE_MODEL_ERROR;
            }
            else
            {
                mes.suc = true;
                mes.mes = ConstHelper.DELETE_MODEL_SUCCESS;
            }
            return mes;
        }
    }
}
