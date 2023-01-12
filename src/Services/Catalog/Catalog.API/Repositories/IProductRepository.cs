using Catalog.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(string Id);
        Task<IEnumerable<Product>> GetProductByName(string Name);
        Task<IEnumerable<Product>> GetProductsByCategory(string categoryName);

        Task AddProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string Id);


            
    }
}
