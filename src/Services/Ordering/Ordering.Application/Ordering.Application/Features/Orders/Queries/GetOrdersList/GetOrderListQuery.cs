using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList
{
    public class GetOrderListQuery:IRequest<List<OrderVM>>
    {
        public string UserName { get; set; }
        public GetOrderListQuery(string userName)
        {
            UserName = userName;
        }
    }
}
