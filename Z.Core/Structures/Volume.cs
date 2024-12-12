namespace Z.Core.Structures;

[JsonConverter(typeof(VolumeJsonConverter))]
public readonly struct Volume
    : IComparable, IFormattable, IComparable<decimal>, IEquatable<decimal>
{
    #region Operators
    public static bool operator ==(Volume lft, Volume rgt)
        => lft._value == rgt._value;

    public static bool operator !=(Volume lft, Volume rgt)
        => lft._value != rgt._value;

    public static bool operator ==(Volume lft, decimal rgt)
        => lft._value == rgt;

    public static bool operator !=(Volume lft, decimal rgt)
        => lft._value != rgt;

    public static bool operator <(Volume lft, Volume rgt)
        => lft._value < rgt._value;

    public static bool operator >(Volume lft, Volume rgt)
        => lft._value > rgt._value;

    public static bool operator <=(Volume lft, Volume rgt)
        => lft._value <= rgt._value;

    public static bool operator >=(Volume lft, Volume rgt)
        => lft._value >= rgt._value;

    public static bool operator <(Volume lft, decimal rgt)
        => lft._value < rgt;

    public static bool operator >(Volume lft, decimal rgt)
        => lft._value > rgt;

    public static bool operator <=(Volume lft, decimal rgt)
        => lft._value <= rgt;

    public static bool operator >=(Volume lft, decimal rgt)
        => lft._value >= rgt;

    public static Quantity operator *(Volume lft, decimal rgt)
        => new(lft._value * rgt, lft.Precision);

    public static Quantity operator *(Volume lft, Price rgt)
        => new(lft._value * rgt, lft.Precision, rgt.Unit);

    public static implicit operator decimal(Volume value)
        => value._value;

    public static implicit operator Volume(decimal value)
        => new(value);
    #endregion

    #region Properties
    /// <summary>
    /// Represent as an empty value
    /// </summary>
    public static readonly Volume Empty = new(decimal.MinValue);

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
    #endregion

    #region Constructions
    public Volume() : this(decimal.MinValue)
    { }

    public Volume(decimal value, byte precision = 0)
    {
        _value = precision > 0 ? Math.Round(value, precision) : value;
        Precision = precision;
    }

    public Volume(string? value)
    {
        var subs = (value?.Trim() ?? "").Split('e');
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
        => Precision > 0 ? $"{Math.Round(_value, Precision)}e{Precision}" : $"{_value}";

    /// <inheritdoc/>
    public string ToString(string? format)
    {
        var vol = Precision > 0 ? Math.Round(_value, Precision) : _value;
        return $"{vol.ToString(format)}";
    }

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? provider)
    {
        var vol = Precision > 0 ? Math.Round(_value, Precision) : _value;
        return $"{vol.ToString(format, provider)}";
    }
    #endregion
}