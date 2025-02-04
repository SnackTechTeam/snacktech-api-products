using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using SnackTech.Products.Driver.DataBase.Entities;

namespace SnackTech.Products.Driver.DataBase.Context;

[ExcludeFromCodeCoverage]
public class RepositoryDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Produto> Produtos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RepositoryDbContext).Assembly);
    }
}