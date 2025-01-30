using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SnackTech.Products.Common.Dto.Api;
using SnackTech.Products.Core.Interfaces;
using SnackTech.Products.Driver.API.Controllers;
using TechTalk.SpecFlow;

[Binding]
public class ProdutosControllerPutSteps
{
    private readonly Mock<ILogger<ProdutosController>> logger;
    private readonly Mock<IProdutoController> produtoController;
    private readonly ProdutosController controller;
    private IActionResult result;
    private Guid identificacaoProduto;
    private ProdutoSemIdDto produtoEditado;

    public ProdutosControllerPutSteps()
    {
        logger = new Mock<ILogger<ProdutosController>>();
        produtoController = new Mock<IProdutoController>();
        controller = new ProdutosController(logger.Object, produtoController.Object);
    }

    [Given(@"que eu tenho um produto existente e dados válidos para edição")]
    public void DadoQueEuTenhoUmProdutoExistenteEDadosValidosParaEdicao()
    {
        identificacaoProduto = Guid.NewGuid();
        produtoEditado = new ProdutoSemIdDto
        {
            Categoria = 1,
            Nome = "Produto Editado",
            Descricao = "Descrição do Produto Editado",
            Valor = 20.0m
        };

        produtoController.Setup(p => p.EditarProduto(identificacaoProduto, produtoEditado))
            .ReturnsAsync(new ResultadoOperacao<ProdutoDto>(new ProdutoDto { IdentificacaoProduto = identificacaoProduto }));
    }

    [When(@"eu chamar o método Put")]
    public async Task QuandoEuChamarOMetodoPut()
    {
        result = await controller.Put(identificacaoProduto, produtoEditado);
    }

    [Then(@"o resultado deve ser um OkObjectResult")]
    public void EntaoOResultadoDeveSerUmOkObjectResult()
    {
        var okResult = Assert.IsType<OkObjectResult>(result);
        var produtoRetornado = Assert.IsType<ProdutoDto>(okResult.Value);
        Assert.Equal(identificacaoProduto, produtoRetornado.IdentificacaoProduto);
    }
}