using Basket.API.Entities;
using Basket.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Basket.API.GrpcServices;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly DiscountGrpcServices _discountService;

        public BasketController(IBasketRepository basketRepository, DiscountGrpcServices discountService)
        {
            this._basketRepository = basketRepository;
            this._discountService = discountService;
        }
        [HttpGet("{userName}",Name ="GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var basket= await _basketRepository.GetBasket(userName);
            return Ok(basket ?? new ShoppingCart(userName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<ShoppingCart>> UpdateShoppingCart(ShoppingCart cart)
        {
            foreach(var item in cart.list)
            {
                var discount = 0;
                var model= await _discountService.GetDiscount(item.ProductName);
                discount = model.Amount;
                item.Price = item.Price - discount;
            }
            return await _basketRepository.UpdateBasket(cart);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]

        public async Task<ActionResult> DeleteBasket(string userName)
        {
            await _basketRepository.DeleteBasket(userName);
            return Ok();
        }
    }
}
