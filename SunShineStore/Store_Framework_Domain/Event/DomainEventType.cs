using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Framework_Domain.Event
{
    public class DomainEventType
    {
        /// <summary>
        /// 类型全称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public Type Type { get; set; }
    }
}
