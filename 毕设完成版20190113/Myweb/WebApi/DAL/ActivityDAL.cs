using Common;
using Models;
using Models.ModelTemplate;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAl.Models;

namespace DAL
{
    public class ActivityDAL
    {
        /// <summary>
        /// 关于活动表的sql操作
        /// </summary>
        SqlSugarClient db;
        public ActivityDAL(){
            db=SqlSugarClientHelper.SqlSugarDB();
        }
        /// <summary>
        /// 增加活动信息sql
        /// </summary>
        /// <param name="model">活动表传参的model类</param>
        /// <returns></returns>
        public TMessage<Activity> AddActivity(ActivityModel model)
        {
            TMessage<Activity> mes = new TMessage<Activity>();
            if (SeleactivityName(model.ActivityName))
            {
                mes.suc = false;
                mes.mes = ConstHelper.NEWSTITLE_UNIQUER;
            }
            else { 
                //也可以批量插入
                //model.ActivityContent
                // model.ActivityName
                Activity activity = new Activity();
                activity.ActivityContent = model.ActivityContent;
                activity.ActivityName = model.ActivityName;
                activity.ActivityDate = DateTime.Now.ToLocalTime();
                activity.ActivityImg = model.ActivityImg;
                 activity.EndTime = model.EndTime;
                 activity.ActivityState = (int)Common.EnumType.StateResolution.OneType;
                var t4 = db.Insertable(activity)
                    .Where(true/* Is no insert null */, true/*off identity*/)
                    //.InsertColumns(it => new { it.ActivityName, it.ActivityContent })
                    .ExecuteCommand(); //.ToSql();//
                if (t4 >= 1)
                {
                    mes.suc = true;
                    mes.mes = ConstHelper.INSERT_MODEL_SUCCESS + t4 + "条";
                }
                else {
                    mes.suc = false;
                    mes.mes = ConstHelper.INSERT_MODEL_ERROR;
                }
            }
            return mes;//result4;.ExecuteReturnIdentity()

        }
       /// <summary>
       /// 根据id删除活动
       /// </summary>
       /// <param name="Id">活动Id</param>
       /// <returns></returns>
        public TMessage<Activity> DeleActivity(int Id){
            TMessage<Activity> mes = new TMessage<Activity>();

            bool SeleTF = SeleActivityById(Id);
            if (SeleTF)
            {
                var t0 = //db.Deleteable<Activity>().Where(new Activity() { Id = Id }).ExecuteCommand();
                    db.Deleteable<Activity>(Id).ExecuteCommand();//.ToSql();//
                mes.suc = true;
                mes.mes = ConstHelper.DELETE_MODEL_SUCCESS + t0 + "条";
            }
            else {
                mes.suc = false;
                mes.mes =ConstHelper.GET_NOTHING+ "," + ConstHelper.DELETE_MODEL_ERROR;
            }
            return mes;
        }
        /// <summary>
        /// 批量删除活动
        /// </summary>
        /// <returns></returns>
        public object DeleBatchActivityInfo(int[] deleBatchById)
        {
            TMessage<Activity> mes = new TMessage<Activity>();
            var t4 = db.Deleteable<Activity>().Where(it => deleBatchById.Contains(it.Id)).ExecuteCommand();//.ToSql();//.In(new int[] { 1,2,3}).ToSql();//.ExecuteCommand();
            if (t4 >= 1)
            {
                mes.suc = true;
                mes.mes = ConstHelper.DELETE_MODEL_SUCCESS + t4 + "条";
            }
            else
            {
                mes.suc = false;
                mes.mes = ConstHelper.GET_NOTHING + "," + ConstHelper.DELETE_MODEL_ERROR;
            }
            return mes;
        }

        /// <summary>
        /// 根据id修改活动
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public TMessage<Activity> UpdateActivity(ActivityModel model) {
            TMessage<Activity> mes = new TMessage<Activity>();

            bool SeleTF = SeleActivityById(model.Id);
            if (SeleTF)//判断是否存在
            {
                //4.指定对象更新，需要先获取在修改
                Activity activity = db.Queryable<Activity>()
                .Where(q => q.Id == model.Id)
                .First();
                if (activity.ActivityContent.Equals(model.ActivityContent)//判断前端传的值是否有修改，前端也会有相关拦截，这里做个保险
                    && activity.ActivityName.Equals(model.ActivityName)
                    && activity.ActivityState.Equals(model.ActivityState))
                {
                    mes.suc = false;
                    mes.mes = ConstHelper.NOT_MODEL_INFO;
                    return mes;
                }
                else {//赋值修改内容（应该是逐条修改，现在是全部修改，可改进！！）
                    //string content = activity.ActivityContent != model.ActivityContent ? (activity.ActivityContent = model.ActivityContent) : (model.ActivityContent = null);//return //model.ActivityContent;
                    //string name = activity.ActivityName != model.ActivityName ? (activity.ActivityName = model.ActivityName) : (model.ActivityName = null);//return //model.ActivityContent;
                    //int state = activity.ActivityState != model.ActivityState ? (activity.ActivityState = model.ActivityState) : (model.ActivityState = model.ActivityState);//return //model.ActivityContent;
                    //string text = "ActivityState" + state + ",";
                    //if (content != null) { text += "ActivityContent=" + content + ","; }
                    //if (name != null) { text += "ActivityName=" + name; }


                    //if (content == model.ActivityContent) { activity.ActivityContent = content; };
                    //if (name == model.ActivityName) { activity.ActivityName = name; };
                    //if (state == model.ActivityState) { activity.ActivityState = state; };
                    ////activity.ActivityName = model.ActivityName;
                    ////activity.ActivityState = model.ActivityState;
                    //var result4 = db.Updateable<Activity>()
                    //                .UpdateColumns(it => new Activity() { text })
                    //                .Where(it => it.Id == 11).ExecuteCommand();

                    activity.ActivityContent = model.ActivityContent;
                    activity.ActivityName = model.ActivityName;
                    activity.ActivityState = model.ActivityState;
                    activity.EndTime = model.EndTime;
                    activity.ActivityImg = model.ActivityImg;
                    //activity.ActivityDate = DateTime.Now.ToLocalTime();
                    var result4 = db.Updateable(activity).ExecuteCommand();//.ToSql();//db.Updateable(work);//.ExecuteCommand(); 
                    mes.suc = true;
                    mes.mes = ConstHelper.MODIFY_SUCCESS;
                }
            }
            else {
                mes.suc = false;
                mes.mes = ConstHelper.GET_NOTHING + "," + ConstHelper.UPDATE_MODEL_ERROR;
            }
            return mes;
        }
        /// <summary>
        /// 根据id获取活动信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object GetActivityInfoById( int id) {
            var activityList = db.Queryable<Activity>()
                .Where(ac => ac.Id == id)
                .Select(ac => new OutActivityListMode
                {
                    ActivityId = ac.Id,
                    ActivityImg = ac.ActivityImg,
                    ActivityTitle = ac.ActivityName,
                    ActivityDate = ac.ActivityDate,
                    EndTime = ac.EndTime,
                    //ActivityTimeDifference
                    LikesCount = ac.LikesCount,
                    //NewTimeDifference
                    Hits = ac.Hits,
                    ActivityContent=ac.ActivityContent,
                    ActivityState = ac.ActivityState,
                    NumberOfWorks = SqlFunc.Subqueryable<Works>().Where(wk => wk.ActivityId == ac.Id 
                        && wk.AllowShow == (int)Common.EnumType.StateResolution.OneType
                        && wk.IsDeleted == (int)Common.EnumType.StateResolution.ZeroType).Count(),//子查询找出相关活动报名类型
                }).ToList();
            return activityList;
        }
        /// <summary>
        /// 获取所有活动信息
        /// </summary>
        /// <param name="outState">输出状态</param>
        /// <param name="lookState">查看状态</param>
        /// <param name="page">分页页数</param>
        /// <param name="searchId">查询活动id</param>
        /// <returns></returns>
        public object GetActivityInfo(int outState,int lookState,int page,int searchId)
        {
            if (outState == 0)//輸出全部
            {
                var activityList = db.Queryable<Activity>()
                    .Where(it => it.ActivityState == (int)EnumType.StateResolution.OneType).ToList();
                return activityList;
            }
            else if (outState == 1)//輸出前五條
            {
                var activityList = db.Queryable<Activity>().Take(5)
                    .Where(it => it.ActivityState == (int)EnumType.StateResolution.OneType).ToList();
                return activityList;
            }
            else if (outState == 2)//分頁輸出
            {//用于活动页面的分页输出,未测试
                var defaultPage = 1;//当前页
                var totalCount = 0;//总数
                var pageSize = 2;//分頁顯示條數;
                string orderByState = "";
                if (page.ToString() != "")
                {
                    defaultPage = page;
                }
                switch (lookState)
                {
                    case 0:
                        orderByState = "ActivityDate Desc";//时间倒叙
                        break;
                    case 1:
                        orderByState = "LikesCount Desc";//赞数倒叙
                        break;
                    case 2:
                        orderByState = "Hits Desc";//浏览量倒叙
                        break;
                    default:
                        orderByState = "ActivityDate Desc";//时间倒叙
                        break;
                }
                var activityList = db.Queryable<Activity>()
                //.Where(ac => ac.ActivityState == (int)EnumType.StateResolution.OneType)
                .WhereIF(searchId!=0,(ac)=>ac.Id==searchId)
                .OrderBy("" + orderByState + "")//发布时间是倒叙
                .Select(ac => new OutActivityListMode
                {
                    ActivityId = ac.Id,
                    ActivityImg = ac.ActivityImg,
                    ActivityTitle = ac.ActivityName,
                    ActivityDate = ac.ActivityDate,
                    EndTime = ac.EndTime,
                    //ActivityTimeDifference
                    LikesCount = ac.LikesCount,
                    //NewTimeDifference
                    Hits = ac.Hits,
                    ActivityState=ac.ActivityState,
                    ActivityContent=ac.ActivityContent,
                    NumberOfWorks = SqlFunc.Subqueryable<Works>().Where(wk => wk.ActivityId == ac.Id).Count(),//子查询找出相关活动报名类型
                }).Mapper((it,cache) => {
                    if (it.ActivityDate != null) {//求发布到当前时间的时间差
                        DateTime dt1 = DateTime.Now;//当前时间
                        DateTime dt2 = DateTime.Parse(it.ActivityDate.ToString());//数据库存入时间
                        TimeSpan ts = dt1 - dt2;
                        if (ts.Days != 0)
                        {
                            if (ts.Days >= 365)
                            {
                                it.NewTimeDifference = "一年前";
                            }
                            else
                            {
                                it.NewTimeDifference = ts.Days.ToString() + "天";//超过365 显示一年前
                            }
                        }
                        else if (ts.Hours != 0)
                        {
                            it.NewTimeDifference = ts.Hours.ToString() + "小时";
                        }
                        else if (ts.Minutes != 0)
                        {
                            it.NewTimeDifference = ts.Minutes.ToString() + "分钟";
                        }
                        else if (ts.Seconds != 0)
                        {
                            it.NewTimeDifference = ts.Seconds.ToString() + "秒";
                        }
                    }
                    else
                    {
                        it.NewTimeDifference = "...";
                    }
                    if (it.EndTime != null || it.ActivityDate != null)
                    {//求发布时间到结束时间的时间差
                        DateTime dt1 = DateTime.Parse(it.EndTime.ToString());//结束时间
                        DateTime dt2 = DateTime.Parse(it.ActivityDate.ToString());//发布时间
                        DateTime dtNew = DateTime.Now;//当前时间
                        TimeSpan ts = dt1 - dt2;
                        TimeSpan ts1 =  dt1-dtNew;
                        if (ts1.Days > 0) { 
                            if (ts.Days > 0)
                            {
                                if (ts.Days > 365)
                                {
                                    it.NewTimeDifference = "结束还有一年多";
                                }
                                else
                                {
                                    it.ActivityTimeDifference = "结束还有" + ts.Days.ToString() + "天";
                                }
                            }
                            else if (ts.Hours > 0)
                            {
                                it.ActivityTimeDifference = "结束还有" + ts.Hours.ToString() + "小时";
                            }
                            else if (ts.Minutes > 0)
                            {
                                it.ActivityTimeDifference = "结束还有" + ts.Minutes.ToString() + "分钟";
                            }
                            else if (ts.Seconds > 0)
                            {
                                it.ActivityTimeDifference = "结束还有" + ts.Seconds.ToString() + "秒";
                            }
                        } else {
                            it.ActivityTimeDifference = "活动已结束";
                            db.Updateable<Activity>(new { ActivityState = (int)EnumType.StateResolution.ZeroType, id = it.ActivityId }).ExecuteCommand();
                            //UPDATE [STudent]  SET [Name]='a' WHERE [Id]=1
                        }
                    }
                    else {
                        it.ActivityTimeDifference = "...";

                    }
                })
                .ToPageList(defaultPage, pageSize, ref totalCount);//.ToSql();//.ToList();//
                    //第几页，显示几条，总行数
                    int newPage = (totalCount - 1) / pageSize + 1;
                    return new { Data = activityList, Total = newPage };//返回多個值。

            }
            //var activityListAll = db.Queryable<Activity>().Where(it => it.ActivityState == (int)EnumType.StateResolution.OneType).ToList();

            return false;
        }
        /// <summary>
        /// 查询活动表相关记录
        /// </summary>
        /// <param name="SelectName">查询的名称</param>
        /// <returns></returns>
        public object  GetSelectActivity(string SelectName, int SelectState)
        {
            TMessage<Activity> mes = new TMessage<Activity>();
            var selectContent = db.Queryable<Activity>().ToList();//GetActivityInfo();//先判活动表内是否有内容，我真牛逼想到这个办法，这样只要return一次就好
            if (selectContent.Count < 1) { mes.suc = false; mes.mes = "没有活动内容"; return mes; }

            if(!string.IsNullOrWhiteSpace(SelectName))//都不为空，两个条件都查&&!string.IsNullOrEmpty(SelectState.ToString())
            {
                selectContent = db.Queryable<Activity>().Where(c => c.ActivityName.Contains(SelectName.Trim()) && c.ActivityState.ToString().Contains(SelectState.ToString().Trim())).ToList();//.ToSql();//
                if (selectContent.Count < 1) { mes.suc = false; mes.mes = "没有查询到相关内容"; return mes; }
            }
            else//(string.IsNullOrEmpty(SelectName))//没有名字查状态
            {
                  selectContent = db.Queryable<Activity>().Where(c => c.ActivityState.ToString().Contains(SelectState.ToString())).ToList();//.ToSql();//
                if (selectContent.Count < 1) { mes.suc = false; mes.mes = "没有查询到相关内容"; return mes; }
            }
            //因为状态的int类型有默认值不存在空和null，所以关于状态的判断没有必要！
            //else if (string.IsNullOrEmpty(SelectState.ToString()))//没有状态查名字
            //{
            //      selectContent = db.Queryable<Activity>().Where(c => c.ActivityName.Contains(SelectName)).ToList();//.ToSql();//
            //    if (selectContent.Count < 1) { mes.suc = false; mes.mes = "没有查询到相关内容"; return mes; }
            //}
            return selectContent;
        }
        /// <summary>做到这里了！！！
        /// 根据活动信息划分作品sql(作品作品，活动表，用户表,类别表的查询，涉及到多表与分页)
        /// </summary>
        /// <returns></returns>
        public object GetAllActivitybyWorksInfo() {
            var AllList = db.Queryable<Activity, Works, UserInfo, Sort>
                ((ac, wk, ui, st) => new object[] { 
                JoinType.Left, ac.Id == wk.ActivityId,
                JoinType.Left,wk.AuthorId==ui.Id,
                JoinType.Left,st.Id==wk.Sort 
            })
                // .GroupBy(ui => new { ui.Id, ui.UserName })
                .OrderBy((ac, wk, ui, st) => ac.ActivityDate ,OrderByType.Desc)//活动发布时间是顺序
                .OrderBy((ac, wk, ui, st) => wk.LikesCount, OrderByType.Desc)//点赞量是倒叙

                .Where((ac, wk, ui, st) => ac.ActivityState != (int)Common.EnumType.StateResolution.ZeroType)
                .Select((ac, wk, ui, st) => new
                {
                    ActivityId = ac.Id,//活动id
                    ActivityName = ac.ActivityName,//活动名称
                    ActivityContent=ac.ActivityContent,//活动简介
                    ActivityDate=ac.ActivityDate,
                    WorksId = wk.Id,//作品id
                    Title = wk.Title,//标题
                    Content = wk.Content,//内容
                    Sort = st.SortName,//作品分类名
                    // AllowShow = wk.AllowShow,
                    LikesCount = wk.LikesCount,//点赞量
                    //CreatedAt = wk.CreatedAt,
                    UserName=ui.UserName,
                    UserImg=ui.UserImg,//用户头像
                    PublishedAt = wk.PublishedAt//发布时间

                })//.ToSql();
                   .ToList();

            return AllList.GroupBy(ac => ac.ActivityName);//分组，大集合嵌套小集合


        }

        /// <summary>
        /// 根据ID查询活动是否存在
        /// </summary>
        /// <param name="Id"></param>
        public bool SeleActivityById(int Id) {
            var getByWhere = db.Queryable<Activity>().Where(it => it.Id == Id).Any();//.ToList();
            return getByWhere;
            
        }
        /// <summary>
        /// 通过id增加活动浏览量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object UpdatActivityHits(int id)
        {
            TMessage<Activity> mes = new TMessage<Activity>();

            var updateLikes = db.Updateable<Activity>()
                .UpdateColumns(it => new Activity() { Hits = it.Hits + 1 })
                .Where(it => it.Id == id).ExecuteCommand();
            if (updateLikes < 1)
            {
                mes.suc = false;
                mes.mes = ConstHelper.UPDATE_MODEL_ERROR;
            }
            else
            {
                mes.suc = true;
                mes.mes = ConstHelper.UPDATE_MODEL_SUCCESS;
            }
            return mes;
        }

        /// <summary>
        /// 通过id增加活动赞数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object UpdatActivityLikesCount(int id)
        {
            TMessage<Activity> mes = new TMessage<Activity>();

            var updateLikes = db.Updateable<Activity>()
                .UpdateColumns(it => new Activity() { LikesCount = it.LikesCount + 1 })
                .Where(it => it.Id == id).ExecuteCommand();
            if (updateLikes < 1)
            {
                mes.suc = false;
                mes.mes = ConstHelper.UPDATE_MODEL_ERROR;
            }
            else
            {
                mes.suc = true;
                mes.mes = ConstHelper.UPDATE_MODEL_SUCCESS;
            }
            return mes;
        }
        /// <summary>
        /// 判断活动名称是否重复
        /// </summary>
        /// <param name="activityTitle">查询用的类型名称</param>
        /// <returns></returns>
        public bool SeleactivityName(string activityTitle)
        {
            TMessage<Activity> mes = new TMessage<Activity>();

            var isAny = db.Queryable<Activity>().Where(it => it.ActivityName == activityTitle).Any();

            return isAny;
        }
    }
}
