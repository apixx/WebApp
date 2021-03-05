using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        //Declaring a dependency on DataContext service (provides access to the application's data)
        private DataContext context;

        public ProductsController(DataContext ctx)
        {
            context = ctx;
        }

        // GET
        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            //Requesting all the Product objects from the database
            return context.Products;
        }

        //Dependencies declared by action methods must be decorated with [FromService]
        [HttpGet("{id}")]
        public Product GetProduct(long id, [FromServices] ILogger<ProductsController> logger)
        {
            //Declaring a dependency on ILoger<T>
            logger.LogDebug("GetProduct Action Invoked");
            return context.Products.Find(id);
        }

        [HttpPost]
        public void SaveProduct([FromBody] Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }

        [HttpPut]
        public void UpdateProduct([FromBody] Product product)
        {
            context.Products.Update(product);
            context.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void DeleteProduct(long id)
        {
            context.Products.Remove(new Product(){ProductId = id});
            context.SaveChanges();
        }
    }
}