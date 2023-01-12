using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }
        public async Task AddProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string Id)
        {
            var data= await _context.Products.DeleteOneAsync(p => p.Id == Id);
            return data.IsAcknowledged && data.DeletedCount > 0;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var data= await _context
                .Products
                .FindAsync(p => true);

            return await data.ToListAsync();
                
        }

        public async Task<Product> GetProductById(string Id)
        {
            var data =await _context.Products.FindAsync(p => p.Id == Id);
            return await data.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string Name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.ElemMatch(p => p.Name, Name);
            var data=await _context.Products.FindAsync(filter);
            return await data.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string categoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);

           return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var data=await _context.Products.ReplaceOneAsync(filter: p => p.Id == product.Id, replacement: product);

            return data.IsAcknowledged && data.ModifiedCount > 0;
        }
    }
}
