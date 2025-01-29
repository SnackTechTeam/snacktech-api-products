using SnackTech.Products.Common.Dto.Api;

namespace SnackTech.Products.Core.Interfaces;

public interface IProdutoController
{
    Task<ResultadoOperacao<ProdutoDto>> CadastrarNovoProduto(ProdutoSemIdDto produtoSemIdDto);
    Task<ResultadoOperacao<ProdutoDto>> EditarProduto(Guid identificacao, ProdutoSemIdDto produtoParaAlterar);
    Task<ResultadoOperacao> RemoverProduto(Guid id);
    Task<ResultadoOperacao<IEnumerable<ProdutoDto>>> BuscarProdutosPorCategoria(int categoriaId);
}