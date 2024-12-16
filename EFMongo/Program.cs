using EFMongo.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

string? connectionString = builder.Configuration.GetSection("MongoDbSettings").GetValue<string>("ConnectionString");
string? nomeBanco = builder.Configuration.GetSection("MongoDbSettings").GetValue<string>("Database");

builder.Services.AddDbContext<MongoDbContext>(options =>
{
    options.UseMongoDB(connectionString, nomeBanco);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
