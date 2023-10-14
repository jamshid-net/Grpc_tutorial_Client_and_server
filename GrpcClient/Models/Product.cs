using Bogus;
using WebApi.Helpers;

namespace WebApi.Models;

public class Product
{
    public Guid Id { get; set; } 
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Image { get; set; }


    public static Task<IEnumerable<Product>> GetProducts(FilterRoot filter)
    {
        Faker faker = new Faker();
        List<Product> products = new List<Product>();   
        for (int i = 0; i < 100; i++)
        {
            products.Add(new Product
            {
                Id =  Guid.NewGuid(),
                Name = faker.Commerce.ProductName(),
                Description = faker.Commerce.ProductDescription(),  
                Price = faker.Commerce.Price().Parser<decimal>(),
                Image = faker.Image.Abstract(),

            });
        }
        var res = products.Skip(filter.first).Take(filter.rows);
        return Task.FromResult(res);
      
    }
}


