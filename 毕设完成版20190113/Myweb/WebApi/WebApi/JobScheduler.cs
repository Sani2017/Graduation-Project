using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using Quartz.Impl;
namespace WebApi
{
    public class JobScheduler
    {
        public static void Start()
        {
            //调度器工厂
            ISchedulerFactory factory = new StdSchedulerFactory();
            //调度器
            IScheduler scheduler = factory.GetScheduler();
            scheduler.GetJobGroupNames();

            /*-------------计划任务代码实现------------------*/
            //创建任务
            //IJobDetail job = JobBuilder.Create<TimeJob>().Build();
            //创建触发器
            //ITrigger trigger = TriggerBuilder.Create().WithIdentity("TimeTrigger", "TimeGroup").WithSimpleSchedule(t => t.WithIntervalInMinutes(1).RepeatForever()).Build();
            //添加任务及触发器至调度器中
           // scheduler.ScheduleJob(job, trigger);
            /*-------------计划任务代码实现------------------*/

            //启动
            scheduler.Start();
        }
    }
}