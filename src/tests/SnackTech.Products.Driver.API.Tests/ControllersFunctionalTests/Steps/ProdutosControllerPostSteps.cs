using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SnackTech.Products.Common.Dto.Api;
using SnackTech.Products.Core.Interfaces;
using SnackTech.Products.Driver.API.Controllers;
using TechTalk.SpecFlow;

namespace SnackTech.Products.Driver.API.Tests.ControllersFunctionalTests.Steps;

[Binding]
public class ProdutosControllerPostSteps
{
    private readonly ProdutosController controller;
    private readonly Mock<ILogger<ProdutosController>> logger;
    private readonly Mock<IProdutoController> produtoController;
    private ProdutoSemIdDto novoProduto;
    private IActionResult result;

    public ProdutosControllerPostSteps()
    {
        logger = new Mock<ILogger<ProdutosController>>();
        produtoController = new Mock<IProdutoController>();
        controller = new ProdutosController(logger.Object, produtoController.Object);
    }

    [Given(@"que eu tenho um novo produto válido")]
    public void DadoQueEuTenhoUmNovoProdutoValido()
    {
        novoProduto = new ProdutoSemIdDto
        {
            Categoria = 1,
            Nome = "Produto Teste",
            Descricao = "Descrição do Produto Teste",
            Valor = 10.0m
        };

        produtoController.Setup(p => p.CadastrarNovoProduto(novoProduto))
            .ReturnsAsync(new ResultadoOperacao<ProdutoDto>(new ProdutoDto { IdentificacaoProduto = Guid.NewGuid() }));
    }

    [When(@"eu chamar o método Post")]
    public async Task QuandoEuChamarOMetodoPost()
    {
        result = await controller.Post(novoProduto);
    }

    [Then(@"o resultado deve ser um OkObjectResult")]
    public void EntaoOResultadoDeveSerUmOkObjectResult()
    {
        var okResult = Assert.IsType<OkObjectResult>(result);
        var produtoRetornado = Assert.IsType<ProdutoDto>(okResult.Value);
        Assert.NotNull(produtoRetornado.IdentificacaoProduto);
    }
}