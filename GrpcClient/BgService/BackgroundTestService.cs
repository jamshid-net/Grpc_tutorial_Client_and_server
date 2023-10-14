using Grpc.Core;
using Grpc.Net.Client;
using GrpcClientTest;
using System.Runtime.CompilerServices;
using System.Threading;
using WebApi.StaticDatas;

namespace WebApi.BgService;

public class BackgroundTestService : BackgroundService
{
    private Timer _timer;
    private readonly    ILogger<BackgroundTestService> _logger;

    public BackgroundTestService(ILogger<BackgroundTestService> logger)
    {
        _logger = logger;
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(async (call) =>
        {
           await ExecuteTask(call,cancellationToken);
        }, null, TimeSpan.Zero, TimeSpan.FromMinutes(1)); ;

    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }
    private  async Task ExecuteTask(object state, CancellationToken stoppingToken)
    {
        try
        {
            _logger.LogCritical("DATE: " + DateTime.Now);
            var channel = GrpcChannel.ForAddress("https://localhost:7274");
            var getList = new GetProduct.GetProductClient(channel).GetList(new());
            await foreach (var item in GetProductsAsync(getList.ResponseStream, stoppingToken))
            {
                StaticProducts.Products.Add(item);
            }

            _logger.LogCritical("Products successfully added: " + DateTime.Now);
        }
        catch (Exception)
        {
            _logger.LogWarning("Not connection");
        }
            
       
    }
    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }

    private async IAsyncEnumerable<ProductDto>
       GetProductsAsync(IAsyncStreamReader<ProductDto> productsStream,
                       [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        while (await productsStream.MoveNext(cancellationToken))
            yield return productsStream.Current;

    }
}
