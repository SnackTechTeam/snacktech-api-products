using Moq;
using SnackTech.Products.Common.Dto.Api;
using SnackTech.Products.Common.Interfaces.DataSources;
using SnackTech.Products.Core.Controllers;
using SnackTech.Products.Core.Interfaces;
using Xunit;
using SnackTech.Products.Common.Dto.DataSource;
using ProdutoDto = SnackTech.Products.Common.Dto.DataSource.ProdutoDto;

namespace SnackTech.Products.Core.Tests.Controllers
{
    public class ProdutoControllerTests
    {
        private readonly ProdutoController _controller;
        private readonly Mock<IProdutoDataSource> _produtoDataSourceMock;

        public ProdutoControllerTests()
        {
            _produtoDataSourceMock = new Mock<IProdutoDataSource>();
            _controller = new ProdutoController(_produtoDataSourceMock.Object);
        }

        [Fact]
        public async Task CadastrarNovoProduto_DeveRetornarResultadoOperacaoComProdutoDto()
        {
            // Arrange
            var produtoSemIdDto = new ProdutoSemIdDto
            {
                Nome = "Produto Teste",
                Descricao = "Descricao Teste",
                Categoria = 1,
                Valor = 10.0m
            };
            var produtoDto = new ProdutoDto()
            {
                Id = Guid.NewGuid(),
                Nome = "Produto Teste",
                Descricao = "Descricao Teste",
                Categoria = 1,
                Valor = 10.0m
            };
            var resultadoOperacao = new ResultadoOperacao<ProdutoDto>(produtoDto);

            _produtoDataSourceMock.Setup(x => x.InserirProdutoAsync(It.IsAny<ProdutoDto>()))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.CadastrarNovoProduto(produtoSemIdDto);

            // Assert
            Assert.True(result.Sucesso);
            Assert.Equal(produtoDto.Nome, result.Dados.Nome);
        }

        [Fact]
        public async Task BuscarProdutosPorCategoria_DeveRetornarResultadoOperacaoComListaDeProdutoDto()
        {
            // Arrange
            var categoriaId = 1;
            var produtos = new List<ProdutoDto>
            {
                new ProdutoDto
                {
                    Id = Guid.NewGuid(),
                    Nome = "Produto 1",
                    Descricao = "Descricao 1",
                    Categoria = 1,
                    Valor = 10.0m
                }
            };
            var resultadoOperacao = new ResultadoOperacao<IEnumerable<ProdutoDto>>(produtos);

            _produtoDataSourceMock.Setup(x => x.PesquisarPorCategoriaIdAsync(categoriaId))
                .ReturnsAsync(produtos);

            // Act
            var result = await _controller.BuscarProdutosPorCategoria(categoriaId);

            // Assert
            Assert.True(result.Sucesso);
            Assert.NotEmpty(result.Dados);
        }

        [Fact]
        public async Task BuscarProdutoPorId_DeveRetornarResultadoOperacaoComProdutoDto()
        {
            // Arrange
            var identificacao = Guid.NewGuid();
            var produtoDto = new ProdutoDto
            {
                Id = identificacao,
                Nome = "Produto 1",
                Descricao = "Descricao 1",
                Categoria = 1,
                Valor = 10.0m
            };
            var resultadoOperacao = new ResultadoOperacao<ProdutoDto>(produtoDto);

            _produtoDataSourceMock.Setup(x => x.PesquisarPorIdentificacaoAsync(identificacao))
                .ReturnsAsync(produtoDto);

            // Act
            var result = await _controller.BuscarProdutoPorId(identificacao);

            // Assert
            Assert.True(result.Sucesso);
            Assert.Equal(produtoDto.Nome, result.Dados.Nome);
        }
    }
}
