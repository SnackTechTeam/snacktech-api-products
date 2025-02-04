using SnackTech.Products.Driver.DataBase.Context;

namespace SnackTech.Products.Driver.DataBase.Tests.Context;

public class RepositoryDbContextFactoryTests
{
    [Fact]
    public void CreateDbContext_ShouldReturnDbContextInstance()
    {
        // Arrange
        var factory = new RepositoryDbContextFactory();

        // Act
        var context = factory.CreateDbContext(new[] { "UseInMemoryDatabase" });

        // Assert
        Assert.NotNull(context);
        Assert.IsType<RepositoryDbContext>(context);
    }

    [Fact]
    public void CreateDbContext_ShouldConfigureDbContextOptions()
    {
        // Arrange
        var factory = new RepositoryDbContextFactory();

        // Act
        var context = factory.CreateDbContext(new[] { "UseInMemoryDatabase" });

        // Assert
        Assert.NotNull(context);
        Assert.NotNull(context.Model);
    }
}