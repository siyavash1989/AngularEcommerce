using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregate
{
    public class DeliveryMethod:BaseEntity
    {
        public string ShortName { get; set; }
        public string Time { get; set; }
        public string Description { get; set; }
        public long Price { get; set; }
    }
}