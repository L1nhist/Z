namespace Z.Core.Structures;

/// <summary>
/// Strong type struct as a replacement for int or long structure
/// using as value amount property of class
/// </summary>
[JsonConverter(typeof(SequenceJsonConverter))]
public readonly struct Sequence
    : IComparable, IFormattable, IComparable<int>, IEquatable<int>, IComparable<uint>, IEquatable<uint>,
        IComparable<long>, IEquatable<long>, IComparable<ulong>, IEquatable<ulong>
{
    #region Operators
    public static bool operator ==(Sequence lft, Sequence rgt)
        => lft._value == rgt._value;

    public static bool operator !=(Sequence lft, Sequence rgt)
        => lft._value != rgt._value;

    public static bool operator ==(Sequence lft, int rgt)
        => lft._value == (ulong)rgt;

    public static bool operator !=(Sequence lft, int rgt)
        => lft._value != (ulong)rgt;

    public static bool operator ==(Sequence lft, uint rgt)
        => lft._value == rgt;

    public static bool operator !=(Sequence lft, uint rgt)
        => lft._value != rgt;

    public static bool operator ==(Sequence lft, long rgt)
        => rgt >= 0 && lft._value == (ulong)rgt;

    public static bool operator !=(Sequence lft, long rgt)
        => rgt >= 0 && lft._value != (ulong)rgt;

    public static bool operator ==(Sequence lft, ulong rgt)
        => lft._value == rgt;

    public static bool operator !=(Sequence lft, ulong rgt)
        => lft._value != rgt;

    public static bool operator <(Sequence lft, Sequence rgt)
        => lft._value < rgt._value;

    public static bool operator >(Sequence lft, Sequence rgt)
        => lft._value > rgt._value;

    public static bool operator <=(Sequence lft, Sequence rgt)
        => lft._value <= rgt._value;

    public static bool operator >=(Sequence lft, Sequence rgt)
        => lft._value >= rgt._value;

    public static bool operator <(Sequence lft, int rgt)
        => rgt < 0 || lft._value < (ulong)rgt;

    public static bool operator >(Sequence lft, int rgt)
        => rgt >= 0 && lft._value > (ulong)rgt;

    public static bool operator <=(Sequence lft, int rgt)
        => rgt < 0 || lft._value <= (ulong)rgt;

    public static bool operator >=(Sequence lft, int rgt)
        => rgt >= 0 && lft._value >= (ulong)rgt;

    public static bool operator <(Sequence lft, uint rgt)
        => lft._value < rgt;

    public static bool operator >(Sequence lft, uint rgt)
        => lft._value > rgt;

    public static bool operator <=(Sequence lft, uint rgt)
        => lft._value <= rgt;

    public static bool operator >=(Sequence lft, uint rgt)
        => lft._value >= rgt;

    public static bool operator <(Sequence lft, long rgt)
        => rgt < 0 || lft._value < (ulong)rgt;

    public static bool operator >(Sequence lft, long rgt)
        => rgt >= 0 && lft._value > (ulong)rgt;

    public static bool operator <=(Sequence lft, long rgt)
        => rgt < 0 || lft._value <= (ulong)rgt;

    public static bool operator >=(Sequence lft, long rgt)
        => rgt >= 0 && lft._value >= (ulong)rgt;

    public static bool operator <(Sequence lft, ulong rgt)
        => lft._value < rgt;

    public static bool operator >(Sequence lft, ulong rgt)
        => lft._value > rgt;

    public static bool operator <=(Sequence lft, ulong rgt)
        => lft._value <= rgt;

    public static bool operator >=(Sequence lft, ulong rgt)
        => lft._value >= rgt;

    public static implicit operator int(Sequence value)
        => value._value > int.MaxValue ? int.MaxValue : (int)value._value;

    public static implicit operator Sequence(int value)
        => new(value < 0 ? 0 : (ulong)value);

    public static implicit operator uint(Sequence value)
        => value._value > uint.MaxValue ? uint.MaxValue : (uint)value._value;

    public static implicit operator Sequence(uint value)
        => new(value);

    public static implicit operator long(Sequence value)
        => value._value > long.MaxValue ? long.MaxValue : (long)value._value;

    public static implicit operator Sequence(long value)
        => new(value < 0 ? 0 : (ulong)value);

    public static implicit operator ulong(Sequence value)
        => value._value;

    public static implicit operator Sequence(ulong value)
        => new(value);
    #endregion

    #region Properties
    /// <summary>
    /// Represent as an empty value
    /// </summary>
    public static readonly Sequence Empty = new(0);

    /// <summary>
    /// Initial value of this instance
    /// </summary>
    private readonly ulong _value;

    /// <summary>
    /// Check whether value is empty or not
    /// </summary>
    public bool IsEmpty => _value == 0;
    #endregion

    #region Constructions
    public Sequence(uint value)
        => _value = value;

    public Sequence(ulong value)
        => _value = value;

    public Sequence(string? value)
        => _value = !Util.IsEmpty(value) && ulong.TryParse(value, out ulong seq) ? seq : ulong.MinValue;
    #endregion

    public Sequence Next()
        => new(_value + 1);

    #region Overridens
    /// <inheritdoc/>
    public int CompareTo(object? value)
        => _value.CompareTo(value);

    /// <inheritdoc/>
    public int CompareTo(int value)
        => _value.CompareTo(value);

    /// <inheritdoc/>
    public int CompareTo(uint value)
        => _value.CompareTo(value);

    /// <inheritdoc/>
    public int CompareTo(long value)
        => _value.CompareTo(value);

    /// <inheritdoc/>
    public int CompareTo(ulong value)
        => _value.CompareTo(value);

    /// <inheritdoc/>
    public override bool Equals([NotNullWhen(true)] object? o)
        => _value.Equals(o);

    /// <inheritdoc/>
    public bool Equals(int value)
        => _value.Equals(value);

    /// <inheritdoc/>
    public bool Equals(uint value)
        => _value.Equals(value);

    /// <inheritdoc/>
    public bool Equals(long value)
        => _value.Equals(value);

    /// <inheritdoc/>
    public bool Equals(ulong value)
        => _value.Equals(value);

    /// <inheritdoc/>
    public override int GetHashCode()
        => _value.GetHashCode();

    /// <inheritdoc/>
    public override string ToString()
        => _value.ToString();

    /// <inheritdoc/>
    public string ToString(string? format)
        => _value.ToString(format);

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? provider)
        => _value.ToString(format, provider);
    #endregion
}
