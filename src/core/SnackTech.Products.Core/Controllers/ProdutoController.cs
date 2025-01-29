using SnackTech.Products.Common.Dto.Api;
using SnackTech.Products.Common.Interfaces.DataSources;
using SnackTech.Products.Core.Gateways;
using SnackTech.Products.Core.Interfaces;
using SnackTech.Products.Core.UseCases;

namespace SnackTech.Products.Core.Controllers;

public class ProdutoController(IProdutoDataSource produtoDataSource) : IProdutoController
{
    public async Task<ResultadoOperacao<ProdutoDto>> CadastrarNovoProduto(ProdutoSemIdDto produtoSemIdDto){
        
        var gateway = new ProdutoGateway(produtoDataSource);
        
        var novoProduto = await ProdutoUseCase.CriarNovoProduto(produtoSemIdDto,gateway);
        
       return novoProduto;
    } 

    public async Task<ResultadoOperacao<ProdutoDto>> EditarProduto(Guid identificacao, ProdutoSemIdDto produtoParaAlterar){
        var gateway = new ProdutoGateway(produtoDataSource);

        var produtoAlterado = await ProdutoUseCase.EditarProduto(identificacao,produtoParaAlterar,gateway);

        return produtoAlterado;
    }    

    public async Task<ResultadoOperacao> RemoverProduto(Guid id){
        var gateway = new ProdutoGateway(produtoDataSource);

        var remocaoProduto = await ProdutoUseCase.RemoverProduto(id,gateway);

        return remocaoProduto;
    }  

    public async Task<ResultadoOperacao<IEnumerable<ProdutoDto>>> BuscarProdutosPorCategoria(int categoriaId){
        var gateway = new ProdutoGateway(produtoDataSource);

        var produtos = await ProdutoUseCase.BuscarProdutoPorCategoria(categoriaId,gateway);

        return produtos;
    } 
}