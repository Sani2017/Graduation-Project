using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{    
    /// <summary>
    /// 返回类型消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TMessage<T> : Message
    {
        /// <summary>
        /// 额外的信息如表中信息等等
        /// </summary>
        public T extra { get; set; }
    }
}
