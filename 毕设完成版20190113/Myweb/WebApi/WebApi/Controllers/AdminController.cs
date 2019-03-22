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
using Newtonsoft.Json;
using System.Web;
using System.IO;
namespace WebApi.Controllers
{
    public class AdminController : ApiController
    {
        AdminBLL dt = new AdminBLL();
        /// <summary>
        /// 查询所有管理员列表
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("Get", "Options")]
        [FunctionView("查询所有管理员列表", OperationType.RETRIEVE)]
        [Route("api/Admin/GetAdminListInfo")]

        public TMessage<List<Admin>> GetAdminListInfo()//JsonResult
        {
            return dt.GetAdminListInfo();
        }
        /// <summary>
        /// 管理员登录验证（用户名与密码）
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [AcceptVerbs("Get", "Options")]
        [FunctionView("管理员验证（用户名与密码）", OperationType.RETRIEVE)]
        [Route("api/Admin/AdminLogin")]

        public TMessage<List<Admin>> AdminLogin(string userName, string password)
        {
            return dt.AdminLogin(userName, password);
        }
        /// <summary>
        /// 管理员注冊
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AcceptVerbs("post", "Options")]
        [FunctionView("管理员注冊", OperationType.RETRIEVE)]
        [Route("api/Admin/AddAdmin")]
        public object AddAdmin(AdminModel model)
        {
            return dt.AddAdmin(model);
        }
        /// 判断用戶名是否重复
        /// </summary>
        /// <param name="userName">查询用的类型名称</param>
        /// <returns></returns>
        [AcceptVerbs("get", "Options")]
        [FunctionView("管理员注冊", OperationType.RETRIEVE)]
        [Route("api/Admin/SeleUserName")]
        public bool SeleUserName(string userName)
        {
            return dt.SeleUserName(userName);
        }
        /// <summary>
        /// 删除管理员信息
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("post", "Options")]
        [FunctionView("删除管理员信息", OperationType.RETRIEVE)]
        [Route("api/Admin/delAdminById")]
        public object delAdminById(int id)
        {
            return dt.delAdminById(id);
        }
        /// <summary>
        /// 获取管理页首页数据
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("get", "Options")]
        [FunctionView("删除管理员信息", OperationType.RETRIEVE)]
        [Route("api/Admin/getAdminIndexInfo")]
        public object getAdminIndexInfo()
        {
            return dt.getAdminIndexInfo();
        }
        /// <summary>
        /// 作品类别下作品总数
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("get", "Options")]
        [FunctionView("删除管理员信息", OperationType.RETRIEVE)]
        [Route("api/Admin/getSortNameAndCount")]

        public object getSortNameAndCount()
        {
            return dt.getSortNameAndCount();
        }
        /// <summary>
        /// 用于“admin用户是否启用列表页
        /// </summary>
        /// <param name="searchValue">搜索值</param>
        /// <param name="page">页码</param>
        /// <param name="userState">用户状态1.启用，0.不启用)默认1</param>
        /// <returns></returns>
                [AcceptVerbs("get", "Options")]
        [FunctionView("删除管理员信息", OperationType.RETRIEVE)]
        [Route("api/Admin/selectUserAndWorkSum")]
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
        [AcceptVerbs("get", "Options")]
        [FunctionView("删除管理员信息", OperationType.RETRIEVE)]
        [Route("api/Admin/getAllowShowWorkAndUserInfo")]
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
        [AcceptVerbs("get", "Options")]
        [FunctionView("删除管理员信息", OperationType.RETRIEVE)]
        [Route("api/Admin/GetActivityInfo")]

        public object GetActivityInfo(int lookState, int page, int searchId, string selectVal)
        {
            return dt.GetActivityInfo(lookState, page, searchId, selectVal);
        }

        /// <summary>
        /// 管理员上传活动相关图片
        /// </summary>
        /// <returns></returns>
        //[Route("api/Admin/ActivityImgUpload")]
        public object ActivityImgUpload()
        {
            var files = HttpContext.Current.Request.Files;
            if (files.Count <= 0)
            {
                return new { errno = 1, msg = "没有文件" };
            }
            var filesEx = new string[] { ".jpg", ".png" };  //可以上传的后缀名
            string saveTempPath = AppDomain.CurrentDomain.BaseDirectory + "\\Upload\\activityImg\\";// "~/SayPlaces/" + "/SayPic/SayPicTemp/";
            var item = files[0];
            var fileExtension = Path.GetExtension(item.FileName).ToLower();
            if (!filesEx.Any(f => f == fileExtension))
            {
                //文件不能上传
                return new { errno = 1, msg = "不支持该类型文件，只支持.jpg.png" };
            }
            var fileLength = item.ContentLength;
            if (item.ContentLength > 1024 * 1024 * 2)
            {
                //文件大于2M
                return new { errno = 1, msg = "文件不能大于2MB" };
            }
            var newItemFileName = Guid.NewGuid().ToString("N") + fileExtension; //新的文件名  item.FileName是原本的文件名
            var newFileName = saveTempPath + newItemFileName;//  组成完整路径
            if (!Directory.Exists(saveTempPath))
            {
                Directory.CreateDirectory(saveTempPath);
            }
            try
            {
                item.SaveAs(newFileName);//保存文件
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { errno = 1, msg = ex.Message });
            }
            return new
            {
                errno = 0,
                data = new string[] { "http://localhost:6992/Upload/activityImg/" + newItemFileName },
                //ImgUrl = newItemFileName,/upload/userImg/02.jpg
                relativePath = "/upload/activityImg/" + newItemFileName
            };  //拼接可访问呢的http路径
        }
        public string Options()
        {
            return null;
        }


	}
}