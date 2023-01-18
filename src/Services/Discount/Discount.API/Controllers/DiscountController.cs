using Discount.API.Entities;
using Discount.API.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Discount.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountController(IDiscountRepository discountRepository)
        {
            this._discountRepository = discountRepository;
        }

        [HttpGet("{productname}",Name ="GetDicount")]
        [ProducesResponseType(type:typeof(Coupon),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> GetDiscount(string productname)
        {
            var discount=await _discountRepository.GetDiscount(productname);
            return Ok(discount);
        }

        [HttpPost]
        [ProducesResponseType(type: typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> CreateDiscount(Coupon coupon)
        {
            await _discountRepository.CreateDiscount(coupon);
            return CreatedAtRoute("GetDiscount", new { productname = coupon.ProductName });
        }

        [HttpPut]
        [ProducesResponseType(type: typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DiscountDiscount(Coupon coupon)
        {
            await _discountRepository.UpdateDiscount(coupon);
            return CreatedAtRoute("GetDiscount", new { productname = coupon.ProductName });
        }

        [HttpDelete]
        [ProducesResponseType(type: typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeletetDiscount(string productName)
        {
            return Ok(await _discountRepository.DeleteDiscount(productName));
            
        }
    }
}
