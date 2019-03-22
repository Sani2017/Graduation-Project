using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
   public class EnumType 
    {
       /// <summary>
       /// 状态枚举，不同表的状态定义，可能不一样
       /// </summary>
       public enum StateResolution
       {
           /// <summary>
           /// 1.启用
           /// </summary>
           OneType = 1,
           /// <summary>
           /// 0.禁用
           /// </summary>
           ZeroType=0,

       }
       //public enum TroFResolution {
       //    FalseType = false,
       //    TrueType = true
       //}

    }
}
