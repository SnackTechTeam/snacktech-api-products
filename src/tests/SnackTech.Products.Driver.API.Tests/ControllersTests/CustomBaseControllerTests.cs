using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SnackTech.Products.Common.Dto.Api;
using SnackTech.Products.Driver.API.Controllers;
using SnackTech.Products.Driver.API.CustomResponses;

namespace SnackTech.Products.Driver.API.Tests.ControllersTests
{
    public class CustomBaseControllerTests
    {
        private readonly Mock<ILogger> logger;
        private readonly Mock<CustomBaseController> mockBaseController;
        private readonly CustomBaseController baseController;

        public CustomBaseControllerTests()
        {
            logger = new Mock<ILogger>();
            mockBaseController = new Mock<CustomBaseController>(logger.Object)
            {
                CallBase = true
            };
            baseController = mockBaseController.Object;
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
    }
}
