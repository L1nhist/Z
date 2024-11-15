using Z.Core.StrongTypes;

namespace Z.Core.Serialize;

public class EpochJsonConverter : JsonConverter<Epoch>
{
    public override Epoch Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        => long.TryParse(reader.GetString(), out long ticks) ? new(ticks) : Epoch.Zero;

    public override void Write(Utf8JsonWriter writer, Epoch epoch, JsonSerializerOptions options)
        => writer.WriteNumberValue(epoch.Timestamp);
}

public class UuidJsonConverter : JsonConverter<Uuid>
{
    public override Uuid Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        => new(reader.GetString());

    public override void Write(Utf8JsonWriter writer, Uuid guid, JsonSerializerOptions options)
        => writer.WriteStringValue(guid.ToString());
}

public class PriceJsonConverter : JsonConverter<Price>
{
    public override Price Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        => new(reader.GetString());

    public override void Write(Utf8JsonWriter writer, Price price, JsonSerializerOptions options)
        => writer.WriteStringValue(price.ToString());
}

public class QuantityJsonConverter : JsonConverter<Quantity>
{
    public override Quantity Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        => new(reader.GetString());

    public override void Write(Utf8JsonWriter writer, Quantity quantity, JsonSerializerOptions options)
        => writer.WriteRawValue(quantity.ToString());
}

public class VolumeJsonConverter : JsonConverter<Volume>
{
    public override Volume Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        => new(reader.GetString());

    public override void Write(Utf8JsonWriter writer, Volume quantity, JsonSerializerOptions options)
        => writer.WriteRawValue(quantity.ToString());
}