using Microsoft.EntityFrameworkCore;
using SnackTech.Products.Common.Enums;
using SnackTech.Products.Driver.DataBase.Context;
using SnackTech.Products.Driver.DataBase.Entities;

namespace SnackTech.Products.Driver.DataBase.Tests.Context;

public class RepositoryDbContextTests
{
    private DbContextOptions<RepositoryDbContext> CreateInMemoryOptions()
    {
        return new DbContextOptionsBuilder<RepositoryDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public void CanAddProduto()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new RepositoryDbContext(options);
        var produto = new Produto
        {
            Id = Guid.NewGuid(),
            Nome = "Produto Teste",
            Descricao = "Descrição do Produto Teste",
            Valor = 10.0m,
            Categoria = CategoriaProduto.Lanche
        };

        // Act
        context.Produtos.Add(produto);
        context.SaveChanges();

        // Assert
        Assert.Equal(1, context.Produtos.CountAsync().Result);
        Assert.Equal("Produto Teste", context.Produtos.SingleAsync().Result.Nome);
    }

    [Fact]
    public void CanUpdateProduto()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new RepositoryDbContext(options);
        var produto = new Produto
        {
            Id = Guid.NewGuid(),
            Nome = "Produto Teste",
            Descricao = "Descrição do Produto Teste",
            Valor = 10.0m,
            Categoria = CategoriaProduto.Lanche
        };
        context.Produtos.Add(produto);
        context.SaveChanges();

        // Act
        produto.Nome = "Produto Atualizado";
        context.Produtos.Update(produto);
        context.SaveChanges();

        // Assert
        Assert.Equal("Produto Atualizado", context.Produtos.SingleAsync().Result.Nome);
    }

    [Fact]
    public void CanDeleteProduto()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new RepositoryDbContext(options);
        var produto = new Produto
        {
            Id = Guid.NewGuid(),
            Nome = "Produto Teste",
            Descricao = "Descrição do Produto Teste",
            Valor = 10.0m,
            Categoria = CategoriaProduto.Lanche
        };
        context.Produtos.Add(produto);
        context.SaveChanges();

        // Act
        context.Produtos.Remove(produto);
        context.SaveChanges();

        // Assert
        Assert.Equal(0, context.Produtos.CountAsync().Result);
    }

    [Fact]
    public void CanRetrieveProduto()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new RepositoryDbContext(options);
        var produto = new Produto
        {
            Id = Guid.NewGuid(),
            Nome = "Produto Teste",
            Descricao = "Descrição do Produto Teste",
            Valor = 10.0m,
            Categoria = CategoriaProduto.Lanche
        };
        context.Produtos.Add(produto);
        context.SaveChanges();

        // Act
        var retrievedProduto = context.Produtos.SingleAsync().Result;

        // Assert
        Assert.Equal(produto.Id, retrievedProduto.Id);
        Assert.Equal(produto.Nome, retrievedProduto.Nome);
        Assert.Equal(produto.Descricao, retrievedProduto.Descricao);
        Assert.Equal(produto.Valor, retrievedProduto.Valor);
        Assert.Equal(produto.Categoria, retrievedProduto.Categoria);
    }
}