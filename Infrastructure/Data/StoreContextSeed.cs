using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedDataAsync(StoreContext context)
        {
            try
            {
                if (!context.Products.Any())
                {
                    var productBrandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(productBrandsData);
                    foreach (var brand in brands)
                    {
                        context.ProductBrands.Add(brand);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.ProdcutTypes.Any())
                {
                    var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    foreach (var type in types)
                    {
                        context.ProdcutTypes.Add(type);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    foreach (var product in products)
                    {
                        context.Products.Add(product);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.DeliveryMethod.Any())
                {
                    var deliveriesData = File.ReadAllText("../Infrastructure/Data/SeedData/delivery.json");
                    var deliveries = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveriesData);
                    foreach (var delivery in deliveries)
                    {
                        context.DeliveryMethod.Add(delivery);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var stack = e.StackTrace;
            }

        }
    }
}