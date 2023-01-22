using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Discount.GRPC.Protos;
using System.Threading.Tasks;
using Grpc.Core;
using Discount.GRPC.Repository;
using AutoMapper;
using Discount.GRPC.Entities;

namespace Discount.GRPC.Services
{
    public class DiscountService:Discount.GRPC.Protos.DiscountService.DiscountServiceBase
    {
        private readonly ILogger<DiscountService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IDiscountRepository _repository;
        private readonly IMapper _mapper;

        public DiscountService(ILogger<DiscountService> logger,
            IConfiguration configuration,
            IDiscountRepository repository,
            IMapper mapper)
        {
            this._logger = logger;
            this._configuration = configuration;
            this._repository = repository;
            this._mapper = mapper;
        }

        public async override Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _repository.GetDiscount(request.ProductName);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"No Coupon code is avalaible for Product {request.ProductName}"));
            }
            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;

        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);
            var data = await _repository.CreateDiscount(coupon);
                return _mapper.Map<CouponModel>(coupon);
        }
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var isDeleted= await _repository.DeleteDiscount(request.ProductName);
            var deleterespose = new DeleteDiscountResponse{Success=isDeleted};
            return deleterespose;
        }
        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);
            var data = await _repository.UpdateDiscount(coupon);
            var updatedCpouponModel = _mapper.Map<CouponModel>(coupon);
            return updatedCpouponModel;
        }

    }
}
