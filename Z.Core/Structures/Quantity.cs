namespace Z.Core.Structures;

[JsonConverter(typeof(QuantityJsonConverter))]
public readonly struct Quantity
    : IComparable, IFormattable, IComparable<decimal>, IEquatable<decimal>
{
    #region Operators
    public static bool operator ==(Quantity lft, Quantity rgt)
        => lft._value == rgt._value;

    public static bool operator !=(Quantity lft, Quantity rgt)
        => lft._value != rgt._value;

    public static bool operator ==(Quantity lft, decimal rgt)
        => lft._value == rgt;

    public static bool operator !=(Quantity lft, decimal rgt)
        => lft._value != rgt;

    public static bool operator <(Quantity lft, Quantity rgt)
        => lft._value < rgt._value;

    public static bool operator >(Quantity lft, Quantity rgt)
        => lft._value > rgt._value;

    public static bool operator <=(Quantity lft, Quantity rgt)
        => lft._value <= rgt._value;

    public static bool operator >=(Quantity lft, Quantity rgt)
        => lft._value >= rgt._value;

    public static bool operator <(Quantity lft, decimal rgt)
        => lft._value < rgt;

    public static bool operator >(Quantity lft, decimal rgt)
        => lft._value > rgt;

    public static bool operator <=(Quantity lft, decimal rgt)
        => lft._value <= rgt;

    public static bool operator >=(Quantity lft, decimal rgt)
        => lft._value >= rgt;

    public static Price operator *(Quantity lft, decimal rgt)
        => new(lft._value / rgt, lft.Precision, lft.Unit);

    public static Price operator /(Quantity lft, Volume rgt)
        => new(lft._value / (decimal)rgt, lft.Precision, lft.Unit);

    public static Volume operator /(Quantity lft, Price rgt)
        => new(lft._value / (decimal)rgt, lft.Precision);

    public static implicit operator decimal(Quantity value)
        => value._value;

    public static implicit operator Quantity(decimal value)
        => new(value);
    #endregion

    #region Properties
    /// <summary>
    /// Represent as an empty value
    /// </summary>
    public static readonly Quantity Empty = new(decimal.MinValue);

    /// <summary>
    /// Initial value of this instance
    /// </summary>
    private readonly decimal _value;

    /// <summary>
    /// Check whether value is empty or not
    /// </summary>
    public bool IsEmpty => _value == decimal.MinValue;

    /// <summary>
    /// Precision number to be rounded
    /// </summary>
    public byte Precision { get; }

    /// <summary>
    /// Name of the currency unit
    /// </summary>
    public string Unit { get; }
    #endregion

    #region Constructions
    public Quantity(decimal value, string? unit = "")
    {
        _value = value;
        Precision = 0;
        Unit = unit ?? "";
    }

    public Quantity(decimal value, byte precision, string? unit = "")
    {
        _value = precision > 0 ? Math.Round(value, precision) : value;
        Precision = precision;
        Unit = unit ?? "";
    }

    public Quantity(string? value)
    {
        var parts = (value?.Trim() ?? "").Split('$');
        Unit = parts.Length == 2 ? parts[1].Trim() : "";

        var subs = parts[0].Split('e');
        Precision = (subs.Length == 2 && byte.TryParse(subs[1], out byte prec)) ? prec : (byte)0;
        _value = subs[0] != "" && decimal.TryParse(subs[0], out decimal price) ? price : 0;
    }
    #endregion

    #region Overridens
    /// <inheritdoc/>
    public int CompareTo(object? value)
        => _value.CompareTo(value);

    /// <inheritdoc/>
    public int CompareTo(decimal value)
        => _value.CompareTo(Precision > 0 ? Math.Round(value, Precision) : value);

    /// <inheritdoc/>
    public override bool Equals([NotNullWhen(true)] object? o)
        => _value.Equals(o);

    /// <inheritdoc/>
    public bool Equals(decimal value)
        => _value.Equals(Precision > 0 ? Math.Round(value, Precision) : value);

    /// <inheritdoc/>
    public override int GetHashCode()
        => _value.GetHashCode();

    /// <inheritdoc/>
    public override string ToString()
    {
        var unit = Unit == "" ? "" : $"${Unit}";
        return Precision > 0 ? $"{Math.Round(_value, Precision)}e{Precision}{unit}" : $"{_value}{unit}";
    }

    /// <inheritdoc/>
    public string ToString(string? format)
    {
        var unit = Unit == "" ? "" : $"${Unit}";
        var price = Precision > 0 ? Math.Round(_value, Precision) : _value;
        return $"{price.ToString(format)}{unit}";
    }

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? provider)
    {
        var unit = Unit == "" ? "" : $"${Unit}";
        var price = Precision > 0 ? Math.Round(_value, Precision) : _value;
        return $"{price.ToString(format, provider)}{unit}";
    }
    #endregion
}