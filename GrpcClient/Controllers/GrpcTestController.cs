using Grpc.Core;
using Grpc.Net.Client;
using GrpcClientTest;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using WebApi.StaticDatas;

namespace WebApi.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class GrpcTestController : ControllerBase
{
    [HttpGet]
    public async Task<ProductDto> GetProduct()
    {
        using var channel = GrpcChannel.ForAddress("https://localhost:7274");

        var client = new GetProduct.GetProductClient(channel);
        GetRequest getRequest = new();
        var product =await client.GetAsync(getRequest);
        return product;
    }

    [HttpGet]
    public  IAsyncEnumerable<ProductDto> GetProducts(CancellationToken cancellationToken)
    {
        var channel = GrpcChannel.ForAddress("https://localhost:7274");
        var getList = new GetProduct.GetProductClient(channel).GetList(new());
      
        return  GetProductsAsync(getList.ResponseStream, cancellationToken);
    }


    [HttpGet]
    public async Task<List<ProductDto>> GetStaticProducts()
        => StaticProducts.Products;

    [NonAction]
    private async IAsyncEnumerable<ProductDto> 
        GetProductsAsync(IAsyncStreamReader<ProductDto> productsStream, 
                        [EnumeratorCancellation] CancellationToken cancellationToken)
    { 
        while (await productsStream.MoveNext(cancellationToken))
               yield return productsStream.Current;
    
    }
        
        
    
}
