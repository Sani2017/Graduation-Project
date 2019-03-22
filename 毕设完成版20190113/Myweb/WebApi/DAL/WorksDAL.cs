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

    public class WorksDAL
    {
        /// <summary>
        /// 关于作品表的sql操作
        /// </summary>
        SqlSugarClient db;
        public WorksDAL()
        {
            db = SqlSugarClientHelper.SqlSugarDB();
        }
        /// <summary>
        /// 查询所有作品表内容sql
        /// </summary>
        /// <returns></returns>
        public TMessage<List<Works>> GetWorksInfo()
        {
            TMessage<List<Works>> mes = new TMessage<List<Works>>();
            var getAll = db.Queryable<Works>()
                //.OrderBy(it => it.CreatedAt,OrderByType.Desc)到这里了，关于用户个人的作品输出
                //有类似但不知道在哪里
                .Where(it => it.IsDeleted !=(int)Common.EnumType.StateResolution.OneType)//.ToSql();
                .ToList();
            if (getAll.Count < 1)
            {
                mes.suc = false;
                mes.mes = ConstHelper.GET_LIST_ERROR +","+ getAll.Count + "条";
            }
            else {
                mes.suc = true;
                mes.mes=ConstHelper.GET_LIST_SUCCESS;
                mes.extra = getAll;
            }
            Console.WriteLine(getAll);
            return mes;
        }
        //where（it=>it.aa="某作者"）.count()
        /// <summary>
        /// 修改作品表内容sql
        /// </summary>
        /// <returns>object</returns>
        public TMessage<Works> UpdateWorksInfo(WorkModel model)
        {
            TMessage<Works> mes = new TMessage<Works>();

            //4.指定对象更新，需要先获取在修改
            Works work = db.Queryable<Works>()
                .Where(q => q.Id == model.Id)
                .First();
            Works NewWorks = new Works();
            //实体类与公共类比较
            if (!work.AllowShow.Equals(model.AllowShow)) { work.AllowShow = model.AllowShow; }//是否允许展示
            if (!work.IsDeleted.Equals(model.IsDeleted)) { work.IsDeleted = model.IsDeleted; }//是否标识已删除
            if (!work.Sort.Equals(model.Sort)) { work.Sort = model.Sort; }//作品分类
            if (!work.ActivityId.Equals(model.ActivityId)) { work.ActivityId = model.ActivityId; }//活动ID
            if (!work.FileAddress.Equals(model.FileAddress)) { work.FileAddress = model.FileAddress; }//文件地址
            if (!work.Content.Equals(model.Content)) { work.Content = model.Content; }//内容简介
            if (!work.Title.Equals(model.Title)) { work.Title = model.Title; }//标题
            work.PublishedAt = DateTime.Now.ToLocalTime();//发布时间（管理员发布）
            work.CreatedAt = work.CreatedAt;//创建时间（用户创建）
            var result4 = db.Updateable(work).ExecuteCommand();//db.Updateable(work);//.ExecuteCommand(); 
            //Console.WriteLine(result4);
            if (result4 == 1)
            {
                mes.suc = true;
                mes.mes = ConstHelper.UPDATE_MODEL_SUCCESS;
            }
            else {
                mes.suc = false;
                mes.mes = ConstHelper.UPDATE_MODEL_ERROR;
            }
            return mes;//result4;
        }

         ///<summary>
         ///根据作者Id去获取相应的作品数量sql
         ///</summary>
         ///<returns></returns>
        public object GetWorksInfoByAuthorId(int AuthorId)
        {
            TMessage<Works> mes = new TMessage<Works>();
            int getAll = db.Queryable<Works>()
                .Where(wk => wk.AuthorId == AuthorId)
                .Count();
            mes.suc = true;
            mes.mes = getAll.ToString();
            return mes;
        }
        /// <summary>
        /// 获取点赞榜排名与用户表关联
        /// </summary>
        /// <returns>
        /// 有个想法，将栏目更改成排行榜，
        /// 有个下拉列表类表选中什么就以什么作为条件输出排行榜
        /// </returns>
        public object GetAllWorksLikesCountInfo() {
            var AllList = db.Queryable<Works, UserInfo,Sort>((wk, ui,st) => new object[] {
                JoinType.Right, ui.Id == wk.AuthorId,
                JoinType.Left,st.Id==wk.Sort 

            })
                // .GroupBy(ui => new { ui.Id, ui.UserName })
               // .OrderBy((wk, ui) => ui.Id, OrderByType.Asc)//id是顺序
                .OrderBy((wk) => wk.LikesCount, OrderByType.Desc)//点赞是倒叙

                .Select((wk, ui,st) => new
                {
                    UserId = ui.Id,//用户id
                    UserName = ui.UserName,//用户名
                    UserImg = ui.UserImg,//用户头像
                    WorksId = wk.Id,//作品id
                    Title = wk.Title,//标题
                    Content = wk.Content,//内容
                    Sort = st.SortName,//作品分类
                   // AllowShow = wk.AllowShow,
                    LikesCount = wk.LikesCount,//点赞量
                    //CreatedAt = wk.CreatedAt,
                    PublishedAt = wk.PublishedAt//发布时间

                })//.ToSql();
                   .ToList();

            return AllList;//.GroupBy(ui => ui.UserName);//分组，大集合嵌套小集合

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
        /// <param name="lookState">查看状态</param>
        /// <param name="searchValue">搜索内容</param>
        /// <param name="page">当前页数</param>
        /// <param name="pageName">页面名称</param>
        /// <param name="sortId">类型Id</param>
        /// <returns></returns>
        public object GetWorkAndUserInfoList(int lookState, string searchValue, int page,string pageName,int sortId)
        {  
            var defaultPage = 1;//当前页
            var totalCount = 0;//总数
            var pageSize = 10;//分頁顯示條數;
            string orderByState ="";
            switch(lookState){
                case 0:
                    orderByState = "PublishedAt Desc";//时间倒叙
                    break;
                case 1:
                    orderByState = "LikesCount Desc";//赞数倒叙
                    break;
                case 2:
                    orderByState = "Hits Desc";//浏览量倒叙
                    break;
                default:
                    orderByState = "PublishedAt Desc";//时间倒叙
                    break;
            }
            //TMessage<List<IndexWorksListModel>> mes = new TMessage<List<IndexWorksListModel>>();
            //发现页/搜索页的操作
            if (pageName == "Find")
            {
                defaultPage = page;
            }
            var AllList = db.Queryable<Works, UserInfo, Sort>((wk, ui, st) => new object[] {
                JoinType.Left, ui.Id == wk.AuthorId,
                JoinType.Left,st.Id==wk.Sort 

            })
                //根据条件判段是否执行过滤，我们可以用WhereIf来实现，true执行过滤，false则不执行
            .WhereIF(!string.IsNullOrEmpty(searchValue), wk => wk.Title.Contains(searchValue.Trim()))
           // .Where(ui => ui.UserName.Contains(selectName.Trim()) && ui.UserState == (int)EnumType.StateResolution.OneType)

            .WhereIF(sortId != 0, (wk, ui, st) => st.Id == sortId)
                // .GroupBy(ui => new { ui.Id, ui.UserName })
                // .OrderBy((wk, ui) => ui.Id, OrderByType.Asc)//id是顺序
                // .OrderBy((wk) => wk.LikesCount, OrderByType.Desc)//点赞是倒叙
            .Where((wk) => wk.AllowShow == (int)Common.EnumType.StateResolution.OneType)//1允许展示
            .Where((wk) => wk.IsDeleted == (int)Common.EnumType.StateResolution.ZeroType)//0未删除
            .Select((wk, ui, st) => new IndexWorksListModel//这个很重要
            {
                WorkImg = wk.WorkImg,//作品封面
                WorksId = wk.Id,//作品id
                WorksTitle = wk.Title,//标题
                LikesCount = wk.LikesCount,//点赞量
                Hits = wk.Hits,//浏览量
                PublishedAt = wk.PublishedAt,//发布时间
                UserId = ui.Id,//用户id
                UserName = ui.UserName,//用户名
                UserImg = ui.UserImg,//用户头像
                WorksSort = st.SortName,//作品分类
            })//.ToSql();
            .Mapper((it, cache) =>
            {
                if (it.PublishedAt != null)
                {
                    DateTime dt1 = DateTime.Now;//当前时间
                    DateTime dt2 = DateTime.Parse(it.PublishedAt.ToString());//数据库存入时间
                    TimeSpan ts = dt1 - dt2;
                    if (ts.Days != 0)
                    {
                        if (ts.Days >= 365)
                        {
                            it.TimeDifference = "一年前";
                        }
                        else
                        {
                            it.TimeDifference = ts.Days.ToString() + "天";//超过365 显示一年前
                        }
                    }
                    else if (ts.Hours != 0)
                    {
                        it.TimeDifference = ts.Hours.ToString() + "小时";
                    }
                    else if (ts.Minutes != 0)
                    {
                        it.TimeDifference = ts.Minutes.ToString() + "分钟";
                    }
                    else if (ts.Seconds != 0)
                    {
                        it.TimeDifference = ts.Seconds.ToString() + "秒";
                    }
                }
                else
                {
                    it.TimeDifference = "...";
                }
            })
            //.Take(50)
            .OrderBy("" + orderByState + "")//发布时间是倒叙
                //.ToSql();
            .ToPageList(defaultPage, pageSize, ref totalCount);//.ToSql();//.ToList();//
                    //第几页，显示几条，总行数
            //.ToList();
            //if (totalCount > 4) { 
            int newPage = (totalCount - 1) / pageSize + 1;
            return new { Data = AllList, Total = newPage };//返回多個值。
            //}
            //如果页数小于1则只输出内容不输出页码
            
            //return AllList;
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
        public object GetWorkInfoByUserId(int lookState, int page,int userId)
        {
            var defaultPage = page;//当前页
            var totalCount = 0;//总数
            var pageSize = 1;//分頁顯示條數;
            string orderByState = "";
            switch (lookState)
            {
                case 0:
                    orderByState = "PublishedAt Desc";//时间倒叙
                    break;
                case 1:
                    orderByState = "LikesCount Desc";//赞数倒叙
                    break;
                case 2:
                    orderByState = "Hits Desc";//浏览量倒叙
                    break;
                default:
                    orderByState = "PublishedAt Desc";//时间倒叙
                    break;
            }

            var AllList = db.Queryable<Works, UserInfo, Sort>((wk, ui, st) => new object[] {
                JoinType.Left, ui.Id == wk.AuthorId,
                JoinType.Left,st.Id==wk.Sort 

            })
            .Where((wk, ui, st) => wk.AuthorId == userId)
            .Where((wk) => wk.AllowShow == (int)Common.EnumType.StateResolution.OneType)//1允许展示
            .Where((wk) => wk.IsDeleted == (int)Common.EnumType.StateResolution.ZeroType)//0未删除
                // .GroupBy(ui => new { ui.Id, ui.UserName })
                // .OrderBy((wk, ui) => ui.Id, OrderByType.Asc)//id是顺序
                // .OrderBy((wk) => wk.LikesCount, OrderByType.Desc)//点赞是倒叙
            .Select((wk, ui, st) => new IndexWorksListModel//这个很重要
            {
                WorkImg = wk.WorkImg,//作品封面
                WorksId = wk.Id,//作品id
                WorksTitle = wk.Title,//标题
                LikesCount = wk.LikesCount,//点赞量
                Hits = wk.Hits,//浏览量
                PublishedAt = wk.PublishedAt,//发布时间
                UserId = ui.Id,//用户id
                UserName = ui.UserName,//用户名
                UserImg = ui.UserImg,//用户头像
                WorksSort = st.SortName,//作品分类
            })//.ToSql();
            .Mapper((it, cache) =>
            {
                if (it.PublishedAt != null)
                {
                    DateTime dt1 = DateTime.Now;//当前时间
                    DateTime dt2 = DateTime.Parse(it.PublishedAt.ToString());//数据库存入时间
                    TimeSpan ts = dt1 - dt2;
                    if (ts.Days != 0)
                    {
                        if (ts.Days >= 365)
                        {
                            it.TimeDifference = "一年前";
                        }
                        else
                        {
                            it.TimeDifference = ts.Days.ToString() + "天";//超过365 显示一年前
                        }
                    }
                    else if (ts.Hours != 0)
                    {
                        it.TimeDifference = ts.Hours.ToString() + "小时";
                    }
                    else if (ts.Minutes != 0)
                    {
                        it.TimeDifference = ts.Minutes.ToString() + "分钟";
                    }
                    else if (ts.Seconds != 0)
                    {
                        it.TimeDifference = ts.Seconds.ToString() + "秒";
                    }
                }
                else
                {
                    it.TimeDifference = "...";
                }
            })
            .OrderBy("" + orderByState + "")//发布时间是倒叙
            .ToPageList(defaultPage, pageSize, ref totalCount);//.ToSql();//.ToList();//
                            //第几页，显示几条，总行数
            //.ToList();
            //if (totalCount > 4) { 
            int newPage = (totalCount - 1) / pageSize + 1;
            return new { Data = AllList, Total = newPage };//返回多個值。
        }

        /// <summary>
        /// 根据用户Id输出作品信息,用于用户私人页的作品输出
        /// </summary>
        /// <param name="page">当前页数</param>
        /// <param name="userId">用户Id</param>
        /// <param name="sortId">类型Id</param>
        /// <param name="if_show">是否显示[0:否,1:是],默认值:0</param>
        /// <param name="if_Deleted">是否软删除[1:是，0:否],默认值:0</param>
        /// 全部 3:0
        /// 是否显示 0:0
        /// 已通过 1:0 
        /// 已删除 3:1
        /// <returns></returns>
        public object GetWorkInfoByUserIdForUserPersonal(int page, int userId, int sortId, int if_show, int if_Deleted)
        {
            var defaultPage = 1;//当前页
            var totalCount = 0;//总数
            var pageSize = 2;//分頁顯示條數;
            string orderByState = "PublishedAt Desc";
            if (page.ToString() != "")
            {
                defaultPage = page;
            }
            var AllList = db.Queryable<Works, UserInfo, Sort>((wk, ui, st) => new object[] {
                JoinType.Left, ui.Id == wk.AuthorId,
                JoinType.Left,st.Id==wk.Sort 

            })
            .Where((wk, ui, st) => wk.AuthorId == userId)//作品与用户关联

            .WhereIF(sortId != 0, (wk, ui, st) => st.Id == sortId)

            .WhereIF(if_show != 3, (wk, ui, st) => wk.AllowShow == if_show)
            .WhereIF(if_Deleted != 3, (wk, ui, st) => wk.IsDeleted == if_Deleted)

            .Select((wk, ui, st) => new IndexWorksListModel//这个很重要
            {
                WorkImg = wk.WorkImg,//作品封面
                WorksId = wk.Id,//作品id
                WorksTitle = wk.Title,//标题
                LikesCount = wk.LikesCount,//点赞量
                Hits = wk.Hits,//浏览量
                PublishedAt = wk.PublishedAt,//发布时间
                UserId = ui.Id,//用户id
                UserName = ui.UserName,//用户名
                UserImg = ui.UserImg,//用户头像
                WorksSort = st.SortName,//作品分类
            })//.ToSql();
            .Mapper((it, cache) =>
            {
                if (it.PublishedAt != null)
                {
                    DateTime dt1 = DateTime.Now;//当前时间
                    DateTime dt2 = DateTime.Parse(it.PublishedAt.ToString());//数据库存入时间
                    TimeSpan ts = dt1 - dt2;
                    if (ts.Days != 0)
                    {
                        if (ts.Days >= 365)
                        {
                            it.TimeDifference = "一年前";
                        }
                        else
                        {
                            it.TimeDifference = ts.Days.ToString() + "天";//超过365 显示一年前
                        }
                    }
                    else if (ts.Hours != 0)
                    {
                        it.TimeDifference = ts.Hours.ToString() + "小时";
                    }
                    else if (ts.Minutes != 0)
                    {
                        it.TimeDifference = ts.Minutes.ToString() + "分钟";
                    }
                    else if (ts.Seconds != 0)
                    {
                        it.TimeDifference = ts.Seconds.ToString() + "秒";
                    }
                }
                else
                {
                    it.TimeDifference = "...";
                }
            })
            .OrderBy("" + orderByState + "")//发布时间是倒叙
            .ToPageList(defaultPage, pageSize, ref totalCount);//.ToSql();//.ToList();//
            //第几页，显示几条，总行数
            //.ToList();
            //if (totalCount > 4) { 
            int newPage = (totalCount - 1) / pageSize + 1;
            return new { Data = AllList, Total = newPage };//返回多個值。
        }




        /// <summary>
        /// 批量删除作品信息
        /// </summary>
        /// <param name="deleBatchById">删除的作品id</param>
        /// <returns></returns>
        public object DeleBatchWorksInfo(int[] deleBatchById)
        {
            
            TMessage<Works> mes = new TMessage<Works>();
            var t4 = db.Deleteable<Works>().Where(it => deleBatchById.Contains(it.Id)).ExecuteCommand();//.ToSql();//.In(new int[] { 1,2,3}).ToSql();//.ExecuteCommand();
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
        /// 删除本地文件
        /// </summary>
        /// <param name="fileUrl">文件路径</param>
        /// <returns></returns>
        //[AcceptVerbs("post", "Options")]
        //[HttpPost]
        //[FunctionView("删除本地文件", OperationType.DELETE)]

        //public object DeleFiles(string fileUrl)
        //{
        //    try
        //    {
        //        string realpath = System.Web.HttpContext.Current.Server.MapPath(fileUrl); ;
        //        bool bl = System.IO.File.Exists(realpath);
        //        if (bl)
        //        {
        //            System.IO.File.Delete(realpath);
        //            //Result.State = DeleteState.Success;
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return e.Message;
        //    }
        //}
        /// <summary>
        /// 批量软删除作品信息update
        /// </summary>
        /// <param name="Id">软删除作品id</param>
        /// <returns></returns>
        public object UpdateWorkIsDeleted(int[] Id ) {
            TMessage<Works> mes = new TMessage<Works>();

            var updatIsDeleted = //db.Updateable<Works>().Where(it => Id.Contains(it.Id)).ExecuteCommand();//.ToSql();//.In(new int[] { 1,2,3}).ToSql();//.ExecuteCommand();
                db.Updateable<Works>().
                UpdateColumns(it => new Works() { IsDeleted = (int)Common.EnumType.StateResolution.OneType }).
                Where(it => Id.Contains(it.Id)).ExecuteCommand();//.ToSql();//
            //db.Updateable<Student>().UpdateColumns(it => new Student()
            //{ Name = it.Name+1}).Where(it => it.Id == 11).ExecuteCommand();
            if (updatIsDeleted >= 1)
            {
                mes.suc = true;
                mes.mes = ConstHelper.DELETE_MODEL_SUCCESS + updatIsDeleted + "条";
            }
            else
            {
                mes.suc = false;
                mes.mes = ConstHelper.GET_NOTHING + "," + ConstHelper.DELETE_MODEL_ERROR;
            }
            return mes;
        
        }

        /// <summary>
        /// 批量复原作品信息update
        /// </summary>
        /// <param name="Id">软删除作品id</param>
        /// <returns></returns>
        public object UpdateWorkrevert(int[] Id)
        {
            TMessage<Works> mes = new TMessage<Works>();

            var updatIsDeleted = //db.Updateable<Works>().Where(it => Id.Contains(it.Id)).ExecuteCommand();//.ToSql();//.In(new int[] { 1,2,3}).ToSql();//.ExecuteCommand();
                db.Updateable<Works>().
                UpdateColumns(it => new Works() { IsDeleted = (int)Common.EnumType.StateResolution.ZeroType }).
                Where(it => Id.Contains(it.Id)).ExecuteCommand();//.ToSql();//
            //db.Updateable<Student>().UpdateColumns(it => new Student()
            //{ Name = it.Name+1}).Where(it => it.Id == 11).ExecuteCommand();
            if (updatIsDeleted >= 1)
            {
                mes.suc = true;
                mes.mes = ConstHelper.MODIFY_SUCCESS + updatIsDeleted + "条";
            }
            else
            {
                mes.suc = false;
                mes.mes = ConstHelper.GET_NOTHING + "," ;
            }
            return mes;

        }
        /// <summary>
        /// 复原作品可查询show update
        /// </summary>
        /// <param name="Id">软删除作品id</param>
        /// <returns></returns>
        public object UpdateWorkShow(int Id)
        {
            TMessage<Works> mes = new TMessage<Works>();

            var updatIsDeleted = //db.Updateable<Works>().Where(it => Id.Contains(it.Id)).ExecuteCommand();//.ToSql();//.In(new int[] { 1,2,3}).ToSql();//.ExecuteCommand();
                db.Updateable<Works>().
                UpdateColumns(it => new Works() { AllowShow = (int)Common.EnumType.StateResolution.OneType }).
                Where(it => Id==it.Id).ExecuteCommand();//.ToSql();//
            //db.Updateable<Student>().UpdateColumns(it => new Student()
            //{ Name = it.Name+1}).Where(it => it.Id == 11).ExecuteCommand();
            if (updatIsDeleted >= 1)
            {
                mes.suc = true;
                mes.mes = ConstHelper.MODIFY_SUCCESS + updatIsDeleted + "条";
            }
            else
            {
                mes.suc = false;
                mes.mes = ConstHelper.GET_NOTHING + ",";
            }
            return mes;

        }
        /// <summary>
        /// 根据是否删除的状态去删除作品信息（定时）
        /// </summary>
        /// <param name="isDeleted">是否标识已删除[1:是，0:否],默认值:0</param>
        /// <returns></returns> async Task<int>
        public object DelWorkInfoByIsDeleted()
        {
            //删除所有isDeleted不等于0的信息
            //List<int> list = new List<int>() { 1, 3 };!list.Contains(!= 0
            var t5 = db.Deleteable<Works>().Where(it => it.IsDeleted == (int)Common.EnumType.StateResolution.OneType).ExecuteCommand();//.ToSql();//
            return t5;
        }
        /// <summary>
        /// 增加留言的点赞量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object UpdatWorksLikes(int id)
        {
            TMessage<Works> mes = new TMessage<Works>();

            var updateLikes = db.Updateable<Works>()
                .UpdateColumns(it => new Works() { LikesCount = it.LikesCount + 1 })
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
        /// 增加作品信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public object AddWorksInfo(WorkModel model) {
            TMessage<Works> mes = new TMessage<Works>();
            //if (!SeleUserName(model.Title)) {
                Works works = new Works();
                works.CreatedAt = DateTime.Now.ToLocalTime();//model.CreatedAt;创建时间（用户创建）
                works.WorkImg = model.WorkImg;//封面
                works.Title = model.Title;//标题
                works.Content = model.Content;//内容
                works.FileAddress = model.FileAddress;//附件地址
                works.AuthorId = model.AuthorId;//作者id
                works.AuthorName = model.AuthorName;//作者姓名
                works.ActivityId = model.ActivityId;//活动ID
                works.Sort = model.Sort;//作品类型
                works.PublishedAt = DateTime.Now.ToLocalTime();

                var t4 = db.Insertable(works)
                    .Where(true, true)///* true*/, true/*off identity*/Is no insert null
                    .ExecuteCommand(); //.ToSql();
                if (t4 >= 1)
                {
                    mes.suc = true;
                    mes.mes = ConstHelper.INSERT_MODEL_SUCCESS + t4 + "条";
                }
                else
                {
                    mes.suc = false;
                    mes.mes = ConstHelper.INSERT_MODEL_ERROR;
                }
            //}
            //else
            //{
            //    mes.suc = false;
            //    mes.mes = ConstHelper.NEWSTITLE_UNIQUER;
            //}
            return mes;//result4;.ExecuteReturnIdentity()

        }

        /// <summary>
        /// 判断作品名称是否重复(作品名应该是可以重复的，主要作者名称不重复就可以了)
        /// </summary>
        /// <param name="workTitle">查询用的类型名称</param>
        /// <returns></returns>
        public bool SeleUserName(string workTitle)
        {
            TMessage<Works> mes = new TMessage<Works>();

            var isAny = db.Queryable<Works>().Where(it => it.Title == workTitle).Any();

            return isAny;
        }
        /// <summary>
        /// 调用两张表（用户，作品）模糊查询名称
        /// </summary>
        /// <param name="seleName"></param>
        /// <returns></returns>
        public object SelectInput(string selectName)
        {
            var selectUser = db.Queryable<UserInfo>()
                .Where(ui => ui.UserName.Contains(selectName.Trim()) && ui.UserState == (int)EnumType.StateResolution.OneType)
                .Select(ui => new { 
                    ui.Id,
                    ui.UserName
                }).ToList();//.ToSql();//
            var selectWork = db.Queryable<Works>()
                .Where(wk => wk.Title.Contains(selectName.Trim()) && wk.AllowShow == (int)EnumType.StateResolution.OneType)
                .Select(wk => new { 
                    wk.Id,
                    wk.Title
                })
                .ToList();//.ToSql();//
            return new { UserName = selectUser, WorkName = selectWork };//返回多個值。
        }
        /// <summary>
        /// 用于榜单的输出。成功了输出总分并降序
        /// </summary>
        ///  <param name="SortId">类型id，默认为0，不筛选全表无条件输出</param>
        /// <returns></returns>
        public object PopularList(int SortId) {
            var orderByState = "TotalScore Desc";
            var PopularList = db.Queryable<Works, UserInfo>
            ((wk, ui) => new object[]{
                JoinType.Left,wk.AuthorId==ui.Id
            })
            .WhereIF(SortId != 0, (wk, ui) => wk.Sort == SortId)
            .Where((wk) => wk.AllowShow == (int)Common.EnumType.StateResolution.OneType)//1允许展示
            .Where((wk) => wk.IsDeleted == (int)Common.EnumType.StateResolution.ZeroType)//0未删除
            .Select((wk, ui) => new OutPopularListMode
            {
                UserId = ui.Id,
                UserName = ui.UserName,
                UserImg = ui.UserImg,
                WorkId = wk.Id,
                WorkImg = wk.WorkImg,
                WorkTitle = wk.Title,
                WorkSort = SqlFunc.Subqueryable<Sort>().Where(st => st.Id == wk.Sort).Select(st => st.SortName).ToString(),
                PublishedAt = wk.PublishedAt,
                //WorksCount = SqlFunc.Subqueryable<Works>()
                //.Where(wks => wks.AuthorId == ui.Id && wks.AllowShow == (int)EnumType.StateResolution.OneType).Count(),
                //作品总数
                WorksCount = SqlFunc.Subqueryable<Works>()
                .Where(it => it.IsDeleted == (int)Common.EnumType.StateResolution.ZeroType //未软删除
                    && it.AllowShow == (int)Common.EnumType.StateResolution.OneType)//允许展示
                    .Count(),
                //TotalScore =wk.LikesCount+wk.Hits,//TotalScore,// 
                //点赞量
                LikesCount=wk.LikesCount,
                //阅读量
                Hits=wk.Hits,


            }).Mapper((it, cache) =>
            {
                it.TotalScore = (float)Math.Round((float)it.LikesCount / it.WorksCount * ((float)70 / 100)
                    + (float)it.Hits / it.WorksCount * ((float)30 / 100), 2);
            })
            //.MergeTable()
          //""+orderByState+""it => new OutPopularListMode {
            .Take(5)
          .ToList()
            .OrderByDescending(x => x.TotalScore);

           // .OrderBy(x=>x.TotalScore);
            return PopularList;
        }
        /// <summary>
        /// 根据活动id查找活动
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public object GetWorkByActivityId(int activityId,int page,int lookState)
        { 
            var defaultPage = 1;//当前页
            var totalCount = 0;//总数
            var pageSize = 2;//分頁顯示條數;
            string orderByState ="";
            switch (lookState)
            {
                case 0:
                    orderByState = "PublishedAt Desc";//时间倒叙
                    break;
                case 1:
                    orderByState = "LikesCount Desc";//赞数倒叙
                    break;
                case 2:
                    orderByState = "Hits Desc";//浏览量倒叙
                    break;
                default:
                    orderByState = "PublishedAt Desc";//时间倒叙
                    break;
            }
            if (!string.IsNullOrWhiteSpace(page.ToString())) { 
                defaultPage = page;
            }
            var AllList = db.Queryable<Works>(
            //    (wk, ui, st) => new object[] {, UserInfo, Sort
            //    JoinType.Left, ui.Id == wk.AuthorId,
            //    JoinType.Left,st.Id==wk.Sort 
            //}, ui, st
            )
            .Where((wk) => wk.ActivityId == activityId)
            .Where((wk) => wk.AllowShow == (int)Common.EnumType.StateResolution.OneType)//1允许展示
            .Where((wk) => wk.IsDeleted == (int)Common.EnumType.StateResolution.ZeroType)//0未删除
            .Select((wk) => new IndexWorksListModel//这个很重要
            {
                WorkImg = wk.WorkImg,//作品封面
                WorksId = wk.Id,//作品id
                WorksTitle = wk.Title,//标题
                LikesCount = wk.LikesCount,//点赞量
                Hits = wk.Hits,//浏览量
                PublishedAt = wk.PublishedAt,//发布时间
                UserId = SqlFunc.Subqueryable<UserInfo>().Where(ui => ui.Id == wk.AuthorId).Select(ui => ui.Id), 
                //ui.Id,//用户id
                UserName = SqlFunc.Subqueryable<UserInfo>().Where(ui => ui.Id == wk.AuthorId).Select(ui => ui.UserName).ToString(),
                //ui.UserName,//用户名
                UserImg = SqlFunc.Subqueryable<UserInfo>().Where(ui => ui.Id == wk.AuthorId).Select(ui => ui.UserImg).ToString(),
                //ui.UserImg,//用户头像
                WorksSort = SqlFunc.Subqueryable<Sort>().Where(st => st.Id == wk.Sort).Select(st => st.SortName).ToString(),
                //st.SortName,//作品分类
            })//.ToSql();
            .Mapper((it, cache) =>
            {
                if (it.PublishedAt != null)
                {
                    DateTime dt1 = DateTime.Now;//当前时间
                    DateTime dt2 = DateTime.Parse(it.PublishedAt.ToString());//数据库存入时间
                    TimeSpan ts = dt1 - dt2;
                    if (ts.Days != 0)
                    {
                        if (ts.Days >= 365)
                        {
                            it.TimeDifference = "一年前";
                        }
                        else
                        {
                            it.TimeDifference = ts.Days.ToString() + "天";//超过365 显示一年前
                        }
                    }
                    else if (ts.Hours != 0)
                    {
                        it.TimeDifference = ts.Hours.ToString() + "小时";
                    }
                    else if (ts.Minutes != 0)
                    {
                        it.TimeDifference = ts.Minutes.ToString() + "分钟";
                    }
                    else if (ts.Seconds != 0)
                    {
                        it.TimeDifference = ts.Seconds.ToString() + "秒";
                    }
                }
                else
                {
                    it.TimeDifference = "...";
                }
            })
                            //.Take(50)
            .OrderBy("" + orderByState + "")//发布时间是倒叙
                            //.ToSql();
            .ToPageList(defaultPage, pageSize, ref totalCount);//.ToSql();//.ToList();//
                        //第几页，显示几条，总行数
                        //.ToList();
                        //if (totalCount > 4) { 
                        int newPage = (totalCount - 1) / pageSize + 1;
                        return new { Data = AllList, Total = newPage };//返回多個值。

        }



        /// <summary>
        /// 根据id查找作品信息
        /// 可用于个人页（公共，非公共）
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public object GetWorkInfoByWorkId(int workId)
        {

            var AllList = db.Queryable<Works>()
                .Where(wk=>wk.Id==workId)
            .Select((wk) => new IndexWorksListModel//这个很重要
            {
                WorkImg = wk.WorkImg,//作品封面
                WorksId = wk.Id,//作品id
                WorksTitle = wk.Title,//标题
                Content = wk.Content,//内容
                LikesCount = wk.LikesCount,//点赞量
                Hits = wk.Hits,//浏览量
                PublishedAt = wk.PublishedAt,//发布时间
                FileAddress=wk.FileAddress,//作品附件
                UserId = SqlFunc.Subqueryable<UserInfo>().Where(ui => ui.Id == wk.AuthorId).Select(ui => ui.Id),
                //ui.Id,//用户id
                UserName = SqlFunc.Subqueryable<UserInfo>().Where(ui => ui.Id == wk.AuthorId).Select(ui => ui.UserName).ToString(),
                //ui.UserName,//用户名
                UserImg = SqlFunc.Subqueryable<UserInfo>().Where(ui => ui.Id == wk.AuthorId).Select(ui => ui.UserImg).ToString(),
                //ui.UserImg,//用户头像
                WorksSort = SqlFunc.Subqueryable<Sort>().Where(st => st.Id == wk.Sort).Select(st => st.SortName).ToString(),
                Userlabel = SqlFunc.Subqueryable<UserInfo>().Where(ui => ui.Id == wk.AuthorId).Select(ui => ui.Userlabel),
                //st.SortName,//作品分类
            })//.ToSql();
            .Mapper((it, cache) =>
            {
                if (it.PublishedAt != null)
                {
                    DateTime dt1 = DateTime.Now;//当前时间
                    DateTime dt2 = DateTime.Parse(it.PublishedAt.ToString());//数据库存入时间
                    TimeSpan ts = dt1 - dt2;
                    if (ts.Days != 0)
                    {
                        if (ts.Days >= 365)
                        {
                            it.TimeDifference = "一年前";
                        }
                        else
                        {
                            it.TimeDifference = ts.Days.ToString() + "天";//超过365 显示一年前
                        }
                    }
                    else if (ts.Hours != 0)
                    {
                        it.TimeDifference = ts.Hours.ToString() + "小时";
                    }
                    else if (ts.Minutes != 0)
                    {
                        it.TimeDifference = ts.Minutes.ToString() + "分钟";
                    }
                    else if (ts.Seconds != 0)
                    {
                        it.TimeDifference = ts.Seconds.ToString() + "秒";
                    }
                }
                else
                {
                    it.TimeDifference = "...";
                }
            }).ToList();
            return AllList; 
                //new { Data = AllList, Total = newPage };//返回多個值。

        }

        /// <summary>
        /// 通过id增加作品浏览量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object UpdatWorksHits(int id)
        {
            TMessage<Works> mes = new TMessage<Works>();

            var updateLikes = db.Updateable<Works>()
                .UpdateColumns(it => new Works() { Hits = it.Hits + 1 })
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
        /// 通过id增加作品赞数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object UpdatWorksLikesCount(int id)
        {
            TMessage<Works> mes = new TMessage<Works>();

            var updateLikes = db.Updateable<Works>()
                .UpdateColumns(it => new Works() { LikesCount = it.LikesCount + 1 })
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
        /// 根据作品id查询该作品是否通过审核与是否删除
        /// </summary>
        /// <param name="workId"></param>
        /// <returns></returns>
        public object outputWorkStateById (int workId){
            TMessage<Works> mes = new TMessage<Works>();
            var isAny = db.Queryable<Works>().Where(it => it.Id == workId).Any();//判断是否存在这个作品
            if (isAny)
            {   //判断是否符合标准
                var isAnyState = db.Queryable<Works>().Where(it => it.Id == workId
                && it.AllowShow == (int)Common.EnumType.StateResolution.OneType//1允许展示
                && it.IsDeleted == (int)Common.EnumType.StateResolution.ZeroType//0未删除
                ).Any();
                if (isAnyState)
                {
                    mes.suc = true;
                }
                else {
                    mes.suc = false;
                    mes.mes = ConstHelper.WORK_CHECK_NOT;
                }
            }
            else {
                mes.suc = false;
                mes.mes = ConstHelper.NO_EXIST_DATA;
            }
            return mes;
        }

    }

}
     //           var allWorks = cache.GetListByPrimaryKeys<Works>(vmodel => vmodel.WorkId);
     //           //排名分数计算方法：阅读文章数/总文章数*30 + 点赞文章数/总文章数*70SqlFunc.Subqueryable
     //           it.WorksCount = allWorks.Where(wk => wk.IsDeleted == (int)Common.EnumType.StateResolution.ZeroType //未软删除
     //               && wk.AllowShow == (int)Common.EnumType.StateResolution.OneType)//允许展示
       //             .Count();
                
                
                //var WorkCount=it.WorksCount = db.Queryable<Works>()
                //    .Where(wk => wk.IsDeleted == (int)Common.EnumType.StateResolution.ZeroType //未软删除
                //        && wk.AllowShow == (int)Common.EnumType.StateResolution.OneType)//允许展示
                //        .Count();
                //it.WorksCount;//作品总数
       //         float Likes = it.LikesCount;//单个作品点赞量
       //         float Hits = it.Hits;//单个作品点击量Math.Round(Hits / WorkCount, 2)
       //         it.TotalScore = Likes + Hits;//(Hits / it.WorksCount * 30 / 100 + Likes / it.WorksCount * 70 / 100);// / WorkCount;/// WorkCount ;//((float)Hits / WorkCount) * 30 + ((float)Likes / WorkCount )* 70;
           
