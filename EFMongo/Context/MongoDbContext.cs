using EFMongo.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace EFMongo.Context;

public class MongoDbContext : DbContext
{
    public DbSet<Produto>? Produtos { get; set; }

    public MongoDbContext(DbContextOptions<MongoDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Produto>().ToCollection("Produtos");
    }
}
