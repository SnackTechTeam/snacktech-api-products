using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SnackTech.Products.Common.Dto.Api;
using SnackTech.Products.Core.Interfaces;
using SnackTech.Products.Driver.API.Controllers;

namespace SnackTech.Products.Driver.API.Tests.ControllersUnitTests;

public class ProdutosControllerTestes
{
    private readonly ProdutosController _controller;
    private readonly Mock<ILogger<ProdutosController>> _loggerMock;
    private readonly Mock<IProdutoController> _produtoControllerMock;

    public ProdutosControllerTestes()
    {
        _loggerMock = new Mock<ILogger<ProdutosController>>();
        _produtoControllerMock = new Mock<IProdutoController>();
        _controller = new ProdutosController(_loggerMock.Object, _produtoControllerMock.Object);
    }

    [Fact]
    public async Task Post_DeveRetornarOkResult_QuandoProdutoForCadastradoComSucesso()
    {
        // Arrange
        var novoProduto = new ProdutoSemIdDto
            { Nome = "Produto Teste", Descricao = "Descricao Teste", Categoria = 1, Valor = 10.0m };
        var produtoDto = new ProdutoDto
        {
            IdentificacaoProduto = Guid.NewGuid(), Nome = "Produto Teste", Descricao = "Descricao Teste", Categoria = 1,
            Valor = 10.0m
        };
        var resultadoOperacao = new ResultadoOperacao<ProdutoDto>(produtoDto);

        _produtoControllerMock.Setup(x => x.CadastrarNovoProduto(novoProduto))
            .ReturnsAsync(resultadoOperacao);

        // Act
        var result = await _controller.Post(novoProduto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var produtoRetornado = Assert.IsType<ProdutoDto>(okResult.Value);

        Assert.Equal(produtoDto.IdentificacaoProduto, produtoRetornado.IdentificacaoProduto);
        Assert.Equal(produtoDto.Nome, produtoRetornado.Nome);
        Assert.Equal(produtoDto.Descricao, produtoRetornado.Descricao);
        Assert.Equal(produtoDto.Categoria, produtoRetornado.Categoria);
        Assert.Equal(produtoDto.Valor, produtoRetornado.Valor);
    }

    [Fact]
    public async Task Put_DeveRetornarOkResult_QuandoProdutoForEditadoComSucesso()
    {
        // Arrange
        var identificacao = Guid.NewGuid();
        var produtoEditado = new ProdutoSemIdDto
            { Nome = "Produto Editado", Descricao = "Descricao Editada", Categoria = 1, Valor = 15.0m };
        var produtoDto = new ProdutoDto
        {
            IdentificacaoProduto = identificacao, Nome = "Produto Editado", Descricao = "Descricao Editada",
            Categoria = 1, Valor = 15.0m
        };
        var resultadoOperacao = new ResultadoOperacao<ProdutoDto>(produtoDto);

        _produtoControllerMock.Setup(x => x.EditarProduto(identificacao, produtoEditado))
            .ReturnsAsync(resultadoOperacao);

        // Act
        var result = await _controller.Put(identificacao, produtoEditado);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var produtoRetornado = Assert.IsType<ProdutoDto>(okResult.Value);

        Assert.Equal(produtoDto.IdentificacaoProduto, produtoRetornado.IdentificacaoProduto);
        Assert.Equal(produtoDto.Nome, produtoRetornado.Nome);
        Assert.Equal(produtoDto.Descricao, produtoRetornado.Descricao);
        Assert.Equal(produtoDto.Categoria, produtoRetornado.Categoria);
        Assert.Equal(produtoDto.Valor, produtoRetornado.Valor);
    }

    [Fact]
    public async Task Delete_DeveRetornarOkResult_QuandoProdutoForRemovidoComSucesso()
    {
        // Arrange
        var identificacao = Guid.NewGuid();
        var resultadoOperacao = new ResultadoOperacao();
        _produtoControllerMock.Setup(x => x.RemoverProduto(identificacao)).ReturnsAsync(resultadoOperacao);

        // Act
        var result = await _controller.Delete(identificacao);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task GetByCategory_DeveRetornarOkResult_QuandoProdutosForemEncontrados()
    {
        // Arrange
        var categoriaId = 1;
        var produtos = new List<ProdutoDto>
        {
            new()
            {
                IdentificacaoProduto = Guid.NewGuid(), Nome = "Produto 1", Descricao = "Descricao 1", Categoria = 1,
                Valor = 10.0m
            }
        };
        var resultadoOperacao = new ResultadoOperacao<IEnumerable<ProdutoDto>>(produtos);
        _produtoControllerMock.Setup(x => x.BuscarProdutosPorCategoria(categoriaId)).ReturnsAsync(resultadoOperacao);

        // Act
        var result = await _controller.GetByCategory(categoriaId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.IsAssignableFrom<IEnumerable<ProdutoDto>>(okResult.Value);
    }

    [Fact]
    public async Task GetById_DeveRetornarOkResult_QuandoProdutoForEncontrado()
    {
        // Arrange
        var identificacao = Guid.NewGuid();
        var produtoDto = new ProdutoDto
        {
            IdentificacaoProduto = identificacao, Nome = "Produto 1", Descricao = "Descricao 1", Categoria = 1,
            Valor = 10.0m
        };
        var resultadoOperacao = new ResultadoOperacao<ProdutoDto>(produtoDto);
        _produtoControllerMock.Setup(x => x.BuscarProdutoPorId(identificacao)).ReturnsAsync(resultadoOperacao);
        // Act
        var result = await _controller.GetById(identificacao);
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var produtoRetornado = Assert.IsType<ProdutoDto>(okResult.Value);
        Assert.Equal(produtoDto.IdentificacaoProduto, produtoRetornado.IdentificacaoProduto);
        Assert.Equal(produtoDto.Nome, produtoRetornado.Nome);
        Assert.Equal(produtoDto.Descricao, produtoRetornado.Descricao);
        Assert.Equal(produtoDto.Categoria, produtoRetornado.Categoria);
        Assert.Equal(produtoDto.Valor, produtoRetornado.Valor);
    }
}