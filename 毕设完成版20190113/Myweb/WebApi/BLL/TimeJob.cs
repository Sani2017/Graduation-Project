using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
namespace BLL
{
    public class TimeJob:IJob
    {
        WorksDAL dt = new WorksDAL();
        /// <summary>
        /// 创建删除的任务
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            //throw new NotImplementedException();
            dt.DelWorkInfoByIsDeleted();
        }
    }
} 
