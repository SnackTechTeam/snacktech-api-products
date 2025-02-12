using FluentAssertions;
using SnackTech.Products.Core.Presenters;

namespace SnackTech.Products.Core.Tests.Presenters;

public class GeralPresenterTest
{
    [Fact]
    public void ApresentarResultadoErroLogicoString_DeveRetornarResultadoOperacaoComMensagemERetornoTrue()
    {
        // Act
        var resultado = GeralPresenter.ApresentarResultadoErroLogico<string>("Mensagem de erro");

        // Assert
        resultado.Should().NotBeNull();
        resultado.Mensagem.Should().Be("Mensagem de erro");
        resultado.Sucesso.Should().BeFalse();
    }

    [Fact]
    public void
        ApresentarResultadoErroLogicoString_DeveRetornarResultadoOperacaoComMensagemERetornoTrue_QuandoMensagemForNull()
    {
        // Act
        var resultado = GeralPresenter.ApresentarResultadoErroLogico<string>(null);

        // Assert
        resultado.Should().NotBeNull();
        resultado.Mensagem.Should().BeNull();
        resultado.Sucesso.Should().BeFalse();
    }

    [Fact]
    public void ApresentarResultadoErroInterno_DeveRetornarResultadoOperacaoComExcecao()
    {
        // Arrange
        var excecao = new Exception("Exceção interna");

        // Act
        var resultado = GeralPresenter.ApresentarResultadoErroInterno(excecao);

        // Assert
        resultado.Should().NotBeNull();
        resultado.Excecao.Should().Be(excecao);
    }

    [Fact]
    public void ApresentarResultadoPadraoSucesso_DeveRetornarResultadoOperacaoComSucessoTrue()
    {
        // Act
        var resultado = GeralPresenter.ApresentarResultadoPadraoSucesso();

        // Assert
        resultado.Should().NotBeNull();
        resultado.Sucesso.Should().BeTrue();
        resultado.Excecao.Should().BeNull();
    }

    [Fact]
    public void ApresentarResultadoErroInternoT_DeveRetornarResultadoOperacaoComExcecao()
    {
        // Arrange
        var excecao = new Exception("Exceção interna");

        // Act
        var resultado = GeralPresenter.ApresentarResultadoErroInterno<string>(excecao);

        // Assert
        resultado.Should().NotBeNull();
        resultado.Excecao.Should().Be(excecao);
    }

    [Fact]
    public void ApresentarResultadoErroLogico_DeveRetornarResultadoOperacaoComMensagem()
    {
        // Act
        var resultado = GeralPresenter.ApresentarResultadoErroLogico("Mensagem de erro");

        // Assert
        resultado.Should().NotBeNull();
        resultado.Mensagem.Should().Be("Mensagem de erro");
        resultado.Sucesso.Should().BeFalse();
    }

    [Fact]
    public void ApresentarResultadoErroLogicoT_DeveRetornarResultadoOperacaoComMensagem()
    {
        // Act
        var resultado = GeralPresenter.ApresentarResultadoErroLogico<string>("Mensagem de erro");

        // Assert
        resultado.Should().NotBeNull();
        resultado.Mensagem.Should().Be("Mensagem de erro");
        resultado.Sucesso.Should().BeFalse();
    }
}