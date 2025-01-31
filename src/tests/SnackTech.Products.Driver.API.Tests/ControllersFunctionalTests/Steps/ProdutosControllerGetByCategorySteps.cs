using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SnackTech.Products.Common.Dto.Api;
using SnackTech.Products.Core.Interfaces;
using SnackTech.Products.Driver.API.Controllers;
using TechTalk.SpecFlow;
using Xunit;
namespace SnackTech.Products.Driver.API.Tests.ControllersFunctionalTests
{
    [Binding]
    public class ProdutosControllerGetByCategorySteps
    {
        private readonly Mock<ILogger<ProdutosController>> logger;
        private readonly Mock<IProdutoController> produtoController;
        private readonly ProdutosController controller;
        private IActionResult result;
        private int categoriaId;

        public ProdutosControllerGetByCategorySteps()
        {
            logger = new Mock<ILogger<ProdutosController>>();
            produtoController = new Mock<IProdutoController>();
            controller = new ProdutosController(logger.Object, produtoController.Object);
        }

        #region GetByCategory - Buscar produtos por categoria

        [Given(@"que eu tenho um identificador de categoria válido")]
        public void DadoQueEuTenhoUmIdentificadorDeCategoriaValido()
        {
            categoriaId = 1;
            produtoController.Setup(p => p.BuscarProdutosPorCategoria(categoriaId))
                .ReturnsAsync(new ResultadoOperacao<IEnumerable<ProdutoDto>>(new List<ProdutoDto>
                {
                    new ProdutoDto { IdentificacaoProduto = Guid.NewGuid(), Nome = "Produto 1", Descricao = "Desc 1", Categoria = 1, Valor = 10.0m },
                    new ProdutoDto { IdentificacaoProduto = Guid.NewGuid(), Nome = "Produto 2", Descricao = "Desc 2", Categoria = 1, Valor = 15.0m }
                }));
        }

        [When(@"eu chamar o método GetByCategory")]
        public async Task QuandoEuChamarOMetodoGetByCategory()
        {
            result = await controller.GetByCategory(categoriaId);
        }

        [Then(@"o resultado deve ser um OkObjectResult")]
        public void EntaoOResultadoDeveSerUmOkObjectResult()
        {
            var okResult = Assert.IsType<OkObjectResult>(result);
            var produtos = Assert.IsAssignableFrom<IEnumerable<ProdutoDto>>(okResult.Value);
            Assert.NotNull(produtos);
        }

        [Then(@"a lista de produtos deve ser retornada")]
        public void EntaoAListaDeProdutosDeveSerRetornada()
        {
            var okResult = Assert.IsType<OkObjectResult>(result);
            var produtos = Assert.IsAssignableFrom<IEnumerable<ProdutoDto>>(okResult.Value);
            Assert.NotEmpty(produtos);
        }

        #endregion

        #region GetById - Buscar produto por identificador
        [Given(@"que eu tenho um identificador de produto válido")]
        public void DadoQueEuTenhoUmIdentificadorDeProdutoValido()
        {
            var id = Guid.NewGuid();
            produtoController.Setup(p => p.BuscarProdutoPorId(id))
                .ReturnsAsync(new ResultadoOperacao<ProdutoDto>(new ProdutoDto
                {
                    IdentificacaoProduto = id,
                    Nome = "Produto 1",
                    Descricao = "Desc 1",
                    Categoria = 1,
                    Valor = 10.0m
                }));
        }
        [When(@"eu chamar o método GetById")]
        public async Task QuandoEuChamarOMetodoGetById()
        {
            result = await controller.GetById(Guid.NewGuid());
        }
        [Then(@"o resultado deve ser um OkObjectResult com o produto")]
        public void EntaoOResultadoDeveSerUmOkObjectResultComOProduto()
        {
            var okResult = Assert.IsType<OkObjectResult>(result);
            var produto = Assert.IsType<ProdutoDto>(okResult.Value);
            Assert.NotNull(produto);
        }
        [Then(@"o produto deve ser retornado")]
        public void EntaoOProdutoDeveSerRetornado()
        {
            var okResult = Assert.IsType<OkObjectResult>(result);
            var produto = Assert.IsAssignableFrom<ProdutoDto>(okResult.Value);
            Assert.NotNull(produto);
        }
        #endregion
    }
}
