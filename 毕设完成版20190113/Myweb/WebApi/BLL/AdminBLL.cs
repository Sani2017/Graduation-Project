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
    public class AdminBLL
    {
        AdminDAL dt = new AdminDAL();
                /// <summary>
        /// 查询所有管理员列表
        /// </summary>
        /// <returns></returns>
        public TMessage<List<Admin>> GetAdminListInfo()//JsonResult
        {
            return dt.GetAdminListInfo();
        }
                /// <summary>
        /// 管理员验证（用户名与密码）
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public TMessage<List<Admin>> AdminLogin(string userName, string password)
        {
            return dt.AdminLogin(userName, password);
        }
                /// <summary>
        /// 管理员注冊
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public object AddAdmin(AdminModel model)
        {
            return dt.AddAdmin(model);
        }
                /// <summary>
        /// 判断用戶名是否重复
        /// </summary>
        /// <param name="userName">查询用的类型名称</param>
        /// <returns></returns>
        public bool SeleUserName(string userName)
        {
            return dt.SeleUserName(userName);
        }
                /// <summary>
        /// 删除管理员信息
        /// </summary>
        /// <returns></returns>
        public object delAdminById(int id) {
            return dt.delAdminById(id);
        }
                /// <summary>
        /// 获取管理页首页数据
        /// </summary>
        /// <returns></returns>
        public object getAdminIndexInfo() {
            return dt.getAdminIndexInfo();
        }
                /// <summary>
        /// 作品类别下作品总数
        /// </summary>
        /// <returns></returns>
        public object getSortNameAndCount() {
            return dt.getSortNameAndCount();
        }
                /// <summary>
        /// 用于“admin用户是否启用列表页
        /// </summary>
        /// <param name="searchValue">搜索值</param>
        /// <param name="page">页码</param>
        /// <param name="userState">用户状态1.启用，0.不启用)默认1</param>
        /// <returns></returns>
        public object selectUserAndWorkSum(string searchValue, int page, int userState)
        {
            return dt.selectUserAndWorkSum(searchValue, page, userState);
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
        public object getAllowShowWorkAndUserInfo(int page, string selectVal)
        {
            return dt.getAllowShowWorkAndUserInfo(page, selectVal);
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
            return dt.GetActivityInfo(lookState, page, searchId, selectVal);
        }
    }

}
