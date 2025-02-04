namespace SnackTech.Products.Core.Domain.Types;

internal class StringNaoVazia : IEquatable<StringNaoVazia>
{
    private string valor = default!;

    internal StringNaoVazia(string value)
    {
        Valor = value;
    }

    internal string Valor
    {
        get => valor;
        set
        {
            ValidarValorString(value);
            valor = value;
        }
    }

    public bool Equals(StringNaoVazia? other)
    {
        if (other == null) return false;
        if (ReferenceEquals(this, other)) return true;

        return Valor == other.Valor;
    }

    public static implicit operator StringNaoVazia(string value)
    {
        return new StringNaoVazia(value);
    }

    public static implicit operator string(StringNaoVazia valor)
    {
        return valor.ToString();
    }

    public override string ToString()
    {
        return Valor;
    }


    private static void ValidarValorString(string value)
    {
        if (string.IsNullOrEmpty(value)) throw new ArgumentException("O valor atribuído não pode ser nulo ou vazio");
    }
}