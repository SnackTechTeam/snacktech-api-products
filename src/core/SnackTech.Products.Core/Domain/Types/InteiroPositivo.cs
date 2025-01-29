namespace SnackTech.Products.Core.Domain.Types;

internal struct InteiroPositivo : IEquatable<InteiroPositivo>
{
    private int valor;

    internal int Valor
    {
        readonly get { return valor; }
        set
        {
            ValidarValor(value);
            valor = value;
        }
    }

    internal InteiroPositivo(int valor)
    {
        Valor = valor;
    }

    public static implicit operator InteiroPositivo(int valor)
    {
        return new InteiroPositivo(valor);
    }

    public static implicit operator int(InteiroPositivo valor)
    {
        return valor.Valor;
    }

    public override readonly string ToString()
    {
        return Valor.ToString();
    }

    private static void ValidarValor(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("O valor precisa ser maior que zero");
        }
    }

    public bool Equals(InteiroPositivo other)
    {
        if (other == null) return false;
        if (ReferenceEquals(this, other)) return true;

        return this.Valor == other.Valor;
    }
}
