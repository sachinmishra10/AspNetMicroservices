using Basket.API.Entities;
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
}
