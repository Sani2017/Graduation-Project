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
namespace DAl.Controllers
{
    public class SortController : ApiController
    {
       SortBLL dt=new SortBLL();
        /// <summary>
        /// 获取表中所有分类信息
        /// </summary>
        /// <returns></returns>
       [AcceptVerbs("Get", "Options")]
       [FunctionView("获取表中所有分类信息", OperationType.RETRIEVE)]
       [Route("api/Sort/GetAllSort")] 
       public object GetAllSort() {
           return dt.AllSort();
       }
        /// <summary>
       /// 增加作品类型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
       [AcceptVerbs("Post", "Options")]
       [FunctionView("增加作品类型", OperationType.CREATE)]
       [Route("api/Sort/AddSortInfo")] 
        public TMessage<Sort> AddSortInfo(SortModel model)
       {
           TMessage<Sort> mes = new TMessage<Sort>();
           if (string.IsNullOrWhiteSpace(model.SortName))
           {
               mes.suc = false;
               mes.mes = ConstHelper.NOT_NULL_ELEMENT_CONTENT;
               return mes;
           }
            return dt.AddSort(model.SortName.Trim());
       }
        /// <summary>
        /// 根据Id删除作品类型
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
       [AcceptVerbs("Post", "Options")]
       [FunctionView("获取表中所有分类信息", OperationType.DELETE)]
       [Route("api/Sort/DelSortInFoById")]
       public TMessage<Sort> DelSortInFoById(SortModel model)
       {
           TMessage<Sort> mes = new TMessage<Sort>();
           if (model.Id == 0)
           {//.ToString().Trim())
               mes.suc = false;
               mes.mes = ConstHelper.NOT_NULL_ELEMENT_CONTENT;
               return mes;
           }
           return dt.DelSortById(model.Id);
       }
        /// <summary>
        /// 更新作品类型信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
       [AcceptVerbs("Post", "Options")]
       [FunctionView("获取表中所有分类信息", OperationType.UPDATE)]
       [Route("api/Sort/UpateSortInFo")]
       public TMessage<Sort> UpateSortInFo(SortModel model)
       {
           TMessage<Sort> mes = new TMessage<Sort>();
           if(string.IsNullOrWhiteSpace(model.SortName)){
               mes.suc = false;
               mes.mes = ConstHelper.NOT_NULL_ELEMENT_CONTENT;
               return mes;
           }
           return dt.UpadteSortInFo(model);
       
       }
        /// <summary>
       /// 批量删除作品类型的信息
        /// </summary>
        /// <param name="deleBatchById"></param>
        /// <returns></returns>
       [AcceptVerbs("post", "Options")]
       [FunctionView("批量删除活动信息", OperationType.DELETE)]
       [Route("api/Sort/DeleBatchSort")]
       public object DeleBatchSort(int[] DeleBatchById)
       {
           return dt.DeleBatchSort(DeleBatchById);
       }
       //[AcceptVerbs("Post", "Options")]
       //[FunctionView("增加作品类型", OperationType.CREATE)]
       //[Route("api/Sort/DelSortById")]
       // public TMessage<Sort> DelSortById(int Id)
       //{
       //    TMessage<Sort> mes = new TMessage<Sort>();
       //    if (string.IsNullOrWhiteSpace(Id.ToString()))
       //    {//.ToString().Trim())
       //        mes.suc = false;
       //        mes.mes = ConstHelper.NOT_NULL_INFORMATION_CONTENT;
       //        return mes;
       //    }
       //    return dt.DelSortById(Id);
       //}
    }
}
