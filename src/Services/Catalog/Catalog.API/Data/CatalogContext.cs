using Catalog.API.Data.Interfaces;
using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {            
            Console.WriteLine("Database Connection String: " + configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            Console.WriteLine("Database Name: " + configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            Console.WriteLine("Collection Name: " + configuration.GetValue<string>("DatabaseSettings:CollectionName"));

            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            CatalogContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
