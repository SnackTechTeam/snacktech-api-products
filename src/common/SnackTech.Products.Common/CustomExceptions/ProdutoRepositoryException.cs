using System.Diagnostics.CodeAnalysis;

namespace SnackTech.Products.Common.CustomExceptions;


[ExcludeFromCodeCoverage]
public class ProdutoRepositoryException : Exception
{
    [ExcludeFromCodeCoverage]
    public ProdutoRepositoryException(string message) : base(message)
    {
    }
}