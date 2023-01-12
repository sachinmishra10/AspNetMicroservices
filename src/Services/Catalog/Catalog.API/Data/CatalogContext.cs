using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration config)
        {
            //var client = new MongoClient(config["DatabaseSettings:ConncetionString"]);
            var client = new MongoClient(config.GetValue<string>("DatabaseSettings:ConncetionString"));
            var database = client.GetDatabase(config.GetValue<string>("DatabaseSettings:DatabaseName"));
            Products = database.GetCollection<Product>(config.GetValue<string>("DatabaseSettings:CollectionName"));
            CatalogcontextSeed.SeedData(Products);
        }
        public IMongoCollection<Product> Products { get; }
    }
}
