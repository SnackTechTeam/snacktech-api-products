namespace SnackTech.Products.Core.Domain.Types;

internal struct GuidValido : IEquatable<GuidValido>
{
    internal Guid Valor { readonly get; private set; }

    internal GuidValido(string guid)
    {
        Valor = ValidarValor(guid);
    }

    internal GuidValido(Guid guid)
    {
        Valor = guid;
    }

    public static implicit operator GuidValido(string guid)
    {
        return new GuidValido(guid);
    }

    public static implicit operator Guid(GuidValido guid)
    {
        return guid.Valor;
    }

    public static implicit operator GuidValido(Guid guid)
    {
        return new GuidValido(guid);
    }

    public static implicit operator string(GuidValido guid)
    {
        return guid.Valor.ToString();
    }

    public readonly override string ToString()
    {
        return Valor.ToString();
    }

    private static Guid ValidarValor(string guidValue)
    {
        if (Guid.TryParse(guidValue, out var guid)) return guid;

        throw new ArgumentException($"A Identificação informada {guidValue} não é um Guid válido.");
    }

    public bool Equals(GuidValido other)
    {
        if (other == null) return false;
        if (ReferenceEquals(this, other)) return true;

        return Valor == other.Valor;
    }
}