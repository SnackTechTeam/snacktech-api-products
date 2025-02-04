using SnackTech.Products.Common.Dto.Api;
using SnackTech.Products.Core.Domain.Entities;

namespace SnackTech.Products.Core.Presenters;

internal static class ProdutoPresenter
{
    internal static ResultadoOperacao<ProdutoDto> ApresentarResultadoProduto(Produto produto)
    {
        var produtoDto = ConverterParaDto(produto);
        return new ResultadoOperacao<ProdutoDto>(produtoDto);
    }

    internal static ResultadoOperacao<IEnumerable<ProdutoDto>> ApresentarResultadoListaProdutos(
        IEnumerable<Produto> produtos)
    {
        var produtosDtos = produtos.Select(ConverterParaDto);
        return new ResultadoOperacao<IEnumerable<ProdutoDto>>(produtosDtos);
    }

    internal static ProdutoDto ConverterParaDto(Produto produto)
    {
        return new ProdutoDto
        {
            IdentificacaoProduto = produto.Id,
            Categoria = produto.Categoria,
            Descricao = produto.Descricao,
            Nome = produto.Nome,
            Valor = produto.Valor
        };
    }
}