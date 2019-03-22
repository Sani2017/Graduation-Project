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
    public class AdminDAL
    {
        SqlSugarClient db;
                /// <summary>
        /// 与数据库帮助类关联
        /// </summary>
        public AdminDAL() 
        {
            db = SqlSugarClientHelper.SqlSugarDB();
        }
        /// <summary>
        /// 查询所有管理员列表
        /// </summary>
        /// <returns></returns>
        public TMessage<List<Admin>> GetAdminListInfo()//JsonResult
        {
            TMessage<List<Admin>> mes = new TMessage<List<Admin>>();
            var getAll = db.Queryable<Admin>().ToList();
            mes.extra = getAll;
            return mes;
        }
        /// <summary>
        /// 管理员验证（用户名与密码）
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public TMessage<List<Admin>> AdminLogin(string userName, string password)
        {
            TMessage<List<Admin>> mes = new TMessage<List<Admin>>();
            var adminLogin = db.Queryable<Admin>().Where(it => it.AdminName == userName && it.AdminPwd == password).ToList();
            if(adminLogin.Count==1){
                mes.suc = true;
                mes.mes = ConstHelper.USER_LOGIN_SUCCESS;
                mes.token = JwtToken.CreateToken(userName, password).ToString();//用户名与密码生成token
                mes.extra = adminLogin;
            }
            else {
                mes.suc = false;
                mes.mes = ConstHelper.USER_OR_PASSWORD_ERROR;
            }
            return mes;
        }
        /// <summary>
        /// 管理员注冊
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public object AddAdmin(AdminModel model)
        {
            TMessage<Admin> mes = new TMessage<Admin>();
            if (!SeleUserName(model.AdminName))
            {
                Admin admin = new Admin();
                    admin.AdminName = model.AdminName;
                    admin.AdminPwd = model.AdminPwd;
                    admin.AdminRight = 2;
                    var addOnline = db.Insertable(admin).ExecuteCommand();
                    mes.suc = true;
                    mes.mes = ConstHelper.REGISTER_SUCCESS;
            }
            else
            {
                mes.suc = false;
                mes.mes = ConstHelper.USER_NAME_IS_TREGISTER;
            }
            return mes;
        }
        /// <summary>
        /// 判断用戶名是否重复
        /// </summary>
        /// <param name="userName">查询用的类型名称</param>
        /// <returns></returns>
        public bool SeleUserName(string userName)
        {
            TMessage<Admin> mes = new TMessage<Admin>();

            var isAny = db.Queryable<Admin>().Where(it => it.AdminName == userName).ToList();//.Any();
            if (isAny.Count >= 1)
            {
                return true;
            }
            return false;

            //return isAny;
        }
        /// <summary>
        /// 删除管理员信息
        /// </summary>
        /// <returns></returns>
        public object delAdminById(int id) {
            TMessage<Admin> mes = new TMessage<Admin>();
            var delAdminById = db.Deleteable<Admin>().Where(it => it.Id == id).ExecuteCommand();//删除等于1的
            if (delAdminById >= 1)
            {
                mes.suc = true;
                mes.mes = ConstHelper.DELETE_MODEL_SUCCESS + delAdminById + "条";
            }
            else
            {
                mes.suc = false;
                mes.mes = ConstHelper.GET_NOTHING + "," + ConstHelper.DELETE_MODEL_ERROR;
            }
            return mes;
        }
        //SqlFunc.Subqueryable<Works>().Where(wk => wk.AuthorId == ui.Id && wk.AllowShow == (int)EnumType.StateResolution.OneType).Count()，
        /// <summary>
        /// 获取管理页首页数据top
        /// </summary>
        /// <returns></returns>
        public object getAdminIndexInfo(){
            var getInfo = db.Queryable<UserInfo>()
            .Select((ui) => new AdminIndexInfoModel
            {
                UserCount = SqlFunc.Subqueryable<UserInfo>()
                .Where((it => it.UserState == (int)EnumType.StateResolution.OneType)).Count(),
                WorkCount = SqlFunc.Subqueryable<Works>().Count(),
                UserForWorkCount = SqlFunc.Subqueryable<Works>()
                .Where(wk => wk.AllowShow == (int)EnumType.StateResolution.OneType
                 && wk.IsDeleted == (int)Common.EnumType.StateResolution.ZeroType)
                 .Count(),//0未删除)
                //MaxWorkCountUserInfo=
            }).Take(1).OrderBy("UserForWorkCount Desc").ToList();
            return getInfo;
        }
        /// <summary>
        /// 作品类别下作品总数
        /// </summary>
        /// <returns></returns>
        public object getSortNameAndCount() {
            var getInfo = db.Queryable<Sort>()
            .Select((st) => new SortModel
            {   Id=st.Id,
                SortName = st.SortName,
                SortWorkCount = SqlFunc.Subqueryable<Works>()
                .Where(wk => wk.Sort == st.Id &&
                    wk.AllowShow == (int)EnumType.StateResolution.OneType
                 && wk.IsDeleted == (int)Common.EnumType.StateResolution.ZeroType
                    )
                 .Count(),//0未删除)
                //MaxWorkCountUserInfo=
            }).ToList();
            return getInfo;
        }
        /// <summary>
        /// 用于“admin用户是否启用列表页
        /// </summary>
        /// <param name="searchValue">搜索值</param>
        /// <param name="page">页码</param>
        /// <param name="userState">用户状态1.启用，0.不启用)默认1</param>
        /// <returns></returns>
        public object selectUserAndWorkSum(string searchValue, int page,int userState)
        {
            var defaultPage = 1;//当前页
            var totalCount = 0;//总数
            var pageSize = 4;//分页显示条数;
            string orderByState = "UserWorksum Desc";
            if (page.ToString() != "")
            {
                defaultPage = page;
            }
            var AllList = db.Queryable<UserInfo>()
            .WhereIF(!string.IsNullOrEmpty(searchValue), ui => ui.UserName.Contains(searchValue.Trim()))
            .WhereIF(userState == (int)EnumType.StateResolution.OneType, ui => ui.UserState == (int)EnumType.StateResolution.OneType)
            .WhereIF(userState == (int)EnumType.StateResolution.ZeroType, ui => ui.UserState == (int)EnumType.StateResolution.ZeroType)
            .Select((ui) => new UserInfoAndWorkSum
            {
                UserId = ui.Id,
                UserImg = ui.UserImg,
                UserName = ui.UserName,
                Userlabel = ui.Userlabel,
                UserState = ui.UserState,
                UserWorksum = SqlFunc.Subqueryable<Works>().Where(wk => wk.AuthorId == ui.Id && wk.AllowShow == (int)EnumType.StateResolution.OneType).Count(),
                //UserWorkImg = SqlFunc.Subqueryable<Works>().Where(wk => wk.AuthorId == ui.Id).Select(it=>it.WorkImg).ToList().ToString(),//试试能不能输出作品封面
            })//不是主键就用另外一种查询方式，好好看文档，这个时候空指针异常还要问
            .OrderBy("" + orderByState + "")
            .ToPageList(defaultPage, pageSize, ref totalCount);//.ToSql();//.ToList();//

            int newPage = (totalCount - 1) / pageSize + 1;
            return new { Data = AllList, Total = newPage };//返回多個值。
        }
        /// <summary>
        /// 用于admin作品列表，用于审核是否批准显示
        /// </summary>
        /// <param name="page"></param>
        /// <param name="userId"></param>
        /// <param name="sortId"></param>
        /// <param name="if_show"></param>
        /// <param name="if_Deleted"></param>
        /// <returns></returns>
        public object getAllowShowWorkAndUserInfo(int page,string selectVal) {

            var defaultPage = 1;//当前页
            var totalCount = 0;//总数
            var pageSize = 5;//分頁顯示條數;
            string orderByState = "CreatedAt Asc";//用户创建时间
            if (page.ToString() != "")
            {
                defaultPage = page;
            }
            var AllIfShowWorkList=db.Queryable<Works>()
                .WhereIF(selectVal!=null, wk => wk.Title.Contains(selectVal.Trim()))

                //.WhereIF(!string.IsNullOrWhiteSpace(selectVal),selectVal.Contains(it.Id)))
                .Where(wk=>wk.AllowShow==(int)EnumType.StateResolution.ZeroType)//找出为0 不允许展示的
                .Where(wk=>wk.IsDeleted==(int)EnumType.StateResolution.ZeroType)//为0，未软删除的、
                .Select(wk=>new IndexWorksListModel{
                    WorkImg = wk.WorkImg,//作品封面
                    WorksId = wk.Id,//作品id
                    Content=wk.Content,//作品内容
                    WorksTitle = wk.Title,//标题
                    LikesCount = wk.LikesCount,//点赞量
                    Hits = wk.Hits,//浏览量
                    CreatedAt = wk.CreatedAt,//创建时间
                    UserId =SqlFunc.Subqueryable<UserInfo>().Where(ui =>ui.Id==wk.AuthorId).Select(ui=>ui.Id),///ui.Id,//用户id
                    UserName = SqlFunc.Subqueryable<UserInfo>().Where(ui => ui.Id == wk.AuthorId).Select(ui => ui.UserName),//用户名
                    UserImg = SqlFunc.Subqueryable<UserInfo>().Where(ui => ui.Id == wk.AuthorId).Select(ui => ui.UserImg),//用户头像
                    WorksSort = SqlFunc.Subqueryable<Sort>().Where(st => st.Id == wk.Sort).Select(st => st.SortName).ToString(),
                    //st.SortName,//作品分类
                }).OrderBy("" + orderByState + "")//创建时间是倒叙
            .ToPageList(defaultPage, pageSize, ref totalCount);//.ToSql();//.ToList();//
            //第几页，显示几条，总行数
            //.ToList();
            //if (totalCount > 4) { 
            int newPage = (totalCount - 1) / pageSize + 1;
            return new { Data = AllIfShowWorkList, Total = newPage };//返回多個值。
        }






        /// <summary>
        /// 获取所有活动信息
        /// </summary>
        /// <param name="lookState">查看状态</param>
        /// <param name="page">分页页数</param>
        /// <param name="searchId">查询活动id</param>
        /// <param name="searchName">查询活动名</param>
        /// <returns></returns>
        public object GetActivityInfo(int lookState, int page, int searchId, string selectVal)
        {
            //用于活动页面的分页输出,未测试
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
                .WhereIF(searchId != 0, (ac) => ac.Id == searchId)
                .WhereIF(!string.IsNullOrEmpty(selectVal), (ac) => ac.ActivityName.Contains(selectVal.Trim()))
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
                    ActivityState = ac.ActivityState,
                    NumberOfWorks = SqlFunc.Subqueryable<Works>().Where(wk => wk.ActivityId == ac.Id).Count(),//子查询找出相关活动报名类型
                }).Mapper((it, cache) =>
                {
                    if (it.ActivityDate != null)
                    {//求发布到当前时间的时间差
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
                        TimeSpan ts1 = dt1 - dtNew;
                        if (ts1.Days > 0)
                        {
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
                        }
                        else
                        {
                            it.ActivityTimeDifference = "活动已结束";
                            db.Updateable<Activity>(new { ActivityState = (int)EnumType.StateResolution.ZeroType, id = it.ActivityId }).ExecuteCommand();
                            //UPDATE [STudent]  SET [Name]='a' WHERE [Id]=1
                        }
                    }
                    else
                    {
                        it.ActivityTimeDifference = "...";

                    }
                })
                .ToPageList(defaultPage, pageSize, ref totalCount);//.ToSql();//.ToList();//
                //第几页，显示几条，总行数
                int newPage = (totalCount - 1) / pageSize + 1;
                return new { Data = activityList, Total = newPage };//返回多個值。
        }

    }
}
