using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistance;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList
{
    public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, List<OrderVM>>
    {
        private readonly IOrderRepository _orderrepository;
        private readonly IMapper _mapper;

        public GetOrderListQueryHandler(IOrderRepository repository,IMapper mapper)
        {
            this._orderrepository = repository;
            this._mapper = mapper;
        }
        public async Task<List<OrderVM>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
        {
           var orderlist=await _orderrepository.GetOrderByUserName(request.UserName);
            return _mapper.Map<List<OrderVM>>(orderlist);
        }
    }
}
