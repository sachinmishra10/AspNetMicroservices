using System.Collections.Generic;

namespace Basket.API.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; }
        public List<ShoppingCartItem> list { get; set; }

        public ShoppingCart()
        {

        }
        public ShoppingCart(string username)
        {
            UserName = username;
        }

        public decimal TotalPrice 
        { 
            get 
            {
                decimal totalPrice = 0;
                foreach(var item in list)
                {
                    totalPrice += item.Price*item.Quantity;
                }
                return totalPrice;
            }
        }
    }
}
