using Common;
using Common.Logging;
using Models;
using Models.ModelTemplate;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
 
namespace DAL
{
        /// <summary>
        /// 关于用户信息表的sql操作
        /// </summary>
    public class UserInfoDAL
    {
        SqlSugarClient db;
        /// <summary>
        /// 与数据库帮助类关联
        /// </summary>
        public UserInfoDAL() 
        {
            db = SqlSugarClientHelper.SqlSugarDB();
        }
        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
        public TMessage<List<UserInfo>> GetUserInfo()//JsonResult
        {
            TMessage<List<UserInfo>> mes= new TMessage<List<UserInfo>>();
            var getAll = db.Queryable<UserInfo>().ToList();
            mes.extra=getAll;
            return mes;
        }

        /// <summary>
        /// 获取所有用户以及作品信息
        /// </summary>
        /// <returns>用户表/作品表查询</returns>
        public object GetWorksInfo() {
            TMessage < List<UserInfo>> mes = new TMessage<List<UserInfo>>();
            var AllList = db.Queryable<Works, UserInfo>((wk, ui) => new object[] { JoinType.Right, ui.Id == wk.AuthorId })
                // .GroupBy(ui => new { ui.Id, ui.UserName })
                //.OrderBy((wk, ui) => ui.Id, OrderByType.Asc)//id是顺序
               // .Where((wk) => wk.PublishedAt, OrderByType.Asc)
                .OrderBy((wk) => wk.PublishedAt, OrderByType.Asc)//发布时间是倒叙
                .Select(( wk,ui) => new
                {
                    UserId = ui.Id,
                    UserName = ui.UserName,
                    UserImg = ui.UserImg,//用户头像
                    WorksId = wk.Id,
                    Title = wk.Title,
                    Content = wk.Content,
                    Sort = wk.Sort,
                    AllowShow = wk.AllowShow,
                    LikesCount = wk.LikesCount,
                    CreatedAt=wk.CreatedAt,
                    PublishedAt = wk.PublishedAt

                })//.ToSql();
                   .ToList();
            //应该建立一个model用来传出
            return AllList;//.GroupBy(ui => ui.UserName);//分组，大集合嵌套小集合
        }
        /// <summary>
        /// 查询用户信息以及相关作品数量,
        /// 用于“用户搜索页”
        /// 子查询的使用，可以用于参加活动的总作品数等等
        /// 使用分离的方法解决了主键问题空指针异常。
        /// 用于用户排名页面——“大佬页”带分页功能用户用作品数量排序，用户下的作品用赞数排序
        /// </summary>
        /// <returns></returns>
        public object selectUserAndWorkSum(string searchValue, int page)
        {
            var defaultPage = 1;//当前页
            var totalCount = 0;//总数
            var pageSize = 4;//分页显示条数;
            string orderByState = "UserWorksum Desc";
            if (page.ToString() != "") {
                defaultPage = page;     
            }
            var AllList = db.Queryable<UserInfo>()
            .WhereIF(!string.IsNullOrEmpty(searchValue), ui => ui.UserName.Contains(searchValue.Trim()))
            .Select((ui) => new UserInfoAndWorkSum { 
                UserId=ui.Id,
                UserImg=ui.UserImg,
                UserName=ui.UserName,
                Userlabel=ui.Userlabel,
                UserState=ui.UserState,
                UserWorksum = SqlFunc.Subqueryable<Works>().Where(wk => wk.AuthorId == ui.Id && wk.AllowShow == (int)EnumType.StateResolution.OneType).Count(),
                //UserWorkImg = SqlFunc.Subqueryable<Works>().Where(wk => wk.AuthorId == ui.Id).Select(it=>it.WorkImg).ToList().ToString(),//试试能不能输出作品封面
            })//不是主键就用另外一种查询方式，好好看文档，这个时候空指针异常还要问
            .OrderBy("" + orderByState + "")
            .ToPageList(defaultPage, pageSize, ref totalCount);//.ToSql();//.ToList();//

            int newPage = (totalCount - 1) / pageSize + 1;
            return new { Data = AllList, Total = newPage };//返回多個值。
        }
        /// <summary>
        /// 用于大佬页——用户排名
        /// </summary>
        /// <param name="page">选择的页数</param>
        /// <returns></returns>
        public object PopularListUser(int page){
            var defaultPage = 1;//当前页
            var totalCount = 0;//总数
            var pageSize = 4;//分页显示条数;
            string orderByState = "UserWorksum Desc";
            if (page.ToString() != "")
            {
                defaultPage = page;
            }
            var AllList = db.Queryable<UserInfo>()
            .Select((ui) => new UserInfoAndWorkSum
            {
                UserId = ui.Id,
                UserImg = ui.UserImg,
                UserName = ui.UserName,
                Userlabel = ui.Userlabel,
                UserWorksum = SqlFunc.Subqueryable<Works>().Where(wk => wk.AuthorId == ui.Id
                    && wk.AllowShow == (int)EnumType.StateResolution.OneType//1允许展示
                    && wk.IsDeleted == (int)Common.EnumType.StateResolution.ZeroType//0未删除
                    ).Count(),
                //UserWorkImg = SqlFunc.Subqueryable<Works>().Where(wk => wk.AuthorId == ui.Id).Select(it=>it.WorkImg).ToList().ToString(),//试试能不能输出作品封面
            })//不是主键就用另外一种查询方式，好好看文档，这个时候空指针异常还要问

            .Mapper((it, cache) =>//备用方法删除这个mapper 重写一个接口先查用户再查用户下的作品，弄不出只能这样了用于大佬页面
            {   //一对多 查询改用户下所有图片封面，未测试,,数量输出有问题
                it.UserWorkImg = UserWorkList(it.UserId);
            })
            .OrderBy("" + orderByState + "")
            .ToPageList(defaultPage, pageSize, ref totalCount);//.ToSql();//.ToList();//

            int newPage = (totalCount - 1) / pageSize + 1;
            return new { Data = AllList, Total = newPage };//返回多個值。
        }
        /// <summary>
        /// 通过用户id去查询名下作品
        /// 按照作品点赞数降序，前3名
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<UserWorkImg> UserWorkList(int userId) {
            var userWorkList = db.Queryable<Works>()
                .Select((wk) => new UserWorkImg
                {
                    WorkId=wk.Id,
                    AuthorId = wk.AuthorId,
                    Title=wk.Title,
                    WorksImg=wk.WorkImg,
                    LikesCount=wk.LikesCount,
                    Hits=wk.Hits,
                })
                .Where(wk => wk.AuthorId == userId)
                .OrderBy("LikesCount desc")
                .Take(4)
                .ToList();
            return userWorkList;
        }

        /// <summary>
        /// 根据用户id获取用户所有信息
        /// </summary>
        /// <param name="Id">用户Id</param>
        /// <returns></returns>
        public TMessage<UserInfo> GetAllUserInfoById(int Id) {
            TMessage<UserInfo> mes = new TMessage<UserInfo>();

            var getByPrimaryKey = db.Queryable<UserInfo>().InSingle(Id);
            if (getByPrimaryKey != null)
            {
                mes.suc = true;
                mes.mes = ConstHelper.GET_MODEL_SUCCESS;
                mes.extra = getByPrimaryKey;
            }
            else {
                mes.suc = false;
                mes.mes = ConstHelper.GET_MODEL_ERROR;
            }
            
            return mes;

        }
        /// <summary>
        /// 根据用户id获取用户信息 用于用户个人页非公共
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public object GetUserInfoByID(int Id)
        {
            TMessage<UserInfo> mes = new TMessage<UserInfo>();
            var GetUserInfoById= db.Queryable<UserInfo>()
            .Where(ui => ui.Id == Id)
            .Select((ui) => new UserInfoAndWorkSum//这个很重要
            {
                UserId = ui.Id,
                UserImg = ui.UserImg,
                UserName = ui.UserName,
                Userlabel = ui.Userlabel,
                UserWorksum = SqlFunc.Subqueryable<Works>().Where(wk => wk.AuthorId == ui.Id && wk.AllowShow == (int)EnumType.StateResolution.OneType).Count(),
            }).ToList();
            if (GetUserInfoById != null)
            {
                return GetUserInfoById;
            }
            else
            {
                mes.suc = false;
                mes.mes = ConstHelper.GET_MODEL_ERROR;
            }

            return mes;
            //TMessage<UserInfo> mes = new TMessage<UserInfo>();
            //var getByPrimaryKey = db.Queryable<UserInfo>()
            //    .Where(it => it.Id == Id)
            //    //.Select((ui) => new UserInfoAndWorkSum
            //    //{
            //    //    UserId = ui.Id,
            //    //    UserImg = ui.UserImg,
            //    //    UserName = ui.UserName,
            //    //    Userlabel = ui.Userlabel,
            //    //    UserWorksum = SqlFunc.Subqueryable<Works>().Where(wk => wk.AuthorId == ui.Id && wk.AllowShow == (int)EnumType.StateResolution.OneType).Count(),
            //    //})
            //    .ToList;//
            ////if (getByPrimaryKey != null)
            ////{
            //    return getByPrimaryKey;
            ////}
            ////else
            ////{
            ////    mes.suc = false;
            ////    mes.mes = ConstHelper.GET_MODEL_ERROR;
            ////}

            ////return mes;

        }
        /// <summary>
        /// 用户登录验证（用户名与密码）
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public TMessage<List<UserInfo>> UserLogin(string userName, string password)
        {
            TMessage<List<UserInfo>> mes = new TMessage<List<UserInfo>>();
            var userLogin= db.Queryable<UserInfo>().ToList(); 
            //判断是用户名还是
            if (!NumberHelper.ValidPhoneNumber(userName))
            {//使用用户名与密码登录
               userLogin = db.Queryable<UserInfo>().Where(it => it.UserName == userName && it.PassWord == password).ToList();//.Any();//.ToSql();
            }
            else
            {
               if (password == "yzm")//使用手机号与验证码登录
               {
                   userLogin = db.Queryable<UserInfo>().Where(it => it.UserPhone == userName).ToList();//.Any();//.ToSql();//
                   if (userLogin.Count != 1)
                   {
                       mes.suc = false;
                       mes.mes = ConstHelper.PHONE_ISNOT_REGISTER;
                       return mes;
                   }

               }
               else {//使用手机号与密码登录
                   userLogin = db.Queryable<UserInfo>().Where(it => it.UserPhone == userName && it.PassWord == password).ToList();//.Any();//.ToSql();//
               }
                //string[] phoneNumbers = { userName };
                //SmsHelper sms = new SmsHelper();　　//短信发送帮助类
                //sms.PutSms(phoneNumbers);
            }

           // = db.Queryable<UserInfo>().Where("+loginName+").ToList();//.Any();//.ToSql();
            //string token="";
            if (userLogin.Count==1)
            {///做到这里了发送用户名密码，获取token
                //mes.extra=token;
                //var userInfo = db.Queryable<UserInfo>().Where(it => it.UserName == userName)
                //    .ToList();//Any();//.ToSql();.Select(it => new { it.UserName,it.UserImg,it.Id})
                mes.suc = true;
                mes.mes = ConstHelper.USER_LOGIN_SUCCESS;
                mes.token = JwtToken.CreateToken(userName, password).ToString();//用户名与密码生成token
                mes.extra = userLogin;
                return mes;
               // mes.ttime= DESCryption.Encode(DateTime.Now.ToString());//token生成加密时间
                //已经对token缓存进行了弹性时间控制，超出时间删除
            }
            else {
                mes.suc = false;
                mes.mes = ConstHelper.USER_OR_PASSWORD_ERROR;
            }
            return mes;
        }

        /// <summary>
        /// 用戶注冊
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public object AddUserInfo(UserInfoModel model) {
            TMessage<UserInfo> mes = new TMessage<UserInfo>();
            if (!SeleUserName(model.UserName)){
//                if (!SeleEmail(model.Email)) {
                    if (!SelePhoneNumber(model.UserPhone)) { 
                        UserInfo userInfo = new UserInfo();
                        userInfo.UserName = model.UserName;
                        userInfo.PassWord = model.PassWord;
                        userInfo.ActualName = model.ActualName;
                        userInfo.UserPhone = model.UserPhone;
                        userInfo.Email = model.Email;
                        userInfo.UserImg = model.UserImg;  
                        var addOnline = db.Insertable(userInfo).ExecuteCommand();
                        mes.suc = true;
                        mes.mes = ConstHelper.REGISTER_SUCCESS;
                        //注册成功就是登录状态。
                        //mes.token = JwtToken.CreateToken(model.UserName,model.PassWord).ToString();//用户名与密码生成token
                        //mes.ttime = DESCryption.Encode(DateTime.Now.ToString());//token生成加密时间

                    }else
                    {
                        mes.suc = false;
                        mes.mes = ConstHelper.USER_REGISTERED + "或" + ConstHelper.PARAMETER_ERROR;
                    }
                //}else
                //{
                //    mes.suc = false;
                //    mes.mes = ConstHelper.EMAIL_IS_TREGISTER; 
                //}
            }else {
                mes.suc = false;
                mes.mes=ConstHelper.USER_NAME_IS_TREGISTER;
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
            TMessage<UserInfo> mes = new TMessage<UserInfo>();

            var isAny = db.Queryable<UserInfo>().Where(it => it.UserName == userName).ToList();//.Any();
            if (isAny.Count >= 1)
            {
                return true;
            }
            return false;

            //return isAny;
        }
        /// <summary>
        /// 判断📫是否重复
        /// </summary>
        /// <param name="email">查询用的类型名称</param>
        /// <returns></returns>
        public bool SeleEmail(string email)
        {
            string strRegex = @"([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,5})+";
            Regex regex = new Regex(strRegex, RegexOptions.IgnoreCase);
            if (regex.Match(email).Success)
            {
                var isAny = db.Queryable<UserInfo>().Where(it => it.Email == email).ToList();//.Any();
                if(isAny.Count>=1){
                    //var userName = db.Queryable<UserInfo>().Where(it => it.Email == email).Select(it => new { it.UserName }).ToJson();//.ToList();
                    
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断手機號是否重复
        /// </summary>
        /// <param name="phoneNumber">查询用的类型名称</param>
        /// <returns></returns>
        public bool SelePhoneNumber(string phoneNumber)
        {
            if (NumberHelper.ValidPhoneNumber(phoneNumber.ToString()))
            {
                var isAny = db.Queryable<UserInfo>().Where(it => it.UserPhone == phoneNumber).ToList();//.Any();
                if (isAny.Count >= 1)//存在且只有一条
                {

                    return true;
                }
                return false;
            }
            else {
                return false;
            }
        }
        /// <summary>
        /// 根据用户名去判断用户状态是否激活
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public bool SeleUserState(string userName){
            bool state=db.Queryable<UserInfo>().Where(it => it.UserName == userName && it.UserState==1 ).Any();
            return state;
        }
        //追加郵箱,手機驗證，與密碼重置等功能誒喲！！
        //-----------------------------------------------------------------------------------------------------
        /// <summary>
        /// 邮箱内容以及配置的编辑
        /// </summary>
        /// <param name="userName">用户姓名</param>
        /// <param name="userEmali">用户邮箱</param>
        /// <param name="smtpName">用户邮箱后缀如：163.com，qq.com</param>
        public object EmailContent(string userName, string userEmali)//, string smtpName)
        {
            string userNameUrl = HttpUtility.UrlEncode(userName);//提前将用户名url加密验证时会自动解密
            //时间addData，随机验证码validataCode，用户状态UserState
            Random random = new Random();
            string checkCode = "";
            for (int i = 0; i <= 4; i++)
            {
                int num = random.Next(1, 10);
                checkCode += num.ToString();
            }
            string validataCode = MD5Helper.GetMd5(checkCode);
            //user.validateCode = validataCode;
            string strSmtpServer = "smtp.qq.com";//这是个常量是不能进行改变的，规定了发送的形式,与下面的发送端相关联。 //+ smtpName;
            string strFrom = "sani_x@qq.com";//"18368718477@163.com";//994579080//942596590，sani_x@qq.com
            string strFromPass = "pzgyglxceninbcia";//"sani316";//pzgyglxceninbcia//jlpemnlkzktdbdgb//ysfnpahtzljhbceg//要开启IMAP/SMTP服务，写入他所需的验证码
            string strto = userEmali;
            string strSubject = "";
            System.Text.StringBuilder strBody = new System.Text.StringBuilder();
            if(userName=="1"){
                //userName=="1"是用户用邮箱找回密码，无用户名
                //还调用这个接口就是重置密码
                userNameUrl = HttpUtility.UrlEncode(userEmali);//提前将邮箱url加密验证时会自动解密
            
                strSubject = "重置密码";//localhost:6992/api/UserInfo/EmailVerifyPassword
                strBody.Append("点击下面链接激活账号，1小时生效，否则重新申请修改，链接只能使用一次，请尽快激活！</br>");
                //strBody.Append("<a href='getPassword_email.html?userName=" + userNameUrl + "&validateCode=" + validataCode + "'>点击这里</a></br>");
                //strBody.Append("如未能激活请点击下面链接：<a href='getPassword_email.html?userName=" + userNameUrl + "&validataCode=" + validataCode + "'>" +
                //"getPassword_email.html?userName=" + userNameUrl + "&validateCode=" + validataCode + "</a></br>");
                strBody.Append("<a href='http://127.0.0.1:5500/getPassword_email.html?userName=" + userNameUrl + "&validateCode=" + validataCode + "'>点击这里</a></br>");
                strBody.Append("如未能激活请点击下面链接：<a href='http://127.0.0.1:5500/getPassword_email.html?userName=" + userNameUrl + "&validataCode=" + validataCode + "'>" +
                "http://127.0.0.1:5500/getPassword_email.html?userName=" + userNameUrl + "&validateCode=" + validataCode + "</a></br>");
            }
            else if (!SeleUserState(userName)) {
                //没有则是激活用户账号
                strSubject = "账号激活";
                strBody.Append("点击下面链接激活账号，1小时生效，否则重新注册账号，链接只能使用一次，请尽快激活！</br>");
                strBody.Append("<a href='http://localhost:6992/api/UserInfo/EmailVerify?userName=" + userNameUrl + "&validateCode=" + validataCode + "'>点击这里</a></br>");
                strBody.Append("如未能激活请点击下面链接：<a href='http://localhost:6992/api/UserInfo/EmailVerify?userName=" + userNameUrl + "&validataCode=" + validataCode + "'>" +
                    "http://localhost:6992/api/UserInfo/EmailVerify?userName=" +  userNameUrl + "&validateCode=" + validataCode + "</a></br>");

            }//有前端页面的时候记得要把链接内容改掉，现在是直接链接到接口
            // SendSMTPEMail(strSmtpServer, strFrom, strFromPass, strto, strSubject, strBody.ToString());

            //string smptport = "25"; // TODO: 初始化为适当的值smptport
            bool bl = sendTheMail(strSmtpServer,  strFrom, strFromPass, strto, strSubject, strBody.ToString());
            if (bl)
            {//将用户名和随机验证码插入缓存，缓存弹性过期时间一小时，既激活账号有效期。
                if (userName == "1")
                {
                    DataCache.SetCacheOneHours(userEmali, validataCode);
                }
                else { 
                    DataCache.SetCacheOneHours(userName, validataCode);//这样就不用再传时间的值去判断了，如果超过一个小时缓存就自动清除
                //GetHostAddress();
                }
            }
            return bl;
        }

        /// <summary>
        /// 发送邮件(有个问题不能给企业邮箱发邮件)
        /// </summary>
        /// <param name="strSmtpServer">邮件服务器</param>
        /// <param name="strFrom">发送端账号</param>
        /// <param name="strFromPass">发送端账号密码</param>
        /// <param name="strto">注册者的邮箱</param>
        /// <param name="strSubject">发送的邮件的主题</param>
        /// <param name="strBody">发送的邮件正文</param>, string smptport
        protected bool sendTheMail(string smtpserver, string strFrom, string strFromPass, string strto, string subj, string bodys)
        {
            SmtpClient _smtpClient = new SmtpClient();

            _smtpClient.EnableSsl = true;//是否启用SSL

            _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
            _smtpClient.UseDefaultCredentials = false;
            _smtpClient.Host = smtpserver;//指定SMTP服务器
            _smtpClient.Credentials = new System.Net.NetworkCredential(strFrom, strFromPass);//邮箱和授权码
            //_smtpClient.Credentials = new NetworkCredential(strto, strFromPass);
            #region 邮箱接口感觉没什么用
            //if (string.IsNullOrWhiteSpace(smptport))
            //{。
            //    int port = Convert.ToInt32(smptport);
            //    if (port > 0)
            //        _smtpClient.Port = port;
            //}
            #endregion

            MailMessage _mailMessage = new MailMessage(strFrom, strto);
            //_mailMessage.From = strFrom;//发件人
            _mailMessage.Subject = subj;//主题
            _mailMessage.Body = bodys;//内容
            _mailMessage.BodyEncoding = System.Text.Encoding.Default;//正文编码
            _mailMessage.IsBodyHtml = true;//设置为HTML格式
            _mailMessage.Priority = MailPriority.High;//优先级
 
            try
            {
                ServicePointManager.ServerCertificateValidationCallback =
delegate(Object obj, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) { return true; };
                //for (var i = 0; i <= 20; i++) {
                _smtpClient.Send(_mailMessage);
                //}
                return true;
            }
            catch //()//Exception e)
            {
               // throw e;
                //var loger = LogManager.GetLogger(typeof(strFrom));
                //loger.Info(string.Format("发送邮件异常,收信邮箱：{0}", this.To[0]), e);
                return false;
            }
        }
        //另一个邮件发送的代码，没有现在的好废弃
        //public void SendSMTPEMail(string strSmtpServer, string strFrom, string strFromPass, string strto, string strSubject, string strBody)
        //{
        //    System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(strSmtpServer);
        //    client.UseDefaultCredentials = false;
        //    client.Credentials =
        //    new System.Net.NetworkCredential(strFrom, strFromPass);
        //    client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

        //    System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(strFrom, strto, strSubject, strBody);
        //    message.BodyEncoding = System.Text.Encoding.UTF8;
        //    message.IsBodyHtml = true;
        //    client.Send(message);
        //}
        /// <summary>
        ///  邮箱注册，链接缓存验证 用户启用
        /// </summary>
        /// <param name="userName">用户名缓存中的名</param>
        /// <param name="validataCode">加密后的随机数</param>
        /// <returns></returns>
        public object EmailVerify(string userName, string validataCode)
        {
            //string userNameUrl = HttpUtility.UrlDecode(userName);
            try
            {
                if (DataCache.GetCache(userName).Equals(validataCode))
                {
                    var t10 = db.Updateable<UserInfo>()
                            .UpdateColumns(it => new UserInfo() { UserState = (int)EnumType.StateResolution.OneType})
                            .Where(it => it.UserName == userName).ExecuteCommand();
                    DataCache.RemoveCacheByCacheKey(userName);
                    return true;
                }
                else
                {
                    DataCache.RemoveCacheByCacheKey(userName);
                    return false;
                }
            }
            catch //()//Exception e)
            //1.可能就是缓存过期已经清除了
            //2.链接已使用过一次，缓存已经删除。可以做个404页面跳转什么的
            {
                return false;
            }

        }
        /// <summary>
        ///  邮箱密码重置链接缓存验证
        /// </summary>
        /// <param name="userValue">用户名缓存中的名</param>
        /// <param name="validataCode">加密后的随机数</param>
        /// <returns></returns>
        public object EmailVerifyPassword(string userValue, string validataCode)
        {
            //string userNameUrl = HttpUtility.UrlDecode(userName);
            try
            {
                if (DataCache.GetCache(userValue).Equals(validataCode))
                {
                    DataCache.RemoveCacheByCacheKey(userValue);
                    return true;
                }
                else
                {
                    DataCache.RemoveCacheByCacheKey(userValue);
                    return false;
                }
            }
            catch //()//Exception e)
            //1.可能就是缓存过期已经清除了
            //2.链接已使用过一次，缓存已经删除。可以做个404页面跳转什么的
            {
                return false;
            }
        }
        /// <summary>
        /// 用户密码重置
        /// </summary>
        /// <param name="userValue">用户名</param>
        /// <param name="newPwd">用户新密码</param>
        /// <returns></returns>
        public object ResetUserPassword(string userValue, string newPwd)
        {
           // GetHostAddress();
            if (SeleEmail(userValue))
            {
                var resetPasswordByEmail = db.Updateable<UserInfo>()
                    .UpdateColumns(it => new UserInfo() { PassWord = newPwd })
                    .Where(it => it.Email == userValue).ExecuteCommand();
                if (resetPasswordByEmail ==1)
                {
                    return true;
                }
                //selectWhere = "";
            }
            else if (SelePhoneNumber(userValue))
            {
                var resetPasswordByPhone = db.Updateable<UserInfo>()
                    .UpdateColumns(it => new UserInfo() { PassWord = newPwd })
                    .Where(it => it.UserPhone == userValue).ExecuteCommand();
                if (resetPasswordByPhone ==1)
                {
                    return true;
                }                //selectWhere = "it => it.UserPhone == userValue";
            }
            return false;
        }
        /// <summary>
        /// 根据用户id更新用户信息
        /// </summary>
        /// <returns></returns>
        public object UpdateUserInfoByUserId(UpdateUserInfoModel model) {
            TMessage<Works> mes = new TMessage<Works>();
            UserInfo userInfo = db.Queryable<UserInfo>()
                .Where(q => q.Id == model.UserId)
                .First();
            userInfo.UserName=model.UserName;
            userInfo.ActualName=model.ActualName;
            userInfo.Userlabel = model.Userlabel;
            userInfo.UserPhone = model.UserPhone;
            userInfo.Email = model.Email;
            userInfo.UserImg = model.UserImg;
            var updateUserInfo= db.Updateable(userInfo)
            .IgnoreColumns(ignoreAllNullColumns: true)//是NULL的列不更新
            .Where(ui => ui.Id == model.UserId)
            .ExecuteCommand();
            if (updateUserInfo == 1)
            {
                mes.suc = true;
                mes.mes = ConstHelper.UPDATE_MODEL_SUCCESS;
            }
            else
            {
                mes.suc = false;
                mes.mes = ConstHelper.UPDATE_MODEL_ERROR;
            }
            return mes;//result4;
        }
        #region 阅读量根据ip

        //取不到，浏览量功能再议！！::1 ==127.0.0.1 不能用本机测试
        /// <summary>
        /// 获取客户端IP地址（无视代理）
        /// </summary>
        /// <returns>若失败则返回回送地址</returns>
        public static string GetHostAddress()
        {
            string userHostAddress = HttpContext.Current.Request.UserHostAddress;
  
            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
 
            //最后判断获取是否成功，并检查IP地址的格式（检查其格式非常重要）
            if (!string.IsNullOrEmpty(userHostAddress) && IsIP(userHostAddress))
            {
            return userHostAddress;
            }
            return "127.0.0.1";
        }
 
        /// <summary>
        /// 检查IP地址格式
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
        #endregion

        /// <summary>
        /// 安全退出
        /// </summary>
        /// <param name="userName">用户名</param>
        public object SafetyExit(string userName) {//应该可以用没有测试过,可以的话前端也可以清除一次session
            DataCache.RemoveCacheByCacheKey(userName);//清除后台用户token缓存
            //清除浏览器缓存
            System.Web.HttpContext.Current.Response.Buffer = true;
            System.Web.HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
            System.Web.HttpContext.Current.Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
            System.Web.HttpContext.Current.Response.Expires = 0;
            System.Web.HttpContext.Current.Response.CacheControl = "no-cache";
            System.Web.HttpContext.Current.Response.Cache.SetNoStore();

            FormsAuthentication.SignOut();
            System.Web.HttpContext.Current.Request.Cookies.Clear();
            //System.Web.HttpContext.Current.Session.Abandon(); 　　//取消当前会话
            //System.Web.HttpContext.Current.Session.Clear(); //清除当前浏览器进程所有session 
            //System.Web.HttpContext.Current.Session.Clear(); //清除当前浏览器进程所有session 
            return true;
        }

        /// <summary>
        /// 批量删除作品信息，以及作品//无法删除作品内容
        /// </summary>
        /// <param name="deleUserById">删除的作品id</param>
        /// <returns></returns>
        public object DeleBatchUserInfoInfo(int[] deleUserById)
        {
            TMessage<UserInfo> mes = new TMessage<UserInfo>();
            var delUser = db.Deleteable<UserInfo>().Where(it => deleUserById.Contains(it.Id)).ExecuteCommand();//.ToSql();//.In(new int[] { 1,2,3}).ToSql();//.ExecuteCommand();
            var delWork = db.Deleteable<Works>().Where(it => deleUserById.Contains(it.AuthorId)).ExecuteCommand();//.ToSql();//.In(new int[] { 1,2,3}).ToSql();//.ExecuteCommand();
            if (delUser >= 1)
            {
                mes.suc = true;
                mes.mes = ConstHelper.DELETE_MODEL_SUCCESS + "用户" + delUser + "条，作品" + delWork+"条";
            }
            else
            {
                mes.suc = false;
                mes.mes = ConstHelper.GET_NOTHING + "," + ConstHelper.DELETE_MODEL_ERROR;
            }
            return mes;
        }


        /// <summary>
        /// 批量禁用用户
        /// </summary>
        /// <param name="deleUserById">删除的作品id</param>
        /// <returns></returns>
        public object updateBatchUserState(int[] updateUserById)
        {
            TMessage<UserInfo> mes = new TMessage<UserInfo>();
            var updatIsDeleted = //db.Updateable<Works>().Where(it => Id.Contains(it.Id)).ExecuteCommand();//.ToSql();//.In(new int[] { 1,2,3}).ToSql();//.ExecuteCommand();
                db.Updateable<UserInfo>().
                UpdateColumns(it => new UserInfo() { UserState = (int)Common.EnumType.StateResolution.ZeroType }).
                Where(it => updateUserById.Contains(it.Id)).ExecuteCommand();//.ToSql();//            if (delUser >= 1)
            if(updatIsDeleted>=1)
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
        /// 批量启用用户
        /// </summary>
        /// <param name="deleUserById">删除的作品id</param>
        /// <returns></returns>
        public object updateBatchUserStateOpen(int[] updateUserById)
        {
            TMessage<UserInfo> mes = new TMessage<UserInfo>();
            var updatIsDeleted = //db.Updateable<Works>().Where(it => Id.Contains(it.Id)).ExecuteCommand();//.ToSql();//.In(new int[] { 1,2,3}).ToSql();//.ExecuteCommand();
                db.Updateable<UserInfo>().
                UpdateColumns(it => new UserInfo() { UserState = (int)Common.EnumType.StateResolution.OneType }).
                Where(it => updateUserById.Contains(it.Id)).ExecuteCommand();//.ToSql();//            if (delUser >= 1)
            if (updatIsDeleted >= 1)
            {
                mes.suc = true;
                mes.mes = ConstHelper.UPDATE_MODEL_SUCCESS + updatIsDeleted + "条";
            }
            else
            {
                mes.suc = false;
                mes.mes = ConstHelper.GET_NOTHING + "," + ConstHelper.UPDATE_MODEL_ERROR;
            }
            return mes;
        }
        /// <summary>
        /// 判斷用戶狀態是否可用是否
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object UserState(int id) {
            var isAny = db.Queryable<UserInfo>().Where(it => it.Id == id && it.UserState == (int)Common.EnumType.StateResolution.ZeroType).Any();
            return isAny;
        }
    }
}
