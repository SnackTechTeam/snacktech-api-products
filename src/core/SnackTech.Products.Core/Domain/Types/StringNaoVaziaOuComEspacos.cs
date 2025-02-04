namespace SnackTech.Products.Core.Domain.Types;

internal class StringNaoVaziaOuComEspacos : IEquatable<StringNaoVaziaOuComEspacos>
{
    private string valor = default!;

    internal StringNaoVaziaOuComEspacos(string value)
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

    public bool Equals(StringNaoVaziaOuComEspacos? other)
    {
        if (other == null) return false;
        if (ReferenceEquals(this, other)) return true;

        return Valor == other.Valor;
    }

    public static implicit operator StringNaoVaziaOuComEspacos(string value)
    {
        return new StringNaoVaziaOuComEspacos(value);
    }

    public static implicit operator string(StringNaoVaziaOuComEspacos valor)
    {
        return valor.ToString();
    }

    public override string ToString()
    {
        return Valor;
    }

    private static void ValidarValorString(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || string.IsNullOrEmpty(value))
            throw new ArgumentException("O valor atribuído não pode ser nulo, vazio ou somente com espaços");
    }
}