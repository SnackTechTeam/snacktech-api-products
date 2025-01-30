using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SnackTech.Products.Common.Dto.Api;
using SnackTech.Products.Driver.API.Controllers;
using SnackTech.Products.Driver.API.CustomResponses;
using TechTalk.SpecFlow;

namespace SnackTech.Products.Driver.API.Tests.ControllersFunctionalTests.Steps
{
    [Binding]
    public class CustomBaseControllerSteps
    {
        private readonly Mock<ILogger> logger;
        private readonly CustomBaseController controller;
        private IActionResult result;
        private Task<ResultadoOperacao<int>> task;

        public CustomBaseControllerSteps()
        {
            logger = new Mock<ILogger>();
            controller = new TestController(logger.Object);
        }

        [Given(@"que eu tenho uma tarefa válida que retorna um ResultadoOperacao bem-sucedido")]
        public void DadoQueEuTenhoUmaTarefaValidaQueRetornaUmResultadoOperacaoBemSucedido()
        {
            task = Task.FromResult(new ResultadoOperacao<int>(10));
        }

        [Given(@"que eu tenho uma tarefa válida que retorna um ResultadoOperacao com uma mensagem de erro")]
        public void DadoQueEuTenhoUmaTarefaValidaQueRetornaUmResultadoOperacaoComUmaMensagemDeErro()
        {
            task = Task.FromResult(new ResultadoOperacao<int>("Erro de lógica", true));
        }

        [Given(@"que eu tenho uma tarefa válida que retorna um ResultadoOperacao com uma exceção")]
        public void DadoQueEuTenhoUmaTarefaValidaQueRetornaUmResultadoOperacaoComUmaExcecao()
        {
            task = Task.FromResult(new ResultadoOperacao<int>(new Exception("Erro inesperado")));
        }

        [Given(@"que eu tenho uma tarefa que lança uma exceção")]
        public void DadoQueEuTenhoUmaTarefaQueLancaUmaExcecao()
        {
            task = Task.Run(() => Task.FromResult(new ResultadoOperacao<int>(new Exception("Erro inesperado"))));
        }

        [When(@"eu chamar o método ExecucaoPadrao")]
        public async Task QuandoEuChamarOMetodoExecucaoPadrao()
        {
            result = await controller.ExecucaoPadrao("TestMethod", task);
        }

        [Then(@"o resultado deve ser um OkObjectResult")]
        public void EntaoOResultadoDoCustomBaseControllerDeveSerUmOkObjectResult()
        {
            Assert.IsType<OkObjectResult>(result);
        }

        [Then(@"o resultado deve ser um BadRequestObjectResult")]
        public void EntaoOResultadoDoCustomBaseControllerDeveSerUmBadRequestObjectResult()
        {
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var errorResponse = Assert.IsType<ErrorResponse>(badRequestResult.Value);
            Assert.Equal("Erro de lógica", errorResponse.Message);
        }

        [Then(@"o resultado deve ser um ObjectResult")]
        public void EntaoOResultadoDoCustomBaseControllerDeveSerUmObjectResult()
        {
            var objectResult = Assert.IsType<ObjectResult>(result);
            var errorResponse = Assert.IsType<ErrorResponse>(objectResult.Value);
            Assert.Equal("Erro inesperado", errorResponse.Message);
        }

        [Then(@"a mensagem de erro deve ser ""(.*)""")]
        public void EntaoAMensagemDeErroDeveSer(string mensagemEsperada)
        {
            var objectResult = Assert.IsType<ObjectResult>(result);
            var errorResponse = Assert.IsType<ErrorResponse>(objectResult.Value);
            Assert.Equal(mensagemEsperada, errorResponse.Message);
        }

        private class TestController : CustomBaseController
        {
            public TestController(ILogger logger) : base(logger) { }
        }
    }
}
