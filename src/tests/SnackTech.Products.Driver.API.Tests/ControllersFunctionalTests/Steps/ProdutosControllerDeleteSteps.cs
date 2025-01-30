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
    public class ProdutosControllerDeleteSteps
    {
        private readonly Mock<ILogger<ProdutosController>> logger;
        private readonly Mock<IProdutoController> produtoController;
        private readonly ProdutosController controller;
        private IActionResult result;
        private Guid identificacaoProduto;

        public ProdutosControllerDeleteSteps()
        {
            logger = new Mock<ILogger<ProdutosController>>();
            produtoController = new Mock<IProdutoController>();
            controller = new ProdutosController(logger.Object, produtoController.Object);
        }

        #region Delete - Remover um produto existente

        [Given(@"que eu tenho um identificador de produto válido")]
        public void DadoQueEuTenhoUmIdentificadorDeProdutoValido()
        {
            identificacaoProduto = Guid.NewGuid();
            produtoController.Setup(p => p.RemoverProduto(identificacaoProduto))
                .ReturnsAsync(new ResultadoOperacao());
        }

        [When(@"eu chamar o método Delete")]
        public async Task QuandoEuChamarOMetodoDelete()
        {
            result = await controller.Delete(identificacaoProduto);
        }

        [Then(@"o resultado deve ser um OkResult")]
        public void EntaoOResultadoDeveSerUmOkResult()
        {
            Assert.IsType<OkResult>(result);
        }

        #endregion
    }
}