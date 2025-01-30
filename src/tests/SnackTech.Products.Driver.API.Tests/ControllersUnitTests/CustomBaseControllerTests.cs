using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SnackTech.Products.Common.Dto.Api;
using SnackTech.Products.Driver.API.Controllers;
using SnackTech.Products.Driver.API.CustomResponses;

namespace SnackTech.Products.Driver.API.Tests.ControllersUnitTests
{
    public class CustomBaseControllerTests
    {
        private readonly Mock<ILogger> logger;
        private readonly Mock<CustomBaseController> mockBaseController;
        private readonly CustomBaseController baseController;
        private readonly CustomBaseController _controller;

        public CustomBaseControllerTests()
        {
            logger = new Mock<ILogger>();
            mockBaseController = new Mock<CustomBaseController>(logger.Object)
            {
                CallBase = true
            };
            baseController = mockBaseController.Object;
            _controller = new TestController(logger.Object);
        }

        [Fact]
        public async Task ExecucaoPadraoWithSuccess()
        {
            var nomeMetodo = "Controller.Nome";
            static async Task<ResultadoOperacao<int>> taskFunc()
            {
                await Task.FromResult(0);
                return new ResultadoOperacao<int>(10);
            }

            var task = taskFunc();

            var resultado = await baseController.ExecucaoPadrao(nomeMetodo, task);

            Assert.IsType<OkObjectResult>(resultado);
        }

        [Fact]
        public async Task ExecucaoPadraoReturningBadRequest()
        {
            var nomeMetodo = "Controller.Nome";

            static async Task<ResultadoOperacao<int>> taskFunc()
            {
                await Task.FromResult(0);
                return new ResultadoOperacao<int>("Erro de lógica", true);
            }

            var task = taskFunc();

            var resultado = await baseController.ExecucaoPadrao(nomeMetodo, task);
            var requestResult = Assert.IsType<BadRequestObjectResult>(resultado);
            var payload = Assert.IsType<ErrorResponse>(requestResult.Value);
            Assert.Null(payload.Exception);
            Assert.Equal("Erro de lógica", payload.Message);
        }

        [Fact]
        public async Task ExecucaoPadraoReturningInternalServerErroFromTask()
        {
            var nomeMetodo = "Controller.Nome";

            static async Task<ResultadoOperacao<int>> taskFunc()
            {
                await Task.FromResult(0);
                return new ResultadoOperacao<int>(new Exception("Erro inesperado"));
            }

            var task = taskFunc();

            var resultado = await baseController.ExecucaoPadrao(nomeMetodo, task);
            var requestResult = Assert.IsType<ObjectResult>(resultado);
            var payload = Assert.IsType<ErrorResponse>(requestResult.Value);
            Assert.NotNull(payload);
            Assert.Equal("Erro inesperado", payload.Message);
        }

        [Fact]
        public async Task ExecucaoPadraoReturningInternalServerErrorFromProcessing()
        {
            var nomeMetodo = "Controller.Nome";

            static async Task<ResultadoOperacao<int>> taskFunc()
            {
                await Task.FromResult(0);
                throw new Exception("Erro inesperado");
            }

            var task = taskFunc();

            var resultado = await baseController.ExecucaoPadrao(nomeMetodo, task);
            var requestResult = Assert.IsType<ObjectResult>(resultado);
            var payload = Assert.IsType<ErrorResponse>(requestResult.Value);
            Assert.NotNull(payload);
            Assert.Equal("Erro inesperado", payload.Message);
        }

        [Fact]
        public async Task ExecucaoPadrao_Success_ReturnsOk()
        {
            // Arrange
            var resultadoOperacao = new ResultadoOperacao();
            var task = Task.FromResult(resultadoOperacao);

            // Act
            var result = await _controller.ExecucaoPadrao("TestMethod", task);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task ExecucaoPadrao_Exception_ReturnsInternalServerError()
        {
            // Arrange
            var exception = new Exception("Test exception");
            var resultadoOperacao = new ResultadoOperacao(exception);
            var task = Task.FromResult(resultadoOperacao);

            // Act
            var result = await _controller.ExecucaoPadrao("TestMethod", task);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }

        [Fact]
        public async Task ExecucaoPadrao_Failure_ReturnsBadRequest()
        {
            // Arrange
            var resultadoOperacao = new ResultadoOperacao("Test error message");
            var task = Task.FromResult(resultadoOperacao);

            // Act
            var result = await _controller.ExecucaoPadrao("TestMethod", task);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var errorResponse = Assert.IsType<ErrorResponse>(badRequestResult.Value);
            Assert.Equal("Test error message", errorResponse.Message);
        }

        private class TestController : CustomBaseController
        {
            public TestController(ILogger logger) : base(logger) { }
        }
    }
}
