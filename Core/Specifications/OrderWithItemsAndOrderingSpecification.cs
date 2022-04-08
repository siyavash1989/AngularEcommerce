using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities.OrderAggregate;

namespace Core.Specifications
{
    public class OrderWithItemsAndOrderingSpecification : BaseSpecification<Order>
    {
        public OrderWithItemsAndOrderingSpecification(string email) : base(o => o.BuyerEmail == email)
        {
            AddIncludes(p => p.OrderItems);
            AddIncludes(p => p.DeliveryMethod);
            AddOrderByDesc(p => p.OrderDate);
        }

        public OrderWithItemsAndOrderingSpecification(string email, int id) : base(o => o.BuyerEmail == email && o.Id == id)
        {
            AddIncludes(p => p.OrderItems);
            AddIncludes(p => p.DeliveryMethod);
        }
    }
}