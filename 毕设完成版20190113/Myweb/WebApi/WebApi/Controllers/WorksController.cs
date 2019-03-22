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
using System.Collections;
using HtmlAgilityPack;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Web;
using System.Web.Hosting;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DAl.Controllers
{
    public class WorksController : ApiController
    {
        WorksBLL dt = new WorksBLL();
        /// <summary>
        /// 获取表中所有作品信息
        /// </summary>
        /// <returns></returns>OperationType.RETRIEVE
        [AcceptVerbs("Get", "Options")]
        [FunctionView("获取表中所有作品信息", OperationType.RETRIEVE)]
        [Route("api/Works/GetAllWorks")]
        public TMessage<List<Works>> GetAllWorks()//List<post>
        {
            return dt.AllWorks();
        }
        /// <summary>
        /// 修改作品表内容
        /// </summary>
        /// <returns>11个内容要全部上传，数据层会判断该是否更改</returns>
        [AcceptVerbs("post", "Options")]
        [FunctionView("修改作品表内容", OperationType.UPDATE)]
        [Route("api/Works/UpdateAllWorks")]
        public TMessage<Works> UpdateAllWorks(WorkModel model)
        {
            //var asd=true;添加判断model中的值是否为空
            TMessage<Works> mes = new TMessage<Works>();
            if (string.IsNullOrWhiteSpace(model.Id.ToString()))
            {//所有传参不能为空
                mes.suc = false;
                mes.mes = ConstHelper.ID_NEEDE;
                return mes;
            }
            if (model == null)
            {
                mes.suc = false;
                mes.mes = ConstHelper.NOT_NULL_ELEMENT_CONTENT;
                return mes;
            }
            return dt.UpdateWorksAll(model);
        }
        /// <summary>
        /// 根据作者Id去获取相应的作品数量
        /// </summary>
        /// <returns></returns>OperationType.RETRIEVE
        [AcceptVerbs("Get", "Options")]
        [FunctionView("根据作者Id获取表中作品数量", OperationType.RETRIEVE)]
        [Route("api/Works/GetWorksInfoByAuthorId")]
        public object GetWorksInfoByAuthorId(int AuthorId)
        {
            return dt.GetAllWorksInfoByAuthorId(AuthorId);
        }

        /// <summary>
        /// 获取点赞榜排名
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("Get", "Options")]
        [FunctionView("获取点赞榜排名", OperationType.RETRIEVE)]
        [Route("api/Works/GetAllWorksLikesCountInfo")]
        public object GetAllWorksLikesCountInfo()
        {
            return dt.GetAllWorksLikesCount();
        }
        /// <summary>
        /// 真删除
        /// 批量删除作品信息
        /// </summary>
        /// <param name="delBatchWorks">删除作品的id</param>
        /// <returns></returns>
        [AcceptVerbs("post", "Options")]
        [FunctionView("批量删除作品信息", OperationType.DELETE)]
        [Route("api/Works/DelBatchWorksById")]
        public object DelBatchWorksById(int[] delBatchWorks)
        {
            return dt.DeleBatchWorksInfo(delBatchWorks);
        }

        /// <summary>
        /// 软删除
        /// 修改作品的删除状态,软删除(IsDeleted,是否标识已删除)
        /// 我还要考虑一下（软删除 +  定时任务）
        /// </summary>
        [AcceptVerbs("post", "Options")]
        [FunctionView("修改作品的删除状态,软删除(IsDeleted,是否标识已删除)", OperationType.RETRIEVE)]
        [Route("api/Works/UpdateWorkIsDeleted")]
        
        public object UpdateWorkIsDeleted(int[] Id)//TextClass data
        {
            //int id = data.Child.Count; true;
            return dt.UpdateWorkIsDeleted(Id);
        }
                //[AcceptVerbs("post", "Options")]
        /// <summary>
        /// 批量复原作品信息update
        /// </summary>
        /// <param name="Id">软删除作品id</param>
        /// <returns></returns>
        [AcceptVerbs("post", "Options")]
        [FunctionView("批量复原作品信息)", OperationType.RETRIEVE)]
        [Route("api/Works/UpdateWorkrevert")]

        public object UpdateWorkrevert(int[] Id)
        {
            return dt.UpdateWorkrevert(Id);
        }

        /// <summary>
        /// 复原作品可查询show update
        /// </summary>
        /// <param name="Id">软删除作品id</param>
        /// <returns></returns>
        [AcceptVerbs("get", "Options")]
        [FunctionView("批量复原作品信息)", OperationType.RETRIEVE)]
        [Route("api/Works/UpdateWorkrevert")]
        public object UpdateWorkShow(int Id)
        {
            return dt.UpdateWorkShow(Id);
        }

        //public class TextClass {
        //    public int Id { get; set; }
        //    public List<TestChildClass> Child { get; set; }
        //}
        //public class TestChildClass {
        //    public int textId { get; set; }
        //}
        /// <summary>
        /// 根据是否删除的状态去删除作品信息（定时）
        /// </summary>
        /// <param name="isDeleted">是否标识已删除[1:是，0:否],默认值:0</param>
        /// <returns></returns>
        [AcceptVerbs("post", "Options")]
        [FunctionView("批量删除作品信息", OperationType.DELETE)]
        [Route("api/Works/DelWorkInfoByIsDeleted")]
        public object DelWorkInfoByIsDeleted()
        {
            return dt.DelWorkInfoByIsDeleted();
        }
        /// <summary>
        /// 增加作品的点赞量
        /// </summary>
        /// <param name="id">作品的id</param>
        /// <returns></returns>
        [AcceptVerbs("get", "Options")]
        [FunctionView("增加作品的点赞量", OperationType.UPDATE)]
        [Route("api/Works/UpdatWorksLikes")]
        public object UpdatWorksLikes(int id)
        {
            return dt.UpdatWorksLikesBll(id);
        }

        /// <summary>
        /// 增加作品信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        //[AcceptVerbs("post", "Options")]
    [HttpPost]
        //[FunctionView("增加作品信息", OperationType.CREATE)]
        [Route("api/Works/AddWorksInfo")]
        public object AddWorksInfo(WorkModel model)
        {
            TMessage<Works> mes = new TMessage<Works>();
            if (string.IsNullOrWhiteSpace(model.Title.ToString()))
            {
                mes.suc = false;
                mes.mes = ConstHelper.NOT_NULL_INFORMATION_TITLE;
                return mes;
            }
            if (string.IsNullOrWhiteSpace(model.Content))
            {
                mes.suc = false;
                mes.mes = ConstHelper.NOT_NULL_INFORMATION_CONTENT;
                return mes;
            }
            if (string.IsNullOrWhiteSpace(model.Sort.ToString()))
            {
                mes.suc = false;
                mes.mes = ConstHelper.NOT_NULL_SORT_CONTENT;
                return mes;
            }
            return dt.AddWorksInfoBll(model);
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
        /// <param name="searchValue">搜索内容</param>
        /// <param name="page">当前页数</param>
        /// <param name="pageName">页面名称</param>
        /// <param name="sortId">类型Id</param>
        /// <returns></returns>
        [AcceptVerbs("Get", "Options")]
        [Route("api/Works/GetWorkAndUserInfoList")]
        //[Route("api/Works/aass")]
        public object GetWorkAndUserInfoList(int lookState, string searchValue, int page, string pageName, int sortId)
        {
            return dt.GetWorkAndUserInfoList(lookState, searchValue, page, pageName, sortId);
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
        [AcceptVerbs("Get", "Options")]
        [Route("api/Works/GetWorkInfoByUserId")]
        public object GetWorkInfoByUserId(int lookState, int page, int userId)
        {
            return dt.GetWorkInfoByUserId(lookState, page, userId);
        }

        /// <summary>
        /// 根据用户Id输出作品信息,用于用户私人页的作品输出
        /// </summary>
        /// <param name="page">当前页数</param>
        /// <param name="userId">用户Id</param>
        /// <param name="sortId">类型Id</param>
        /// <param name="if_show">是否显示[0:否,1:是],默认值:0</param>
        /// <param name="if_Deleted">是否软删除[1:是，0:否],默认值:0</param>
        /// 全部 3:3
        /// 是否显示 0:0
        /// 已通过 1:0 
        /// 已删除 3:1
        /// <returns></returns>
        [AcceptVerbs("Get", "Options")]
        [Route("api/Works/GetWorkInfoByUserIdForUserPersonal")]

        public object GetWorkInfoByUserIdForUserPersonal(int page, int userId, int sortId, int if_show, int if_Deleted)
        {
            return dt.GetWorkInfoByUserIdForUserPersonal(page, userId, sortId, if_show, if_Deleted);
        }

        /// <summary>
        /// 调用两张表（用户，作品）模糊查询名称
        /// </summary>
        /// <param name="seleName"></param>
        /// <returns></returns>
        [AcceptVerbs("Get", "Options")]
        [FunctionView("调用两张表（用户，作品）模糊查询名称", OperationType.RETRIEVE)]
        [Route("api/Works/SelectInput")]

        [Route("api/Works/SelectInput")]
        public object SelectInput(string selectName)
        {
            return dt.SelectInputBll(selectName);
        }
        /// <summary>
        /// 用于榜单的输出。未测试
        /// </summary>
        ///  <param name="SortId">类型id，默认为0，不筛选全表无条件输出</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Works/PopularList")]
        public object PopularList(int SortId)
        {
            return dt.PopularList(SortId);
        }  
                /// <summary>
        /// 根据活动id查找作品
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Works/GetWorkByActivityId")]
        public object GetWorkByActivityId(int activityId, int page, int lookState)
        {
            return dt.GetWorkByActivityId(activityId, page, lookState);
        }
        /// <summary>
        /// 根据id查找作品信息
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
       [HttpGet]
       [Route("api/Works/GetWorkInfoByWorkId")]
        public object GetWorkInfoByWorkId(int workId)
        {
           return dt.GetWorkInfoByWorkId(workId);
        }
       /// <summary>
       /// 通过id增加作品浏览量
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       [HttpGet]
       [Route("api/Works/UpdatWorksHits")]
        public object UpdatWorksHits(int id)
       {
           return dt.UpdatWorksHits(id);
       }

       /// <summary>
       /// 通过id增加作品赞数
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       [HttpGet]
       [Route("api/Works/UpdatWorksLikesCount")]
       public object UpdatWorksLikesCount(int id)
       {
           return dt.UpdatWorksLikesCount(id);
       }
       /// <summary>
       /// 根据作品id查询该作品是否通过审核与是否删除
       /// </summary>
       /// <param name="workId"></param>
       /// <returns></returns>
       [HttpGet]
       [Route("api/Works/outputWorkStateById")]
        public object outputWorkStateById(int workId)
       {
           return dt.outputWorkStateById(workId);
       }
        /// <summary>
        /// 输出作品以及相关信息，用于发现页
        /// </summary>
        /// <param name="searchState">查询的状态</param>
        /// <param name="saerchValue">查询的值</param>
        /// <returns></returns>
        //public object GetWorksAndUserInfoBySearch(string searchState, string saerchValue) { 

        //}
        ////以下是不经过数据交互的
        //[AcceptVerbs("post", "Options")]
        //[FunctionView("获取表中所有作品信息", OperationType.RETRIEVE)]
        //[Route("api/Works/GetAllWorks")]

        /// <summary>
        /// 获取到图片所在的路径  获取了img的src  content应该是富文本上传的内容，到这里过滤一下，查看用户是否是上传后台，后前端删除
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("get", "Options")]
        [Route("api/Works/GetImagesPath")]
        public object GetImagesPath(string content)
        {//这个是有用的，但是路由出问题了

            HtmlWeb webClient = new HtmlWeb();
            HtmlDocument doc = webClient.Load("http://localhost:6992/HtmlPage1-1.html");//(content);


            var imgObj = doc.DocumentNode.SelectSingleNode("//img");
            string src = "a";
            if (imgObj != null)
            {
                //foreach(){}
                src = imgObj.Attributes["src"].Value;
                return "img src:" + src;
            }
            return AppDomain.CurrentDomain.BaseDirectory + "\\uploadFile\\image";
            //return src;
            //return new { Data = getAll, Total = page };//返回多個值。
        }
        //关于去客户端上传的图片img路径问题，可以用js解决取src="";
        //HtmlAgilityPack无法识别到富文本所在的iframe内的内容！！！
        [AcceptVerbs("get", "Options")]
        [Route("api/Works/GetServerImg")]
        public object GetServerImg()
        {
            string imgtype = "*.BMP|*.JPG|*.GIF|*.png";
            string[] ImageType = imgtype.Split('|');//多种图片格式
            string[] newDirs;
            for (int i = 0; i < ImageType.Length; i++)
            {
                //第一次上传作品图片的时候日期一定是当天的，所以获取当前年月日
                //若是第二次修改，取数据库中的创建日期与当前时间进行比对。
                //若不相等这以创建时间为路径文件名，否则以当前时间为路径文件名
                string newUrl = AppDomain.CurrentDomain.BaseDirectory + "upload\\image";//+date
                string[] dirs = Directory.GetFiles(newUrl, ImageType[i]);
                int j = 0;
                foreach (string dir in dirs)
                {
                    string FileName = Path.GetFileName(dir);//获取文件名
                    FileStream fs = new FileStream(dir, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    byte[] photo = br.ReadBytes((int)fs.Length);
                    br.Close();
                    fs.Close();
                    j++;
                }
                newDirs = dirs;// return newDirs;
            }
            return false;
        }

        //测试——————————————————————————————
        /// <summary>
        /// 弃用缺少判断且复杂！
        /// 文件上传  
        /// 应该可以修改一下先判断一下上传文件的后缀，用于富文本的图片回显
        /// </summary>
        /// <param name="guid">GUID由前端生成</param>
        /// <returns></returns>
        //[HttpPost]
        [Route("api/Upload")]
        public async Task<string> PostFile()//string guid
        {
            string guid = DESCryption.Encode(DateTime.Now.Second.ToString());//new Guid().ToString();
            //if (!Request.Content.IsMimeMultipartContent())
            //{
            //    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            //}
            string uploadFolderPath = HostingEnvironment.MapPath("~/Upload/file");

            //如果路径不存在，创建路径
            if (!Directory.Exists(uploadFolderPath))
                Directory.CreateDirectory(uploadFolderPath);

            List<string> files = new List<string>();
            var provider = new WithExtensionMultipartFormDataStreamProvider(uploadFolderPath, guid);
            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                // This illustrates how to get the file names.

                foreach (var file in provider.FileData)
                {//接收文件
                    files.Add(Path.GetFileName(file.LocalFileName));
                }
            }
            catch
            {
                throw;
            }
            return string.Join(",", files);
        }
        public class WithExtensionMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
        {
            public string guid { get; set; }

            public WithExtensionMultipartFormDataStreamProvider(string rootPath, string guidStr)
                : base(rootPath)
            {
                guid = guidStr;
            }

            public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
            {
                string extension = !string.IsNullOrWhiteSpace(headers.ContentDisposition.FileName) ? Path.GetExtension(GetValidFileName(headers.ContentDisposition.FileName)) : "";
                return guid + extension;
            }

            private string GetValidFileName(string filePath)
            {
                char[] invalids = System.IO.Path.GetInvalidFileNameChars();
                return String.Join("_", filePath.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd('.');
            }
        }

        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="fPath">文件相对地址</param>
        [AcceptVerbs("get", "Options")]
        [Route("api/Works/DownloadFile")]
        public void DownloadFile(string fileName, string fPath)
        {
            //http://localhost:6992/api/UserInfo/DownloadFile?fileName=kkjkj&fPath=/upload/1234567890.zip
            try
            {
                string strFilePath = System.Web.HttpContext.Current.Server.MapPath("~") + fPath;//服务器文件路径
                FileInfo fileInfo = new FileInfo(strFilePath);
                //string OutFileName = "NewFileName";
                //下载文件重命名
                string tempfile = Path.GetFileName(fileInfo.Name);//获取文件名称
                tempfile = fileName + tempfile.Substring(tempfile.LastIndexOf("."));//获取文件后缀拼接文件名字符串
                string DownloadFileName = null;
                //string a=System.Web.HttpContext.Current.Request.Browser.Type;
                // string curBrowser = HttpContext.Current.Request.Browser.Type.ToLower();
                // string ab=System.Web.HttpContext.Current.Request.Browser.Version;
                // bool adl=System.Web.HttpContext.Current.Request.Browser.Cookies;
                // string add=System.Web.HttpContext.Current.Request.Browser.Platform;
                // string affd = System.Web.HttpContext.Current.Request.UserHostAddress;
                // string b = System.Web.HttpContext.Current.Request.Browser.Browser;
                //判断当前用户使用的浏览器类型，解决不同浏览器附件名乱码现象。
                string browser = System.Web.HttpContext.Current.Request.Browser.Browser.ToUpper();
                if (browser.Contains("FIREFOX") == true)//火狐内核比较特殊//(browser.Contains("MS") == true && browser.Contains("IE") == true)
                {
                    DownloadFileName = "\"" + tempfile + "\"";
                    // 这一步调试时中文文件名也是正常。在ie中中文显示正常，但在firefox中，中文依然为
                    //乱码，所以这里要判断用户使用浏览器类型，来保持中文文件名的正常显示
                }
                else//chrome内核ie内等核用此方法修改
                {
                    DownloadFileName = HttpUtility.UrlEncode(tempfile);// 这一步弹出下载保存的对话框，出现文件名乱码，但变量中的文件名是正常的。
                }

                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.Charset = "GB2312";
                System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + DownloadFileName);//使用重命名
                // HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                //System.Web.HttpContext.Current.Server.UrlEncode(fileName + fileInfo.Name) 这个是可以的，显示上传时加密的文件名称
                System.Web.HttpContext.Current.Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                System.Web.HttpContext.Current.Response.ContentType = "application/x-bittorrent";
                System.Web.HttpContext.Current.Response.WriteFile(fileInfo.FullName);
                System.Web.HttpContext.Current.Response.End();
                //return true;
            }
            catch// (Exception ex)
            {
                //Console.WriteLine("An error occurred: '{0}'", ex);
                //throw ex; 
                //return false; //返回错误描述
            }
            //string filePath = System.Web.HttpContext.Current.Server.MapPath(fPath);//路径
            ////以字符流的形式下载文件
            //FileStream fs = new FileStream(filePath, FileMode.Open);
            //byte[] bytes = new byte[(int)fs.Length];
            //fs.Read(bytes, 0, bytes.Length);
            //fs.Close();
            //System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
            ////通知浏览器下载文件而不是打开
            //System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;   filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
            //System.Web.HttpContext.Current.Response.BinaryWrite(bytes);
            //System.Web.HttpContext.Current.Response.Flush();
            //System.Web.HttpContext.Current.Response.End(); 

        }


        //[HttpPost]
        /// <summary>
        /// 富文本上传图片，封面上传
        /// 不能给它定义路径，会报无法访问405错误
        /// </summary>
        /// <returns></returns>
        //[Route("api/Works/ImgUpload")]
        public object ImgUpload()
        {
            var files = HttpContext.Current.Request.Files;
            if (files.Count <= 0)
            {
                return new { errno = 1, msg = "没有文件" };
            }
            var filesEx = new string[] { ".jpg", ".png" };  //可以上传的后缀名
            string saveTempPath = AppDomain.CurrentDomain.BaseDirectory + "\\Upload\\img\\";// "~/SayPlaces/" + "/SayPic/SayPicTemp/";
            var item = files[0];
            var fileExtension = Path.GetExtension(item.FileName).ToLower();
            if (!filesEx.Any(f => f == fileExtension))
            {
                //文件不能上传
                return new { errno = 1, msg = "不支持该类型文件，只支持.jpg.png" };
            }
            var fileLength = item.ContentLength;
            if (item.ContentLength > 1024 * 1024 * 3)
            {
                //文件大于2M
                return new { errno = 1, msg = "文件不能大于3MB" };
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
            return new { errno = 0, data = new string[] { "http://localhost:6992/Upload/img/" + newItemFileName },
                                    ImgUrl = newItemFileName,
                                    relativePath = "/upload/userImg/" + newItemFileName};  //拼接可访问呢的http路径, relativePath = "/Upload/img/" + newItemFileName 
        }
        public string Options()
        {
            return null;
        }
        /// <summary>
        /// 作品附件上传
        /// </summary>
        /// <returns></returns>
        [Route("api/Works/zipFileUpload")]
        public object zipFileUpload()
        {
            var files = HttpContext.Current.Request.Files;
            if (files.Count <= 0)
            {
                return new { errno = 1, msg = "没有文件" };
            }
            var filesEx = new string[] { ".zip", ".rar" };  //可以上传的后缀名
            string saveTempPath = AppDomain.CurrentDomain.BaseDirectory + "\\Upload\\file\\";// "~/SayPlaces/" + "/SayPic/SayPicTemp/";
            var item = files[0];
            var fileExtension = Path.GetExtension(item.FileName).ToLower();
            if (!filesEx.Any(f => f == fileExtension))
            {
                //文件不能上传
                return new { errno = 1, msg = "不支持该类型文件，只支持.zip.rar" };
            }
            var fileLength = item.ContentLength;
            if (item.ContentLength > 1024 * 1024 * 10)
            {
                //文件大于10M
                return new { errno = 1, msg = "文件不能大于10MB" };
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
                data = new string[] { "http://localhost:6992/Upload/file/" + newItemFileName },
                relativePath = "/upload/file/" + newItemFileName
            };  //拼接可访问呢的http路径, relativePath = "/Upload/img/" + newItemFileName 
        }

        /// <summary>
        /// 用户头像上传
        /// </summary>
        /// <returns></returns>
        [Route("api/Works/UserImgUpload")]
        public object UserImgUpload()
        {
            var files = HttpContext.Current.Request.Files;
            if (files.Count <= 0)
            {
                return new { errno = 1, msg = "没有文件" };
            }
            var filesEx = new string[] { ".jpg", ".png" };  //可以上传的后缀名
            string saveTempPath = AppDomain.CurrentDomain.BaseDirectory + "\\Upload\\userImg\\";// "~/SayPlaces/" + "/SayPic/SayPicTemp/";
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
                data = new string[] { "http://localhost:6992/Upload/userImg/" + newItemFileName },
                //ImgUrl = newItemFileName,/upload/userImg/02.jpg
                relativePath = "/upload/userImg/" + newItemFileName
            };  //拼接可访问呢的http路径
        }
        /// <summary>
        /// 删除本地文件
        /// </summary>
        /// <param name="fileUrl">文件路径</param>
        /// <returns></returns>
        //[AcceptVerbs("post", "Options")]
        //[HttpPost]
        //[FunctionView("删除本地文件", OperationType.DELETE)]
        [HttpGet]
        [Route("api/Works/DeleFiles")]
        public object DeleFiles(string fileUrl)
        {
            try
            {
                string realpath = System.Web.HttpContext.Current.Server.MapPath(fileUrl); ;
                bool bl = System.IO.File.Exists(realpath);
                if (bl)
                {
                    System.IO.File.Delete(realpath);
                    //Result.State = DeleteState.Success;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        //public string Options()
        //{
        //    return null;
        //}
    }




}