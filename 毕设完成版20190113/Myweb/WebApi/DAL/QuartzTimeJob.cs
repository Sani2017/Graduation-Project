using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using Quartz.Impl;
using DAL;

namespace DAl
{
    public class QuartzTimeJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {//方法引用的问题
            //WorksDAL.DelWorkInfoByIsDeleted();
        }
    }
}