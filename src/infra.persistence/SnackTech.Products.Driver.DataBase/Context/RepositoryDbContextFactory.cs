using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SnackTech.Products.Driver.DataBase.Context;

public class RepositoryDbContextFactory : IDesignTimeDbContextFactory<RepositoryDbContext>
{
    public RepositoryDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<RepositoryDbContext>();

        if (args.Contains("UseInMemoryDatabase"))
        {
            optionsBuilder.UseInMemoryDatabase("TestDatabase");
        }
        else
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connString = configuration.GetConnectionString("DefaultConnection")
                             ?? throw new InvalidOperationException("Connection string is not set in configuration.");

            optionsBuilder.UseSqlServer(connString);
        }

        return new RepositoryDbContext(optionsBuilder.Options);
    }
}