using Discount.GRPC.Protos;
using System.Threading.Tasks;

namespace Basket.API.GrpcServices
{
    public class DiscountGrpcServices
    {
        private readonly DiscountService.DiscountServiceClient _discountService;

        public DiscountGrpcServices(DiscountService.DiscountServiceClient discountService)
        {
            this._discountService = discountService;
        }
        
        public async Task<CouponModel> GetDiscount(string productName)
        {
            var getdiscountRequest = new GetDiscountRequest { ProductName = productName };
           return  await _discountService.GetDiscountAsync(getdiscountRequest);
        }
    }
}
