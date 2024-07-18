using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CompiledQuery
{
    public class Program
    {
        private static readonly Func<ProductContext, int, Product?> GetProductById =
            EF.CompileQuery((ProductContext context, int productId) =>
                context.Set<Product>().FirstOrDefault(x => x.ProductId == productId));
        
        //async
        private static readonly Func<ProductContext, string, decimal, Task<Product?>> GetProduct =
            EF.CompileAsyncQuery((ProductContext context, string name, decimal price) => 
                context.Set<Product>().FirstOrDefault(x => x.Price == price && x.Name.ToLower() == name.ToLower()));
        
        public static void Main(string[] args)
        {
            using var db = new ProductContext();
            // db.Database.EnsureCreated();
            // for (var i = 1; i <= 10; i++)
            // {
            //     db.Products.Add(new Product
            //     {
            //         ProductId = i+1,
            //         Name = $"Product {i}",
            //         Price = i * 10
            //     });
            // }
            // db.SaveChanges();
            
            

            foreach (var product in db.Products)
            {
                Console.WriteLine($"Id:{product.ProductId},Product: {product.Name}, Price: {product.Price}");
            }

            const int productId5 = 5;

            var productById = GetProductById(db, 5);

            var getProduct = GetProduct(db, "Product 1", 10);

            Console.WriteLine(productById != null
                ? $"Found Product: {productById.Name}, Price: {productById.Price}"
                : $"Product with ID {productId5} not found.");
            
            
            Console.WriteLine("________________________________________________________________");

            Console.WriteLine("Async result");

            var result =  getProduct.Result;
            Console.WriteLine($"{result.Name}");
        }
    }
}