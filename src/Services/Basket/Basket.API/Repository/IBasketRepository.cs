using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Basket.API.Repository
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasket(string userName);
        Task<ShoppingCart> UpdateBasket(ShoppingCart cart);
        Task DeleteBasket(string userName);
    }

    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _rediscache;

        public BasketRepository(IDistributedCache distributedCache)
        {
            this._rediscache = distributedCache;
        }
        public async Task DeleteBasket(string userName)
        {
            await _rediscache.RemoveAsync(userName);
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basket =await _rediscache.GetStringAsync(userName);
            if (string.IsNullOrWhiteSpace(basket))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart cart)
        {
            await _rediscache.SetStringAsync(cart.UserName, JsonConvert.SerializeObject(cart));
            return await GetBasket(cart.UserName);
        }
    }
}
