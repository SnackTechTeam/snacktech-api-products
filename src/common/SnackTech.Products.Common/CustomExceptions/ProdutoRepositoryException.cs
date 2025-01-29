namespace SnackTech.Products.Common.CustomExceptions
{
    public class ProdutoRepositoryException : Exception
    {
        public ProdutoRepositoryException(string message) : base(message)
        {}
    }
}