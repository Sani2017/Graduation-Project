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
using System.Threading.Tasks;
using System.Web.Hosting;
using System.IO;
using System.Web;

using qcloudsms_csharp;
using qcloudsms_csharp.json;
using qcloudsms_csharp.httpclient;
using System.Web.Caching;
using System.Text;

namespace DAl.Controllers
{
    
    public class UserInfoController : ApiController
    {
        UserInfoBLL dt = new UserInfoBLL();
        /// <summary>
        /// 获取表中所有用户信息
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("Get", "Options")]
        [FunctionView("获取表中所有用户信息", OperationType.RETRIEVE)]
        [Route("api/UserInfo/GetAllUserInfo")]

        public TMessage<List<UserInfo>> GetAllUserInfo()//List<post>JsonResult
        {
            return dt.AllUserInfor();
        }

        /// <summary>
        /// 获取表中所有用户作品
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("Get", "Options")]
        [FunctionView("获取表中所有用户以及作品", OperationType.RETRIEVE)]
        [Route("api/UserInfo/GetAllUserWorksInfo")] 
        public object GetAllUserWorksInfo(){
            return dt.AllUserWorksInfo();
        }
        /// <summary>
        /// 查询用户信息以及相关作品数量
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public object selectUserAndWorkSum(string searchValue,int page)
        {
            return dt.selectUserAndWorkSum(searchValue,page);
        }
                /// <summary>
        /// 用于大佬页——用户排名
        /// </summary>
        /// <param name="page">选择的页数</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/UserInfo/PopularListUser")]
        public object PopularListUser(int page) {
            return dt.PopularListUser(page);
        }
        /// <summary>
        /// 根据用户id获取用户信息
        /// </summary>
        /// <param name="Id">用户Id</param>
        /// <returns></returns>
        [AcceptVerbs("Get", "Options")]
        [FunctionView("根据用户id获取用户信息", OperationType.RETRIEVE)]
        [Route("api/UserInfo/GetAllUserInfoById")]
        public object GetAllUserInfoById(int Id)
        {
            return dt.GetAllUserInfoById(Id);
        }
        /// <summary>
        /// 根据用户id获取用户信息 用于用户个人页非公共
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public object GetUserInfoByID(int id)
        {
            return dt.GetUserInfoByID(id);
        }
        /// <summary>
        /// 用户登录验证
        /// </summary>
        /// <param name="model">用户表传参model，有用户名登录与手机号登录</param>
        /// <returns></returns>
        [AcceptVerbs("Post", "Options")]
        [FunctionView("用户登录验证", OperationType.LOGON)]
        [Route("api/UserInfo/UserLogin")]
        public TMessage<List<UserInfo>> UserLogin(UserInfoModel model)
        {

            TMessage<List<UserInfo>> mes = new TMessage<List<UserInfo>>();
            //判断是否为空
            if (!string.IsNullOrWhiteSpace(model.UserName) || !string.IsNullOrWhiteSpace(model.PassWord))
            {
                return dt.UserLogin(model.UserName.Trim(), model.PassWord.Trim());
            }
            else { 
                mes.suc = false;
                mes.mes = ConstHelper.USERNAME_PASSWORD_EMPTY;
            } //判断是手机号还是用户名
            return mes;          
        }
        /// <summary>
        /// 用戶注冊
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        /// 1.注册成功就直接登录(暂定使用第一种，生成token去验证~)
        /// 2.返回登录页面再登录
        /// </returns>
        [AcceptVerbs("Post", "Options")]
        [FunctionView("用戶注冊", OperationType.OTHER)]
        [Route("api/UserInfo/AddUserInfo")]
        public object AddUserInfo(UserInfoModel model)
        {
            TMessage<UserInfo> mes = new TMessage<UserInfo>();

            Random rd = new Random();
            string random=rd.Next(1,6).ToString();
            if (string.IsNullOrWhiteSpace(model.PassWord.Trim())
                || string.IsNullOrWhiteSpace(model.UserPhone.ToString().Trim()))
            {
                mes.suc = false;
                mes.mes = ConstHelper.NOT_NULL_ELEMENT_CONTENT;
                return mes;
            }
            model.UserImg=GlobalConstant.USER_PHOTO_DIR+""+random+""+".jpg";
            model.UserName=GenerateCheckCode();
            return dt.AddUserInfo(model);
        }
        /// <summary>
        /// 生成随机用户名(数字字母混和)
        /// </summary>
        /// <param name="codeCount"></param>
        /// <returns></returns>
        private string GenerateCheckCode()
        {
            string str = string.Empty;
            int rep = 0;
            long num2 = DateTime.Now.Ticks + rep;
            rep++;
            Random random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> rep)));
            for (int i = 0; i < 8; i++)
            {
                char ch;
                int num = random.Next();
                if ((num % 2) == 0)
                {
                    ch = (char)(0x30 + ((ushort)(num % 10)));
                }
                else
                {
                    ch = (char)(0x41 + ((ushort)(num % 0x1a)));
                }
                str = str + ch.ToString();
            }
            return str;
        }
        /// <summary>
        /// 判断用戶名是否重复
        /// </summary>
        /// <param name="UserName">查询用的类型名称</param>
        /// <returns></returns>
        [AcceptVerbs("get", "Options")]
        [FunctionView("判断用戶名是否重复", OperationType.RETRIEVE)]
        [Route("api/UserInfo/SeleUserName")]
        public bool SeleUserName(string userName)
        {
            return dt.SeleUserName(userName);
        }
        /// <summary>
        /// 判断📫是否重复
        /// </summary>
        /// <param name="email">查询用的类型名称</param>
        /// <returns></returns>
        [AcceptVerbs("get", "Options")]
        [FunctionView("判断📫是否重复", OperationType.RETRIEVE)]
        [Route("api/UserInfo/SeleEmail")]
        public bool SeleEmail(string email)
        {
            return dt.SeleEmail(email);
        }
        /// <summary>
        /// 判断手機號是否重复
        /// </summary>
        /// <param name="PhoneNumber">查询用的类型名称</param>
        /// <returns></returns>
        [AcceptVerbs("get", "Options")]
        [FunctionView("判断手機號是否重复", OperationType.RETRIEVE)]
        [Route("api/UserInfo/SelePhoneNumber")]
        //[HttpPost]
        public bool SelePhoneNumber(string phoneNumber)
        {
            return dt.SelePhoneNumber(phoneNumber);
        }
        [AcceptVerbs("Post", "Options")]
        [Route("api/UserInfo/SelectPhone")]
        public bool SelectPhone(string phone)
        {
            return false;
        }
        //追加郵箱,手機驗證，與密碼重置等功能誒喲！！
//-------------------------------------------------------------------        
     
        /// <summary>
        /// 邮箱内容以及配置的编辑
        /// 邮箱发生内容样式还要完善
        /// 此处为邮件发送的主体
        /// </summary>
        /// <param name="userName">用户姓名</param>
        /// <param name="userEmali">用户邮箱</param>
        /// <param name="smtpName">用户邮箱后缀如：163.com，qq.com</param>
        [AcceptVerbs("get", "Options")]
        [FunctionView("邮箱内容以及配置的编辑", OperationType.RETRIEVE)]
        [Route("api/UserInfo/EmailContent")]
        public object EmailContent(string userName, string userEmali)//, string smtpName)
        {
            return dt.EmailContent(userName, userEmali);//, smtpName);
        }
        /// <summary>
        /// 邮箱发送功能一：用户激活
        /// 邮箱注册链接缓存验证。
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="validataCode">用户验证随机验证码</param>
        /// <returns></returns>
        [AcceptVerbs("get", "Options")]
        [FunctionView("邮箱注册链接缓存验证", OperationType.UPDATE)]
        [Route("api/UserInfo/EmailVerify")]
        public object EmailVerify(string userName, string validataCode)
        {
            return dt.EmailVerify(userName, validataCode);
        }

        /// <summary>
        /// 邮箱发送功能二：重置密码
        /// 邮箱重置密码链接缓存验证。
        /// </summary>
        /// <param name="userValue">用户名</param>
        /// <param name="validataCode">用户验证随机验证码</param>
        /// <returns></returns>
        [AcceptVerbs("get", "Options")]
        [FunctionView("邮箱注册链接缓存验证", OperationType.RETRIEVE)]
        [Route("api/UserInfo/EmailVerifyPassword")]
        public object EmailVerifyPassword(string userValue, string validataCode)
        {
            return dt.EmailVerifyPassword(userValue, validataCode);
        }
        /// <summary>
        /// 重置密码
        /// 要通过密码缓存验证true才能出现密码框
        /// 页面不跳转，从新渲染DOM
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("post", "Options")]
        [FunctionView("重置密码", OperationType.UPDATE)]
        [Route("api/UserInfo/ResetUserPassword")]
        public object ResetUserPassword(UserInfoModel model) {
            return dt.ResetUserPassword(model.UserPhone, model.PassWord);
        }
        /// <summary>
        /// 根据用户id更新用户信息
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("post", "Options")]
        [FunctionView("根据用户id更新用户信息", OperationType.UPDATE)]
        [Route("api/UserInfo/UpdateUserInfoByUserId")]
        public object UpdateUserInfoByUserId(UpdateUserInfoModel model)
        {
            TMessage<UserInfo> mes = new TMessage<UserInfo>();
            if(model.UserName!=null){
                if (SeleUserName(model.UserName)) {
                    mes.suc = false;
                    mes.mes = ConstHelper.USER_NAME_IS_TREGISTER;
                    return mes;
                }
            }
            if(model.UserPhone!=null){
                if (SelePhoneNumber(model.UserPhone)) {
                    mes.suc = false;
                    mes.mes = ConstHelper.USER_REGISTERED + "或" + ConstHelper.PARAMETER_ERROR;
                    return mes;
                }
            }
            if(model.Email!=null){
                if (SeleEmail(model.Email)) {
                    mes.suc = false;
                    mes.mes = ConstHelper.EMAIL_IS_TREGISTER;
                    return mes;
                }
            }
                return dt.UpdateUserInfoByUserId(model);

        }
        /// <summary>
        /// 安全退出
        /// </summary>
        [AcceptVerbs("get", "Options")]
        [Route("api/UserInfo/SafetyExit")]
        public object SafetyExit(string userName) {
            return dt.SafetyExitBll(userName);
        }

        /// <summary>
        /// 批量禁用用户
        /// </summary>
        /// <param name="deleUserById">删除的作品id</param>
        /// </summary>
        [AcceptVerbs("post", "Options")]
        [Route("api/UserInfo/updateBatchUserState")]
        public object updateBatchUserState(int[] updateUserById)
        {
            return dt.updateBatchUserState(updateUserById);
        }
        /// <summary>
        /// 批量启用用户
        /// </summary>
        /// <param name="deleUserById">删除的作品id</param>
        /// <returns></returns>
                [AcceptVerbs("post", "Options")]
                [Route("api/UserInfo/updateBatchUserStateOpen")]
        public object updateBatchUserStateOpen(int[] updateUserById)
        {
            return dt.updateBatchUserStateOpen(updateUserById);
        }

        /// <summary>
        /// 判斷用戶狀態是否可用是否
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/UserInfo/UserState")]
        public object UserState(int id)
        {
            return dt.UserState(id);
        }

        /// <summary>
        /// 发送短信测试版
        /// </summary>
        [AcceptVerbs("post", "Options")]
        [Route("api/UserInfo/PutSms")]
        public void PutSms(string[] phoneNumbers) {
            //string[] newPhoneNumbers={phoneNumbers};newPhoneNumbers
            //SmsHelper sms = new SmsHelper();　　//短信发送帮助类
           // sms.PutSms(phoneNumbers);
        }
        //[AcceptVerbs("post", "Options")]
        //[Route("api/UserInfo/SmsOut")]
        [HttpGet]
        public object SmsOut(string phone,string intState) {
            TMessage<UserInfo> mes = new TMessage<UserInfo>();
            if (NumberHelper.ValidPhoneNumber(phone.Trim()))
            {//后端验证是否是手机号 
                if (SelePhoneNumber(phone))//已存在该手机号
                {
                    string[] newPhoneNumbers = { phone };//newPhoneNumbers
                    SmsHelper sms = new SmsHelper();　　//短信发送帮助类
                    sms.PutSms(newPhoneNumbers);
                }
                else if (intState == "1")
                    {//注册状态下数据库无该手机号
                        string[] newPhoneNumbers = { phone };//newPhoneNumbers
                        SmsHelper sms = new SmsHelper();　　//短信发送帮助类
                        sms.PutSms(newPhoneNumbers);
                        mes.suc = true;
                    }
                    else { 
                        mes.suc = false;
                        mes.mes = ConstHelper.PHONE_ISNOT_REGISTER;
                    }
                
            }
            else {
                mes.suc = false;
                mes.mes= ConstHelper.PHONE_ERROR;
            }
            return mes;
        }
        /// <summary>
        /// 登录注册验证码校验
        /// </summary>
        /// <param name="CacheName">手机号</param>
        /// <param name="smsCd">验证码</param>
        /// <param name="intoState">操作状态0：登录；1：注册</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/UserInfo/SmsCache")]
        public object SmsCache(string CacheName, string smsCd, string intoState)
        {
            TMessage<UserInfo> mes = new TMessage<UserInfo>();
            if (NumberHelper.ValidPhoneNumber(CacheName))
            {//后端验证是否是手机号 

                var phoneCache = DataCache.GetCacheTorF(CacheName);
                if (phoneCache != null) {
                    if (DataCache.GetCache(CacheName).Equals(smsCd))
                    {
                        DataCache.RemoveCacheByCacheKey(CacheName);//与前端验证码相符则删除服务器缓存。
                       
                        if (intoState == "1")
                        {
                            mes.suc = true;//前端注册接口调用，判断用户输入手机验证码是否正确
                        }
                        else {
                            string phone_Cd = "yzm";
                            return dt.UserLogin(CacheName, phone_Cd);//登录，并查询信息
                        }
                    }
                }else
                {
                    mes.suc = false;
                    mes.mes = ConstHelper.VERIFICATION_CODE_ERROR;
                }

            }
            else {
                mes.suc = false;
                mes.mes= ConstHelper.PHONE_ERROR;
            }
            return mes;

        }
        //public void asd() {
        //    string a = "";
        //}
        //        [AcceptVerbs("post", "Options")]

        //[Route("api/UserInfo/asd")]
        //public void PutSms()
        //{
        //    //SmsHelper.PutSms(phoneNumbers, random);

        //}
    }
}
