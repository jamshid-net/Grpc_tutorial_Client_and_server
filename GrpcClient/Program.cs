using WebApi.BgService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument();
builder.Services.AddGrpc();
builder.Services.AddHostedService<BackgroundTestService>();

builder.Services.AddCors(setup =>
{
    setup.AddPolicy("AllowOrigin", cfg =>
    {
        cfg.AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .WithOrigins("http://localhost:4200", "http://localhost:4202");
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi3();
}

app.UseHttpsRedirection();
app.UseCors("AllowOrigin");

app.UseAuthorization();


app.MapControllers();

app.Run();
