using Bogus;
using Grpc.Core;

namespace GrpcTest.GrpcServices;

public class ProductService:GetProduct.GetProductBase
{
    public override Task<ProductDto> Get(GetRequest request, ServerCallContext context)
    {
        ProductDto product = new ProductDto();
        product.Name = "Milk";
        product.Price = 1000;
        product.Description = "Description";
        product.Id = Guid.NewGuid().ToString();
        product.Image = "No photo";

        return Task.FromResult(product);
    }
    public override async Task GetList(GetRequest request, IServerStreamWriter<ProductDto> responseStream, ServerCallContext context)
    {
        //List<ProductDto> list = new List<ProductDto>();
        Faker faker = new Faker();  
        for (int i = 0; i < 100; i++)
        {
            ProductDto product = new ProductDto();
            product.Name = faker.Commerce.ProductName();
            product.Price = float.Parse(faker.Commerce.Price());
            product.Description = faker.Commerce.ProductDescription();
            product.Id = Guid.NewGuid().ToString();
            product.Image = faker.Image.ToString();
           // list.Add(product);
           await responseStream.WriteAsync(product);
        }

        
    }


}
