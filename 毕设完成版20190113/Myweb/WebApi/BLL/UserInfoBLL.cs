using Common;
using DAL;
using Models.ModelTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BLL
{
    public class UserInfoBLL
    {
        UserInfoDAL dt = new UserInfoDAL();
        /// <summary>
        /// 获取所有用户信息BLL
        /// </summary>
        /// <returns></returns>
        public TMessage<List<UserInfo>> AllUserInfor() // JsonResult
        {
            return dt.GetUserInfo();
        }

        /// <summary>
        /// 获取所有用户以及作品信息BLL
        /// </summary>
        /// <returns></returns>
        public object AllUserWorksInfo()
        {
            return dt.GetWorksInfo();
        }
        /// <summary>
        /// 查询用户信息以及相关作品数量
        /// </summary>
        /// <returns></returns>
        public object selectUserAndWorkSum(string searchValue,int page)
        {
            return dt.selectUserAndWorkSum(searchValue ,page);
        }
                /// <summary>
        /// 用于大佬页——用户排名
        /// </summary>
        /// <param name="page">选择的页数</param>
        /// <returns></returns>
        public object PopularListUser(int page) {
            return dt.PopularListUser(page);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
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
        /// 用户登录验证BLL
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public TMessage<List<UserInfo>> UserLogin(string userName, string pwd)
        {
            return dt.UserLogin(userName, pwd);
        }
        /// <summary>
        /// 用戶注冊BLL
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public object AddUserInfo(UserInfoModel model)
        {
            return dt.AddUserInfo(model);
        }
        /// <summary>
        /// 判断用戶名是否重复BLL
        /// </summary>
        /// <param name="UserName">查询用的类型名称</param>
        /// <returns></returns>
        public bool SeleUserName(string userName)
        {
            return dt.SeleUserName(userName);
        }
        /// <summary>
        /// 判断📫是否重复BLL
        /// </summary>
        /// <param name="email">查询用的类型名称</param>
        /// <returns></returns>
        public bool SeleEmail(string email)
        {
            return dt.SeleEmail(email);
        }
        /// <summary>
        /// 判断手機號是否重复BLL
        /// </summary>
        /// <param name="PhoneNumber">查询用的类型名称</param>
        /// <returns></returns>
        public bool SelePhoneNumber(string phoneNumber)
        {
            return dt.SelePhoneNumber(phoneNumber);
        }

        /// <summary>
        /// 邮箱内容以及配置的编辑Bll
        /// </summary>
        /// <param name="userName">用户姓名</param>
        /// <param name="userEmali">用户邮箱</param>
        /// <param name="smtpName">用户邮箱后缀如：163.com，qq.com</param>
        public object EmailContent(string userName, string userEmali)//, string smtpName)
        {
            return dt.EmailContent(userName, userEmali);//, smtpName);
        }
        /// <summary>
        /// 邮箱注册，链接缓存验证Bll
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="validataCode">用户验证随机验证码</param>
        /// <returns></returns>
        public object EmailVerify(string userName, string validataCode)
        {
            return dt.EmailVerify(userName, validataCode);
        }
        /// <summary>
        /// 邮箱密码重置链接缓存验证Bll
        /// </summary>
        /// <param name="userValue">用户名</param>
        /// <param name="validataCode">用户验证随机验证码</param>
        /// <returns></returns>
        public object EmailVerifyPassword(string userValue, string validataCode)
        {
            return dt.EmailVerifyPassword(userValue, validataCode);
        }
        /// <summary>
        /// 重置密码BLL
        /// </summary>
        /// <returns></returns>
        public object ResetUserPassword(string userValue, string newPwd)
        {
            return dt.ResetUserPassword(userValue,newPwd);
        }
                /// <summary>
        /// 根据用户id更新用户信息
        /// </summary>
        /// <returns></returns>
        public object UpdateUserInfoByUserId(UpdateUserInfoModel model) {
            return dt.UpdateUserInfoByUserId(model);
        }
        /// <summary>
        /// 安全退出Bll
        /// </summary>
        public object SafetyExitBll(string userName)
        {
            return dt.SafetyExit(userName);
        }

                /// <summary>
        /// 批量禁用用户
        /// </summary>
        /// <param name="deleUserById">删除的作品id</param>
        /// <returns></returns>
        public object updateBatchUserState(int[] updateUserById)
        {
            return dt.updateBatchUserState(updateUserById);
        }
                /// <summary>
        /// 批量启用用户
        /// </summary>
        /// <param name="deleUserById">删除的作品id</param>
        /// <returns></returns>
        public object updateBatchUserStateOpen(int[] updateUserById)
        {
            return dt.updateBatchUserStateOpen(updateUserById);
        }
                /// <summary>
        /// 判斷用戶狀態是否可用是否
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object UserState(int id) {
            return dt.UserState(id);
        }
    }
}
